using asp_servicios.Controllers;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicios
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
            services.Configure<KestrelServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            // Repositorios
            services.AddScoped<IConexion, Conexion>();
            // Aplicaciones
            services.AddScoped<IBolasAplicacion, BolasAplicacion>();
            services.AddScoped<IJugadoresAplicacion, JugadoresAplicacion>();
            services.AddScoped<IJugadoresBolasAplicacion, JugadoresBolasAplicacion>();
            services.AddScoped<IJugadoresLanzamientosAplicacion, JugadoresLanzamientosAplicacion>();
            services.AddScoped<IJugadoresPartidasAplicacion, JugadoresPartidasAplicacion>();
            services.AddScoped<ILanzamientosAplicacion, LanzamientosAplicacion>();
            services.AddScoped<IPartidasAplicacion, PartidasAplicacion>();
            services.AddScoped<IPistasAplicacion, PistasAplicacion>();
            // Controladores
            services.AddScoped<TokenController, TokenController>();

            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors();
        }
    }
}