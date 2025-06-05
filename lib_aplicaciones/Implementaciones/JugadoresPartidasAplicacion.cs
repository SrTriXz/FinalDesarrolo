using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class JugadoresPartidasAplicacion : IJugadoresPartidasAplicacion
    {
        private IConexion? IConexion = null;

        public JugadoresPartidasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public JugadoresPartidas? Borrar(JugadoresPartidas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.JugadoresPartidas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public JugadoresPartidas? Guardar(JugadoresPartidas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.JugadoresPartidas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<JugadoresPartidas> Listar()
        {
            return this.IConexion!.JugadoresPartidas!.Take(20).ToList();
        }

        public List<JugadoresPartidas> Porcodigo(JugadoresPartidas? entidad)
        {
            return this.IConexion!.JugadoresPartidas!
                .Where(x => x.codigo!.Contains(entidad!.codigo!))
                .ToList();
        }

        public JugadoresPartidas? Modificar(JugadoresPartidas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<JugadoresPartidas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
