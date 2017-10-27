using ConsoleTableToProps.Models.ConfigModels;
using ConsoleTableToProps.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleTableToProps
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("Settings").Bind(appSettings);

            DataTypeMappingService dataTypeMappingService = new DataTypeMappingService();

            dataTypeMappingService.GenerateTxtPocoOutput(appSettings);

            Console.ReadKey();
        }
    }
}
