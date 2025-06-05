using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class LanzamientosAplicacion : ILanzamientosAplicacion
    {
        private IConexion? IConexion = null;

        public LanzamientosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Lanzamientos? Borrar(Lanzamientos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Lanzamientos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Lanzamientos? Guardar(Lanzamientos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Lanzamientos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Lanzamientos> Listar()
        {
            return this.IConexion!.Lanzamientos!.Take(20).ToList();
        }

        public List<Lanzamientos> Pornombre(Lanzamientos? entidad)
        {
            return this.IConexion!.Lanzamientos!
                .Where(x => x.nombre!.Contains(entidad!.nombre!))
                .ToList();
        }

        public Lanzamientos? Modificar(Lanzamientos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Lanzamientos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
