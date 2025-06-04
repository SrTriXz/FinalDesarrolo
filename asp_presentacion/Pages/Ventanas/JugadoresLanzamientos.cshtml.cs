using ClosedXML.Excel;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class JugadoresLanzamientosModel : PageModel
    {
        private IJugadoresLanzamientosPresentacion? iPresentacion = null;

        public JugadoresLanzamientosModel(IJugadoresLanzamientosPresentacion iPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                Filtro = new JugadoresLanzamientos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public JugadoresLanzamientos? Actual { get; set; }
        [BindProperty] public JugadoresLanzamientos? Filtro { get; set; }
        [BindProperty] public List<JugadoresLanzamientos>? Lista { get; set; }
        [BindProperty] public IFormFile? ArchivoImagen { get; set; }


        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                //var variable_session = HttpContext.Session.GetString("Usuario");
                //if (String.IsNullOrEmpty(variable_session))
                //{
                //    HttpContext.Response.Redirect("/");
                //    return;
                //}

                Filtro!.Jugador = Filtro!.Jugador ;

                Accion = Enumerables.Ventanas.Listas;
                var task = this.iPresentacion!.Porjugador(Filtro!);
                task.Wait();
                Lista = task.Result;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new JugadoresLanzamientos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtGuardar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                Task<JugadoresLanzamientos>? task = null;
                if (Actual!.id == 0)
                    task = this.iPresentacion!.Guardar(Actual!)!;
                else
                    task = this.iPresentacion!.Modificar(Actual!)!;
                task.Wait();
                Actual = task.Result;
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            try
            {
                var task = this.iPresentacion!.Borrar(Actual!);
                Actual = task.Result;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IActionResult OnPostBtExportarExcel()
        {
            try
            {

                OnPostBtRefrescar();

                if (Lista == null || !Lista.Any())
                {
                    ViewData["Error"] = "No hay datos para exportar.";
                    return Page();
                }

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("JugadoresLanzamientos");


                worksheet.Cell(1, 1).Value = "jugador Id";
                worksheet.Cell(1, 2).Value = "Jugador";
                worksheet.Cell(1, 3).Value = "lanzamiento Id";
                worksheet.Cell(1, 3).Value = "Lanzamiento";
                worksheet.Cell(1, 3).Value = "Pino derribado";
                worksheet.Cell(1, 3).Value = "Puntaje obtenido";


                for (int i = 0; i < Lista.Count; i++)
                {
                    var JugadoresLanzamientos = Lista[i];
                    worksheet.Cell(i + 2, 1).Value = JugadoresLanzamientos.jugadorId;
                    worksheet.Cell(i + 2, 2).Value = JugadoresLanzamientos.Jugador?.nombre ?? "Sin nombre";
                    worksheet.Cell(i + 2, 3).Value = JugadoresLanzamientos.lanzamientoId;
                    worksheet.Cell(i + 2, 3).Value = JugadoresLanzamientos.Lanzamiento?.nombre ?? "Sin nombre";
                    worksheet.Cell(i + 2, 3).Value = JugadoresLanzamientos.pino_derribado;
                    worksheet.Cell(i + 2, 3).Value = JugadoresLanzamientos.puntaje_obtenido;

                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var nombreArchivo = $"JugadoresLanzamientos_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    nombreArchivo);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }
    }
}