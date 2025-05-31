using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; 

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        public bool EstaLogueado = false; 
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contrasena { get; set; }

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Usuario"); 
            if (!String.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true; 

            }
            else
            {
                EstaLogueado = false; 
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty; 
                Contrasena = string.Empty; 
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); 
            }
        }


        public IActionResult OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Contrasena)) 
                {
                    ViewData["MensajeErrorLogin"] = "Debe ingresar usuario y contraseña.";
                    OnPostBtClean();
                    return Page(); 
                }

                
                if (Email.ToLower() == "admin" && Contrasena == "123") 
                {
                    HttpContext.Session.SetString("Usuario", Email); 
                    HttpContext.Session.SetString("UserRole", "Admin"); 
                    EstaLogueado = true; 
                                      
                            
                               

                    
                    return RedirectToPage("/Index"); 
                }

                else if (Email.ToLower() == "cliente" && Contrasena == "123")
                {
                    HttpContext.Session.SetString("Usuario", Email);
                    HttpContext.Session.SetString("UserRole", "Client"); 
                     EstaLogueado = true;
                     return RedirectToPage("/Index"); 
                 }
                else
                {
                    ViewData["MensajeErrorLogin"] = "Usuario o contraseña incorrectos.";
                    OnPostBtClean(); 
                    EstaLogueado = false;
                    return Page(); 
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); 
                return Page(); 
            }
        }

        
        public IActionResult OnPostBtClose() 
        {
            try
            {
                HttpContext.Session.Clear(); 
                EstaLogueado = false; 
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!); 
            }
            return RedirectToPage("/Index");
        }
    }
}