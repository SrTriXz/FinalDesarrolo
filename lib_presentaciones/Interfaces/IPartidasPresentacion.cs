using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IPartidasPresentacion
    {
        Task<List<Partidas>> Listar();
        Task<List<Partidas>> PorEstudiante(Partidas? entidad);
        Task<Partidas?> Guardar(Partidas? entidad);
        Task<Partidas?> Modificar(Partidas? entidad);
        Task<Partidas?> Borrar(Partidas? entidad);
    }
}