using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
namespace lib_presentaciones.Implementaciones
{
    public class LanzamientosPresentacion : ILanzamientosPresentacion
    {
        private Comunicaciones? comunicaciones = null;
        public async Task<List<Lanzamientos>> Listar()
        {
            var lista = new List<Lanzamientos>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Lanzamientos/Listar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Lanzamientos>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<List<Lanzamientos>> Pornombre(Lanzamientos? entidad)
        {
            var lista = new List<Lanzamientos>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Lanzamientos/Pornombre");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Lanzamientos>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<Lanzamientos?> Guardar(Lanzamientos? entidad)
        {
            if (entidad!.id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Lanzamientos/Guardar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Lanzamientos>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<Lanzamientos?> Modificar(Lanzamientos? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Lanzamientos/Modificar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Lanzamientos>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<Lanzamientos?> Borrar(Lanzamientos? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Lanzamientos/Borrar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Lanzamientos>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}