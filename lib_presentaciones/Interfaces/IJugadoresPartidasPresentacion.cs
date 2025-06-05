using lib_dominio.Entidades;
namespace lib_presentaciones.Interfaces
{
    public interface IJugadoresPartidasPresentacion
    {
        Task<List<JugadoresPartidas>> Listar();
        Task<List<JugadoresPartidas>> Porcodigo(JugadoresPartidas? entidad);
        Task<JugadoresPartidas?> Guardar(JugadoresPartidas? entidad);
        Task<JugadoresPartidas?> Modificar(JugadoresPartidas? entidad);
        Task<JugadoresPartidas?> Borrar(JugadoresPartidas? entidad);
       
    }
}