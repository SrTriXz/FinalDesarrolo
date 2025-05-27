using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class JugadoresPartidasPrueba
    {
        private readonly IConexion? iConexion;
        private List<JugadoresPartidas>? lista;
        private JugadoresPartidas? entidad;

        public JugadoresPartidasPrueba()
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
            this.lista = this.iConexion!.JugadoresPartidas!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.JugadoresPartidas(1,2)!;
            this.iConexion!.JugadoresPartidas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.codigo = "XX1XX1";

            var entry = this.iConexion!.Entry<JugadoresPartidas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.JugadoresPartidas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}