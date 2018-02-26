using ConsoleTableToProps.Models.ConfigModels;
using ConsoleTableToProps.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleTableToProps
{
    internal class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        private static void Main()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var appSettings = new AppSettings();
            Configuration.GetSection("Settings").Bind(appSettings);

            var dataTypeMappingService = new DataTypeMappingService();

            dataTypeMappingService.GenerateTxtPocoOutput(appSettings);

            Console.ReadKey();
        }
    }
}