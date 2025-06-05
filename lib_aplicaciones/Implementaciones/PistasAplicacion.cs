using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PistasAplicacion : IPistasAplicacion
    {
        private IConexion? IConexion = null;

        public PistasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Pistas? Borrar(Pistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Pistas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pistas? Guardar(Pistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Pistas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Pistas> Listar()
        {
            return this.IConexion!.Pistas!.Take(20).ToList();
        }

        public List<Pistas> Pornombre(Pistas? entidad)
        {
            return this.IConexion!.Pistas!
                .Where(x => x.nombre!.Contains(entidad!.nombre!))
                .ToList();
        }

        public Pistas? Modificar(Pistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Pistas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
