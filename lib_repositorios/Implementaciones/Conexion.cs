using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Bolas>? Bolas { get; set; }
        public DbSet<Jugadores>? Jugadores { get; set; }
        public DbSet<JugadoresBolas>? JugadoresBolas { get; set; }
        public DbSet<JugadoresLanzamientos>? JugadoresLanzamientos { get; set; }
        public DbSet<JugadoresPartidas>? JugadoresPartidas { get; set; }
        public DbSet<Lanzamientos>? Lanzamientos { get; set; }
        public DbSet<Partidas>? Partidas { get; set; }
        public DbSet<Pistas>? Pistas { get; set; }


    }
}