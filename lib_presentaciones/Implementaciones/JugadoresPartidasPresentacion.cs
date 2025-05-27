using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
namespace lib_presentaciones.Implementaciones
{
    public class JugadoresPartidasPresentacion : IJugadoresPartidasPresentacion
    {
        private Comunicaciones? comunicaciones = null;
        public async Task<List<JugadoresPartidas>> Listar()
        {
            var lista = new List<JugadoresPartidas>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresPartidas/Listar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<JugadoresPartidas>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<List<JugadoresPartidas>> Porjugador(JugadoresPartidas? entidad)
        {
            var lista = new List<JugadoresPartidas>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresPartidas/Porjugador");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<JugadoresPartidas>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<JugadoresPartidas?> Guardar(JugadoresPartidas? entidad)
        {
            if (entidad!.id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresPartidas/Guardar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresPartidas>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<JugadoresPartidas?> Modificar(JugadoresPartidas? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresPartidas/Modificar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresPartidas>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<JugadoresPartidas?> Borrar(JugadoresPartidas? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresPartidas/Borrar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresPartidas>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}