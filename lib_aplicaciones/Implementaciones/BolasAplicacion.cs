using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class BolasAplicacion : IBolasAplicacion
    {
        private IConexion? IConexion = null;

        public BolasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Bolas? Borrar(Bolas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Bolas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Bolas? Guardar(Bolas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Bolas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Bolas> Listar()
        {
            return this.IConexion!.Bolas!.Take(20).ToList();
        }

        public List<Bolas> Pornombre(Bolas? entidad)
        {
            return this.IConexion!.Bolas!
                .Where(x => x.nombre!.Contains(entidad!.nombre!))
                .ToList();
        }

        public Bolas? Modificar(Bolas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Bolas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
