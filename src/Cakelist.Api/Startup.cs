using System.IO;
using Cakelist.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cakelist.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {

            services.AddApplicationInsightsTelemetry();

            // Setup EF Core context - InMemory
            services.AddDbContext<CakelistContext>(options => options.UseInMemoryDatabase("CakelistDB"));

            // Setup EF Core context - SQL
            //services.AddDbContext<CakelistContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CakelistDatabase"), x => x.MigrationsAssembly("Cakelist.Infrastructure")));


            // Register MvcCore and some extra parts (for APIs)
            services.AddMvcCore().AddApiExplorer().AddDataAnnotations();

            Cakelist.Infrastructure.Startup.SetupDependencyInjection(services);

            // Registeres Swagger document generation
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Title = "Cakelist API",
                    Description = "Cakelist management API.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact {
                        Name = "Jacob Møhl",
                        Email = string.Empty,
                        Url = new System.Uri("https://jacobmohl.dk")
                    },
                    Version = "v1"
                });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Cakelist.Api.xml");
                c.IncludeXmlComments(filePath);
            });


            // Registers health checks services
            services.AddHealthChecks()
                // Entity Framework health check
                .AddDbContextCheck<CakelistContext>();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                // Show developer exception (YSOD)
                app.UseDeveloperExceptionPage();
            }
            else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Force HTTPS
            app.UseHttpsRedirection();

            // Use MVC in the request pipeline
            app.UseRouting();

            // Use Swashbuckle to genereate OPEN API JSON documentation
            app.UseSwagger();

            // Use Swashbuckle to make a SwaggerUI client with above documentation
            app.UseSwaggerUI(c => {
                // Setup SwaggerUI to read the generated swagger file
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");

                // Show Operation Ids in the Swagger UI
                c.DisplayOperationId();
            });


            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
