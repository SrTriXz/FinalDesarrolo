using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IJugadoresPresentacion
    {
        Task<List<Jugadores>> Listar();
        Task<List<Jugadores>> PorEstudiante(Jugadores? entidad);
        Task<Jugadores?> Guardar(Jugadores? entidad);
        Task<Jugadores?> Modificar(Jugadores? entidad);
        Task<Jugadores?> Borrar(Jugadores? entidad);
    }
}