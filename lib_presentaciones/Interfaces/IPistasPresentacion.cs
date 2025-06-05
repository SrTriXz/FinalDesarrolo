using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IPistasPresentacion
    {
        Task<List<Pistas>> Listar();
        Task<List<Pistas>> Porestado(Pistas? entidad);
        Task<Pistas?> Guardar(Pistas? entidad);
        Task<Pistas?> Modificar(Pistas? entidad);
        Task<Pistas?> Borrar(Pistas? entidad);
    }
}