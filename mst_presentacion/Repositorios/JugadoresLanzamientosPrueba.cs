using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using mst_presentacion.Nucleo;

namespace mst_presentacion.Repositorios
{
    [TestClass]
    public class JugadoresLanzamientosPrueba
    {
        private readonly IConexion? iConexion;
        private List<JugadoresLanzamientos>? lista;
        private JugadoresLanzamientos? entidad;

        public JugadoresLanzamientosPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.JugadoresLanzamientos!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.JugadoresLanzamientos(1,2)!;
            this.iConexion!.JugadoresLanzamientos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.pino_derribado = 10;

            var entry = this.iConexion!.Entry<JugadoresLanzamientos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.JugadoresLanzamientos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}