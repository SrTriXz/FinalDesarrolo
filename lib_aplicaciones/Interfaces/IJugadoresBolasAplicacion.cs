using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IJugadoresBolasAplicacion
    {
        void Configurar(string StringConexion);
        List<JugadoresBolas> Porefectividad(JugadoresBolas? entidad);
        List<JugadoresBolas> Listar();
        JugadoresBolas? Guardar(JugadoresBolas? entidad);
        JugadoresBolas? Modificar(JugadoresBolas? entidad);
        JugadoresBolas? Borrar(JugadoresBolas? entidad);
    }
}
