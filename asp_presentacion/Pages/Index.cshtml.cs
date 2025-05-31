using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // <<--- AÑADE ESTE USING para SessionExtensions

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        public bool EstaLogueado = false; // El PDF usa esta variable, la mantenemos
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contrasena { get; set; }

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Usuario"); //
            if (!String.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true; //
                // Ya no es necesario el 'return;' aquí si el método es void
            }
            else
            {
                EstaLogueado = false; // Aseguramos que esté false si no hay sesión
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty; //
                Contrasena = string.Empty; //
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); //
            }
        }

        // --- MÉTODO MODIFICADO ---
        public IActionResult OnPostBtEnter() // Cambiado a IActionResult para permitir redirección
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Contrasena)) // Tu validación original para campos vacíos
                {
                    ViewData["MensajeErrorLogin"] = "Debe ingresar usuario y contraseña.";
                    OnPostBtClean();
                    return Page(); // Quédate en la página actual para mostrar el error
                }

                // Lógica de autenticación (admin.123)
                if (Email.ToLower() == "admin" && Contrasena == "123") // Convertimos Email a minúsculas para ser más flexible
                {
                    HttpContext.Session.SetString("Usuario", Email); //
                    HttpContext.Session.SetString("UserRole", "Admin"); // <<--- NUEVO: Guardamos el Rol
                    EstaLogueado = true; //
                                         // ViewData["Logged"] = true; // Esta línea no es tan necesaria si usas EstaLogueado o rediriges
                                         // OnPostBtClean(); // Limpiar campos después de un login exitoso es opcional,
                                         // usualmente se redirige y la página se recarga.

                    // Redirigir a una página principal o dashboard después del login
                    return RedirectToPage("/Index"); // O "/Ventanas/Bolas" o una página de bienvenida
                }

                else if (Email.ToLower() == "cliente" && Contrasena == "cliente123")
                {
                    HttpContext.Session.SetString("Usuario", Email);
                    HttpContext.Session.SetString("UserRole", "Client"); // <<--- NUEVO: Guardamos el Rol "Client"
                     EstaLogueado = true;
                     return RedirectToPage("/Index"); // O a la página del cliente
                 }
                else
                {
                    ViewData["MensajeErrorLogin"] = "Usuario o contraseña incorrectos.";
                    OnPostBtClean(); // Limpia los campos si el login falla
                    EstaLogueado = false;
                    return Page(); // Quédate en la página actual para mostrar el error
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); //
                return Page(); // Quédate en la página actual
            }
        }

        // --- MÉTODO MODIFICADO ---
        public IActionResult OnPostBtClose() // Cambiado a IActionResult
        {
            try
            {
                HttpContext.Session.Clear(); // Esto borra "Usuario", "UserRole" y cualquier otra cosa en sesión
                EstaLogueado = false; //
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); //
            }
            return RedirectToPage("/Index"); // Redirige a la página de Index (login) [inspirado por HttpContext.Response.Redirect("/")]
        }
    }
}