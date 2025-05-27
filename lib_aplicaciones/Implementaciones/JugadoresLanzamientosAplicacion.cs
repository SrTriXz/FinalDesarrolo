using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class JugadoresLanzamientosAplicacion : IJugadoresLanzamientosAplicacion
    {
        private IConexion? IConexion = null;

        public JugadoresLanzamientosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public JugadoresLanzamientos? Borrar(JugadoresLanzamientos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.JugadoresLanzamientos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public JugadoresLanzamientos? Guardar(JugadoresLanzamientos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.JugadoresLanzamientos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<JugadoresLanzamientos> Listar()
        {
            return this.IConexion!.JugadoresLanzamientos!.Take(20).ToList();
        }

        public List<JugadoresLanzamientos> PorJugador(JugadoresLanzamientos? entidad)
        {
            return this.IConexion!.JugadoresLanzamientos!
.Where(x => x.Jugador!.nombre!.Contains(entidad!.pino_derribado!.ToString()))
                .ToList();
        }

        public JugadoresLanzamientos? Modificar(JugadoresLanzamientos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<JugadoresLanzamientos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
