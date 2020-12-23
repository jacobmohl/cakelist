using System;
using Cakelist.Business.Interfaces;
using Cakelist.Business.Services;
using Cakelist.Infrastructure.Data;
using Cakelist.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Setup EF Core context - InMemory
            services.AddDbContext<CakelistContext>(options => options.UseInMemoryDatabase("CakelistDB"));

            // Setup EF Core context - SQL
            //services.AddDbContext<CakelistContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CakelistDatabase"), x => x.MigrationsAssembly("Cakelist.Infrastructure")));

            // Dependency Injection
            services.AddTransient<ICakelistService, CakelistService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICakeRequestRepository, CakeRequestRepository>();
            services.AddTransient<IUserNotificationService, UserNotificationService>();

            return services;
        }
    }
}