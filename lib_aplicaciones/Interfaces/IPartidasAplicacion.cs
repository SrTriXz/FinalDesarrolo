using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IPartidasAplicacion
    {
        void Configurar(string StringConexion);
        List<Partidas> Porestado(Partidas? entidad);
        List<Partidas> Listar();
        Partidas? Guardar(Partidas? entidad);
        Partidas? Modificar(Partidas? entidad);
        Partidas? Borrar(Partidas? entidad);
    }
}
