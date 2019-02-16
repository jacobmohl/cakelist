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
            services.AddScoped<ICakelistService, CakelistService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICakeRequestRepository, CakeRequestRepository>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();

            // Registeres Swagger document generation
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Cakelist API", Version = "v1" });
            });

            // Registers health checks services
            services.AddHealthChecks()
                .AddDbContextCheck<CakelistContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Use Swashbuckle to genereate OPEN API JSON documentation
            app.UseSwagger();

            // Use Swashbuckle to make a SwaggerUI client with above documentation
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.DisplayOperationId();
            });

            // Use Healtch check on below endpoint
            app.UseHealthChecks("/health");
        }
    }
}
