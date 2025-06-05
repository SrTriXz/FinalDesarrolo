using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
namespace lib_presentaciones.Implementaciones
{
    public class JugadoresBolasPresentacion : IJugadoresBolasPresentacion
    {
        private Comunicaciones? comunicaciones = null;
        public async Task<List<JugadoresBolas>> Listar()
        {
            var lista = new List<JugadoresBolas>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresBolas/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<JugadoresBolas>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<List<JugadoresBolas>> Porefectividadmbre(JugadoresBolas? entidad)
        {
            var lista = new List<JugadoresBolas>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresBolas/Porefectividadmbre");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<JugadoresBolas>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<JugadoresBolas?> Guardar(JugadoresBolas? entidad)
        {
            if (entidad!.id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresBolas/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresBolas>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<JugadoresBolas?> Modificar(JugadoresBolas? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresBolas/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresBolas>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<JugadoresBolas?> Borrar(JugadoresBolas? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresBolas/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresBolas>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public Task<List<JugadoresBolas>> Porefectividad(JugadoresBolas? entidad)
        {
            throw new NotImplementedException();
        }
    }
}