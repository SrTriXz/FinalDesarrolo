using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
namespace lib_presentaciones.Implementaciones
{
    public class JugadoresLanzamientosPresentacion : IJugadoresLanzamientosPresentacion
    {
        private Comunicaciones? comunicaciones = null;
        public async Task<List<JugadoresLanzamientos>> Listar()
        {
            var lista = new List<JugadoresLanzamientos>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresLanzamientos/Listar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<JugadoresLanzamientos>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<List<JugadoresLanzamientos>> Porjugador(JugadoresLanzamientos? entidad)
        {
            var lista = new List<JugadoresLanzamientos>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresLanzamientos/Porjugador");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<JugadoresLanzamientos>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<JugadoresLanzamientos?> Guardar(JugadoresLanzamientos? entidad)
        {
            if (entidad!.id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresLanzamientos/Guardar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresLanzamientos>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<JugadoresLanzamientos?> Modificar(JugadoresLanzamientos? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresLanzamientos/Modificar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresLanzamientos>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<JugadoresLanzamientos?> Borrar(JugadoresLanzamientos? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "JugadoresLanzamientos/Borrar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<JugadoresLanzamientos>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public object PorJugador(JugadoresLanzamientos jugadoresLanzamientos)
        {
            throw new NotImplementedException();
        }
    }
}