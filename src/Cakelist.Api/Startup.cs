using Cakelist.Business.Interfaces;
using Cakelist.Business.Services;
using Cakelist.Infrastructure.Data;
using Cakelist.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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
        public void ConfigureServices(IServiceCollection services)
        {

            // Setup EF Core context
            services.AddDbContext<CakelistContext>(options => options.UseInMemoryDatabase("CakelistDB"));


            // Register MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Dependency Injection
            services.AddTransient<ICakelistService, CakelistService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICakeRequestRepository, CakeRequestRepository>();
            services.AddTransient<IUserNotificationService, UserNotificationService>();

            // Registeres Swagger document generation
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Title = "Cakelist API",
                    Description = "Cakelist management API.",
                    TermsOfService = "None",
                    Contact = new Contact {
                        Name = "Jacob Møhl",
                        Email = string.Empty,
                        Url = "https://jacobmohl.dk"
                    },
                    Version = "v1"
                });
            });

            // Registers health checks services
            services.AddHealthChecks()
                // Entity Framework health check
                .AddDbContextCheck<CakelistContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();

            // Use Swashbuckle to genereate OPEN API JSON documentation
            app.UseSwagger();

            // Use Swashbuckle to make a SwaggerUI client with above documentation
            app.UseSwaggerUI(c => {
                // Setup SwaggerUI to read the generated swagger file
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");

                // Show Operation Ids in the Swagger UI
                c.DisplayOperationId();
            });

            // Use Healtch check on below endpoint
            app.UseHealthChecks("/health");
        }
    }
}
