using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IJugadoresLanzamientosAplicacion
    {
        void Configurar(string StringConexion);
        List<JugadoresLanzamientos> PorJugador(JugadoresLanzamientos? entidad);
        List<JugadoresLanzamientos> Listar();
        JugadoresLanzamientos? Guardar(JugadoresLanzamientos? entidad);
        JugadoresLanzamientos? Modificar(JugadoresLanzamientos? entidad);
        JugadoresLanzamientos? Borrar(JugadoresLanzamientos? entidad);
    }
}
