using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IPistasAplicacion
    {
        void Configurar(string StringConexion);
        List<Pistas> Pornombre(Pistas? entidad);
        List<Pistas> Listar();
        Pistas? Guardar(Pistas? entidad);
        Pistas? Modificar(Pistas? entidad);
        Pistas? Borrar(Pistas? entidad);
    }
}
