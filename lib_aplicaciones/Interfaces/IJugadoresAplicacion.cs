using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IJugadoresAplicacion
    {
        void Configurar(string StringConexion);
        List<Jugadores> Pornombre(Jugadores? entidad);
        List<Jugadores> Listar();
        Jugadores? Guardar(Jugadores? entidad);
        Jugadores? Modificar(Jugadores? entidad);
        Jugadores? Borrar(Jugadores? entidad);
    }
}
