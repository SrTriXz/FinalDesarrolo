using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PartidasAplicacion : IPartidasAplicacion
    {
        private IConexion? IConexion = null;

        public PartidasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Partidas? Borrar(Partidas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Partidas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Partidas? Guardar(Partidas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Partidas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Partidas> Listar()
        {
            return this.IConexion!.Partidas!.Take(20).ToList();
        }

        public List<Partidas> Porestado(Partidas? entidad)
        {
            return this.IConexion!.Partidas!
                .Where(x => x.estado!.Contains(entidad!.estado!))
                .ToList();
        }

        public Partidas? Modificar(Partidas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Partidas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
