﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Typin;

namespace thingiverseCLI
{
    internal class Program
    {
        private static async Task Main(string[] args) =>
            await new CliApplicationBuilder()
            .ConfigureLogging(logger => logger.ClearProviders().AddConsole())
            .ConfigureServices(services =>
            {
                services.AddHttpClient<Services.ThingiverseAPI>(c =>
                {
                    c.BaseAddress = new System.Uri(@"https://api.thingiverse.com/");
                    c.DefaultRequestHeaders.Add("Accept", "application/json");
                });
                services.AddSingleton(provider => (IConfiguration)new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .AddUserSecrets("5e4dcb19-a883-4045-874e-2fe2367012c1")
                .Build());
            })
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
    }
}
