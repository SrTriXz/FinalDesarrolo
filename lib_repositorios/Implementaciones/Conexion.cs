using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Http; // Para IHttpContextAccessor
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims; // Para ClaimTypes
using System.Threading;
using System.Threading.Tasks;

namespace lib_repositorios.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private bool _isAuditingNow = false; // Flag para prevenir recursión en SaveChanges

        // --- TUS DbSet ---
        public DbSet<Bolas>? Bolas { get; set; }
        public DbSet<Jugadores>? Jugadores { get; set; }
        public DbSet<JugadoresBolas>? JugadoresBolas { get; set; }
        public DbSet<JugadoresLanzamientos>? JugadoresLanzamientos { get; set; }
        public DbSet<JugadoresPartidas>? JugadoresPartidas { get; set; }
        public DbSet<Lanzamientos>? Lanzamientos { get; set; }
        public DbSet<Partidas>? Partidas { get; set; }
        public DbSet<Pistas>? Pistas { get; set; }
        
        public DbSet<AuditLog>? AuditLogs { get; set; }


        public Conexion() { }

        public Conexion(DbContextOptions<Conexion> options, IHttpContextAccessor? httpContextAccessor = null)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                if (!string.IsNullOrEmpty(StringConexion))
                {
                    optionsBuilder.UseSqlServer(StringConexion);
                }
                else
                {

                     throw new InvalidOperationException("La cadena de conexión no ha sido configurada.");
                }
            }
            
            
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        }

        

        private string GetCurrentUserId()
        {
           
            var userName = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            return string.IsNullOrEmpty(userName) ? "SYSTEM_OR_ANONYMOUS" : userName;
        }

        
        public override int SaveChanges()
        {
           
            return SaveChangesAsync(true, CancellationToken.None).GetAwaiter().GetResult();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            if (_isAuditingNow) 
            {
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }

            var auditEntries = CreateAuditEntriesForCurrentChanges();

            
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            if (auditEntries.Any())
            {
                try
                {
                    _isAuditingNow = true; 
                    ApplyKeyValuesFromResolvedTemporaryProperties(auditEntries); 

                    if (this.AuditLogs != null) 
                    {
                        this.AuditLogs.AddRange(auditEntries.Select(ae => ae.ToAuditLog()));
                        
                        await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
                    }
                }
                finally
                {
                    _isAuditingNow = false; 
                }
            }
            return result;
        }

        private List<AuditEntryHelper> CreateAuditEntriesForCurrentChanges()
        {
            ChangeTracker.DetectChanges(); 
            var entries = new List<AuditEntryHelper>();
            var currentUserId = GetCurrentUserId();

            foreach (var entry in ChangeTracker.Entries())
            {
                
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntryHelper(entry)
                {
                    TableName = entry.Metadata.GetTableName() ?? entry.Entity.GetType().Name, 
                    UserId = currentUserId,
                    Timestamp = DateTime.UtcNow 
                };

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey()) 
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        if (property.IsTemporary) auditEntry.TemporaryProperties.Add(property); 
                        continue;
                    }

                    switch (entry.State) 
                    {
                        case EntityState.Added:
                            auditEntry.ActionType = "Added";
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.ChangedColumns.Add(propertyName);
                            if (property.IsTemporary) auditEntry.TemporaryProperties.Add(property);
                            break;
                        case EntityState.Deleted:
                            auditEntry.ActionType = "Deleted";
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.ChangedColumns.Add(propertyName);
                            break;
                        case EntityState.Modified:
                            if (property.IsModified) 
                            {
                                auditEntry.ActionType = "Modified"; 
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.ChangedColumns.Add(propertyName);
                            }
                            break;
                    }
                }
                
                if (!string.IsNullOrEmpty(auditEntry.ActionType)) entries.Add(auditEntry);
            }
            return entries;
        }

        private void ApplyKeyValuesFromResolvedTemporaryProperties(List<AuditEntryHelper> auditEntries)
        {
            
            foreach (var auditEntry in auditEntries.Where(ae => ae.HasTemporaryProperties))
            {
                foreach (var tempProperty in auditEntry.TemporaryProperties)
                {
                    var propertyName = tempProperty.Metadata.Name;
                    if (tempProperty.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = tempProperty.CurrentValue;
                    }
                    
                    if (auditEntry.ActionType == "Added" && auditEntry.NewValues.ContainsKey(propertyName))
                    {
                        auditEntry.NewValues[propertyName] = tempProperty.CurrentValue;
                    }
                }
            }
        }
    }

   
    internal class AuditEntryHelper
    {
        public AuditEntryHelper(EntityEntry entry) { Entry = entry; }
        public EntityEntry Entry { get; }
        public string? UserId { get; set; }
        public string? TableName { get; set; }
        public string? ActionType { get; set; } 
        public DateTime Timestamp { get; set; }


        public Dictionary<string, object?> KeyValues { get; } = new Dictionary<string, object?>();
        public Dictionary<string, object?> OldValues { get; } = new Dictionary<string, object?>();
        public Dictionary<string, object?> NewValues { get; } = new Dictionary<string, object?>();
        public List<string> ChangedColumns { get; } = new List<string>(); 

        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>(); 
        public bool HasTemporaryProperties => TemporaryProperties.Any();

        
        public AuditLog ToAuditLog() => new AuditLog
        {
            UserId = UserId,
            EntityType = TableName,
            ActionType = ActionType,
            Timestamp = Timestamp,
            
            EntityKey = KeyValues.Any() ? JsonConvert.SerializeObject(KeyValues, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) : null,
            OldValues = OldValues.Any() ? JsonConvert.SerializeObject(OldValues, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) : null,
            NewValues = NewValues.Any() ? JsonConvert.SerializeObject(NewValues, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) : null,
            ChangedColumns = ChangedColumns.Any() ? JsonConvert.SerializeObject(ChangedColumns) : null
        };
    }
}