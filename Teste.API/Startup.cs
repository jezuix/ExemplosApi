using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Teste.API.Models;

namespace Teste.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()), ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Teste de API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetService<ApiContext>();
            AdicionarDadosTeste(context);
        }

        private static void AdicionarDadosTeste(ApiContext context)
        {
            #region Usuário 1
            {
                var usuario = new Usuario(nome: "Dan Ravelli Paz Occa");
                var carteira = new Carteira(usuario.Id, 1010214.21m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }
            #endregion

            #region Usuário 2

            {
                var usuario = new Usuario(nome: "Will Wayne J. Toddy");
                var carteira = new Carteira(usuario.Id, 3123213m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            #region Usuário 3

            {
                var usuario = new Usuario(nome: "Rikka Trinha Phel Fis");
                var carteira = new Carteira(usuario.Id, 2957135.66m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            #region Usuário 4

            {
                var usuario = new Usuario(nome: "Frey Mercury Golden");
                var carteira = new Carteira(usuario.Id, 9999999.89m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            #region Usuário 5

            {
                var usuario = new Usuario(nome: "Fallen Bruno");
                var carteira = new Carteira(usuario.Id, 123321.12m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            #region Usuário 6

            {
                var usuario = new Usuario(nome: "Bill Bars Kgates");
                var carteira = new Carteira(usuario.Id, 122332123.12m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            #region Usuário 7

            {
                var usuario = new Usuario(nome: "Carlitos de Jesus");
                var carteira = new Carteira(usuario.Id, 133547889.12m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            #region Usuário 8

            {
                var usuario = new Usuario(nome: "Nenvini Jasumiu");
                var carteira = new Carteira(usuario.Id, 9999999999.99m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            #region Usuário 9

            {
                var usuario = new Usuario(nome: "Marcola Noharir");
                var carteira = new Carteira(usuario.Id, 874123654m);

                usuario.Carteira = carteira;
                usuario.IdCarteira = carteira.Id;
                context.Usuarios.Add(usuario);

                carteira.Usuario = usuario;
                carteira.IdUsuario = usuario.Id;
                context.Carteiras.Add(carteira);
            }

            #endregion

            context.SaveChanges();
        }
    }
}
