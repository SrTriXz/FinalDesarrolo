using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IJugadoresPartidasAplicacion
    {
        void Configurar(string StringConexion);
        List<JugadoresPartidas> Porcodigo(JugadoresPartidas? entidad);
        List<JugadoresPartidas> Listar();
        JugadoresPartidas? Guardar(JugadoresPartidas? entidad);
        JugadoresPartidas? Modificar(JugadoresPartidas? entidad);
        JugadoresPartidas? Borrar(JugadoresPartidas? entidad);
    }
}
