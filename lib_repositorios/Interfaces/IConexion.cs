using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        public DbSet<Bolas>? Bolas { get; set; }
        public DbSet<Jugadores>? Jugadores { get; set; }
        public DbSet<JugadoresBolas>? JugadoresBolas { get; set; }
        public DbSet<JugadoresLanzamientos>? JugadoresLanzamientos { get; set; }
        public DbSet<JugadoresPartidas>? JugadoresPartidas { get; set; }
        public DbSet<Lanzamientos>? Lanzamientos { get; set; }
        public DbSet<Partidas>? Partidas { get; set; }
        public DbSet<Pistas>? Pistas { get; set; }
        public DbSet<AuditLog>? AuditLogs { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}