using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IBolasAplicacion
    {
        void Configurar(string StringConexion);
        List<Bolas> Pornombre(Bolas? entidad);
        List<Bolas> Listar();
        Bolas? Guardar(Bolas? entidad);
        Bolas? Modificar(Bolas? entidad);
        Bolas? Borrar(Bolas? entidad);
    }
}

