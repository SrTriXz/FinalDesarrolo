using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface ILanzamientosAplicacion
    {
        void Configurar(string StringConexion);
        List<Lanzamientos> Pornombre(Lanzamientos? entidad);
        List<Lanzamientos> Listar();
        Lanzamientos? Guardar(Lanzamientos? entidad);
        Lanzamientos? Modificar(Lanzamientos? entidad);
        Lanzamientos? Borrar(Lanzamientos? entidad);
    }
}
