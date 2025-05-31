using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // <<--- A�ADE ESTE USING para SessionExtensions

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
                // Ya no es necesario el 'return;' aqu� si el m�todo es void
            }
            else
            {
                EstaLogueado = false; // Aseguramos que est� false si no hay sesi�n
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

        // --- M�TODO MODIFICADO ---
        public IActionResult OnPostBtEnter() // Cambiado a IActionResult para permitir redirecci�n
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Contrasena)) // Tu validaci�n original para campos vac�os
                {
                    ViewData["MensajeErrorLogin"] = "Debe ingresar usuario y contrase�a.";
                    OnPostBtClean();
                    return Page(); // Qu�date en la p�gina actual para mostrar el error
                }

                // L�gica de autenticaci�n (admin.123)
                if (Email.ToLower() == "admin" && Contrasena == "123") // Convertimos Email a min�sculas para ser m�s flexible
                {
                    HttpContext.Session.SetString("Usuario", Email); //
                    HttpContext.Session.SetString("UserRole", "Admin"); // <<--- NUEVO: Guardamos el Rol
                    EstaLogueado = true; //
                                         // ViewData["Logged"] = true; // Esta l�nea no es tan necesaria si usas EstaLogueado o rediriges
                                         // OnPostBtClean(); // Limpiar campos despu�s de un login exitoso es opcional,
                                         // usualmente se redirige y la p�gina se recarga.

                    // Redirigir a una p�gina principal o dashboard despu�s del login
                    return RedirectToPage("/Index"); // O "/Ventanas/Bolas" o una p�gina de bienvenida
                }

                else if (Email.ToLower() == "cliente" && Contrasena == "cliente123")
                {
                    HttpContext.Session.SetString("Usuario", Email);
                    HttpContext.Session.SetString("UserRole", "Client"); // <<--- NUEVO: Guardamos el Rol "Client"
                     EstaLogueado = true;
                     return RedirectToPage("/Index"); // O a la p�gina del cliente
                 }
                else
                {
                    ViewData["MensajeErrorLogin"] = "Usuario o contrase�a incorrectos.";
                    OnPostBtClean(); // Limpia los campos si el login falla
                    EstaLogueado = false;
                    return Page(); // Qu�date en la p�gina actual para mostrar el error
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); //
                return Page(); // Qu�date en la p�gina actual
            }
        }

        // --- M�TODO MODIFICADO ---
        public IActionResult OnPostBtClose() // Cambiado a IActionResult
        {
            try
            {
                HttpContext.Session.Clear(); // Esto borra "Usuario", "UserRole" y cualquier otra cosa en sesi�n
                EstaLogueado = false; //
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); //
            }
            return RedirectToPage("/Index"); // Redirige a la p�gina de Index (login) [inspirado por HttpContext.Response.Redirect("/")]
        }
    }
}