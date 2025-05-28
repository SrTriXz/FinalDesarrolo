using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IJugadoresBolasPresentacion
    {
        Task<List<JugadoresBolas>> Listar();
        Task<List<JugadoresBolas>> Porefectividad(JugadoresBolas? entidad);
        Task<JugadoresBolas?> Guardar(JugadoresBolas? entidad);
        Task<JugadoresBolas?> Modificar(JugadoresBolas? entidad);
        Task<JugadoresBolas?> Borrar(JugadoresBolas? entidad);
    }
}