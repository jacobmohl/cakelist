using System;
using System.Collections.Generic;
using System.Text;
using Cakelist.Business.Interfaces;
using Cakelist.Business.Services;
using Cakelist.Infrastructure.Data;
using Cakelist.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cakelist.Infrastructure
{
    public class Startup
    {
        public static void SetupDependencyInjection(IServiceCollection services)
        {
            // Dependency Injection
            services.AddTransient<ICakelistService, CakelistService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICakeRequestRepository, CakeRequestRepository>();
            services.AddTransient<IUserNotificationService, UserNotificationService>();
        }
    }
}
