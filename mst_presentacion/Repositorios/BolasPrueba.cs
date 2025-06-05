using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using mst_presentacion.Nucleo;

namespace mst_presentacion.Repositorios
{
    [TestClass]
    public class BolasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Bolas>? lista;
        private Bolas? entidad;

        public BolasPrueba()
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
            this.lista = this.iConexion!.Bolas!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Bolas()!;
            this.iConexion!.Bolas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Color = "Rojo";

            var entry = this.iConexion!.Entry<Bolas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Bolas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}