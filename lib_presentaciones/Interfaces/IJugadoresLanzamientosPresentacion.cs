using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IJugadoresLanzamientosPresentacion
    {
        Task<List<JugadoresLanzamientos>> Listar();
        Task<List<JugadoresLanzamientos>> Porjugador(JugadoresLanzamientos? entidad);
        Task<JugadoresLanzamientos?> Guardar(JugadoresLanzamientos? entidad);
        Task<JugadoresLanzamientos?> Modificar(JugadoresLanzamientos? entidad);
        Task<JugadoresLanzamientos?> Borrar(JugadoresLanzamientos? entidad);
        object PorJugador(JugadoresLanzamientos jugadoresLanzamientos);
    }
}