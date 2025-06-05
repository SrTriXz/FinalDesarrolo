using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using mst_presentacion.Nucleo;

namespace mst_presentacion.Repositorios
{
    [TestClass]
    public class PistasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Pistas>? lista;
        private Pistas? entidad;

        public PistasPrueba()
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
            this.lista = this.iConexion!.Pistas!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Pistas()!;
            this.iConexion!.Pistas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.estado = "Disponible";

            var entry = this.iConexion!.Entry<Pistas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Pistas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}