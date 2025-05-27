using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IJugadoresBolasPresentacion
    {
        Task<List<JugadoresBolas>> Listar();
        Task<List<JugadoresBolas>> PorEstudiante(JugadoresBolas? entidad);
        Task<JugadoresBolas?> Guardar(JugadoresBolas? entidad);
        Task<JugadoresBolas?> Modificar(JugadoresBolas? entidad);
        Task<JugadoresBolas?> Borrar(JugadoresBolas? entidad);
    }
}