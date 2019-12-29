using System;
using Cakelist.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Cakelist.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //// Seed the database with test data. 
            //using (var scope = host.Services.CreateScope()) {
            //    var services = scope.ServiceProvider;
            //    try {
            //        var context = services.GetRequiredService<CakelistContext>();
            //        SeedData.Initialize(context);
            //    }
            //    catch (Exception ex) {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }
            //}

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>


        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });

        //Host.CreateDefaultBuilder(args)
        //    .ConfigureAppConfiguration((context, config) =>
        //    {
        //        if (context.HostingEnvironment.IsProduction()) {
        //            var builtConfig = config.Build();

        //            var azureServiceTokenProvider = new AzureServiceTokenProvider();
        //            var keyVaultClient = new KeyVaultClient(
        //                new KeyVaultClient.AuthenticationCallback(
        //                    azureServiceTokenProvider.KeyVaultTokenCallback));

        //            config.AddAzureKeyVault(
        //                $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
        //                keyVaultClient,
        //                new DefaultKeyVaultSecretManager());
        //        }
        //    })
        //    .UseStartup<Startup>();
    }
}
