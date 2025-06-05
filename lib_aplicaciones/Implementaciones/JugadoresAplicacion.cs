using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class JugadoresAplicacion : IJugadoresAplicacion
    {
        private IConexion? IConexion = null;

        public JugadoresAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Jugadores? Borrar(Jugadores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Jugadores!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Jugadores? Guardar(Jugadores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Jugadores!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Jugadores> Listar()
        {
            return this.IConexion!.Jugadores!.Take(20).ToList();
        }

        public List<Jugadores> Pornombre(Jugadores? entidad)
        {
            return this.IConexion!.Jugadores!
                .Where(x => x.nombre!.Contains(entidad!.nombre!))
                .ToList();
        }

        public Jugadores? Modificar(Jugadores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Jugadores>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
