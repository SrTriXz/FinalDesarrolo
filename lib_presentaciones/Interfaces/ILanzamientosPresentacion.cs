using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface ILanzamientosPresentacion
    {
        Task<List<Lanzamientos>> Listar();
        Task<List<Lanzamientos>> PorEstudiante(Lanzamientos? entidad);
        Task<Lanzamientos?> Guardar(Lanzamientos? entidad);
        Task<Lanzamientos?> Modificar(Lanzamientos? entidad);
        Task<Lanzamientos?> Borrar(Lanzamientos? entidad);
    }
}