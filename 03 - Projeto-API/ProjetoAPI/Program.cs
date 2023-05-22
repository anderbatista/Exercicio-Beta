using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProjetoAPI.Data.Dtos.CategoriaDto;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ProjetoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = GetConfiguration();

            ConfiguraLog(configuration);

            try
            {
                Log.Information("\n##############################\n" +
                                "#### Iniciando ProjetoAPI ####\n" +
                                "##############################\n");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "\n####################\n" +
                              "#### Erro fatal ####\n" +
                              "####################\n");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfiguraLog(IConfigurationRoot configuration)
        {
            Log.Logger = new LoggerConfiguration()

                //.Enrich.FromLogContext()
                //.MinimumLevel.Information()
                //.WriteTo.Async(l => l.Console())
                //.WriteTo.Async(l => l.File("yLogs/log.txt" /* Caminho do arquivo */,
                //    fileSizeLimitBytes: 100000 /* Limita por quantidade por quantidade de Bytes */,
                //    rollOnFileSizeLimit: true /* Cria um novo arquivo quando o limite é atigindo */,
                //    rollingInterval: RollingInterval.Day /* Gera Logs por dia */ ))

                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{ambiente}.json")
                .Build();
            return configuration;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
