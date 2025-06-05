using ClosedXML.Excel;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class BolasModel : PageModel
    {
        private IBolasPresentacion? iPresentacion = null;

        public BolasModel(IBolasPresentacion iPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                Filtro = new Bolas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Bolas? Actual { get; set; }
        [BindProperty] public Bolas? Filtro { get; set; }
        [BindProperty] public List<Bolas>? Lista { get; set; }
        [BindProperty] public IFormFile? ArchivoImagen { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/");
                    return;
                }

                Filtro!.nombre = Filtro!.nombre ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = this.iPresentacion!.Pornombre(Filtro!);
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
                Actual = new Bolas();
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

                Task<Bolas>? task = null;
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
                var worksheet = workbook.Worksheets.Add("Bolas");


                worksheet.Cell(1, 1).Value = "Nombre";
                worksheet.Cell(1, 2).Value = "Diámetro";
                worksheet.Cell(1, 3).Value = "Color";
                worksheet.Cell(1, 4).Value = "Peso";

                for (int i = 0; i < Lista.Count; i++)
                {
                    var bola = Lista[i];
                    worksheet.Cell(i + 2, 1).Value = bola.nombre;
                    worksheet.Cell(i + 2, 2).Value = bola.diametro;
                    worksheet.Cell(i + 2, 3).Value = bola.Color;
                    worksheet.Cell(i + 2, 4).Value = bola.peso;
                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var nombreArchivo = $"Bolas_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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