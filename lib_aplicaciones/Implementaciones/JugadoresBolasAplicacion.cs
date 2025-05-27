using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class JugadoresBolasAplicacion : IJugadoresBolasAplicacion
    {
        private IConexion? IConexion = null;

        public JugadoresBolasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public JugadoresBolas? Borrar(JugadoresBolas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.JugadoresBolas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public JugadoresBolas? Guardar(JugadoresBolas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.JugadoresBolas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<JugadoresBolas> Listar()
        {
            return this.IConexion!.JugadoresBolas!.Take(20).ToList();
        }

        public List<JugadoresBolas> Porefectividad(JugadoresBolas? entidad)
        {
            return this.IConexion!.JugadoresBolas!
                .Where(x => x.efectividad!.Contains(entidad!.efectividad!))
                .ToList();
        }

        public JugadoresBolas? Modificar(JugadoresBolas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<JugadoresBolas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
