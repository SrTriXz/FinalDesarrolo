using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
namespace asp_presentacion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static IConfiguration? Configuration { set; get; }
        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            // Presentaciones
            services.AddScoped<IBolasPresentacion, BolasPresentacion>();
            services.AddScoped<IJugadoresPresentacion, JugadoresPresentacion>();
            services.AddScoped<IJugadoresBolasPresentacion, JugadoresBolasPresentacion>();
            services.AddScoped<IJugadoresLanzamientosPresentacion, JugadoresLanzamientosPresentacion>();
            services.AddScoped<IJugadoresPartidasPresentacion, JugadoresPartidasPresentacion>();
            services.AddScoped<ILanzamientosPresentacion, LanzamientosPresentacion>();
            services.AddScoped<IPartidasPresentacion, PartidasPresentacion>();
            services.AddScoped<IPistasPresentacion, PistasPresentacion>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();
            app.Run();
        }
    }
}
