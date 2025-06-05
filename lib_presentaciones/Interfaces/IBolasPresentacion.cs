using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IBolasPresentacion
    {
        Task<List<Bolas>> Listar();
        Task<List<Bolas>> Pornombre(Bolas? entidad);
        Task<Bolas?> Guardar(Bolas? entidad);
        Task<Bolas?> Modificar(Bolas? entidad);
        Task<Bolas?> Borrar(Bolas? entidad);
    }
}