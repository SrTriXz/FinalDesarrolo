using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
namespace lib_presentaciones.Implementaciones
{
    public class JugadoresPresentacion : IJugadoresPresentacion
    {
        private Comunicaciones? comunicaciones = null;
        public async Task<List<Jugadores>> Listar()
        {
            var lista = new List<Jugadores>();
            var datos = new Dictionary<string, object>();
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Jugadores/Listar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Jugadores>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<List<Jugadores>> Porjugador(Jugadores? entidad)
        {
            var lista = new List<Jugadores>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Jugadores/Porjugador");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Jugadores>>(
            JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<Jugadores?> Guardar(Jugadores? entidad)
        {
            if (entidad!.id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Jugadores/Guardar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Jugadores>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<Jugadores?> Modificar(Jugadores? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Jugadores/Modificar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Jugadores>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
        public async Task<Jugadores?> Borrar(Jugadores? entidad)
        {
            if (entidad!.id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Jugadores/Borrar");
            var respuesta = await comunicaciones!.Execute(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Jugadores>(
            JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}