using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApp.Repositories.Interfaces;
using TodoApp.Services;
using Xml = TodoApp.Repositories.XmlRepository;
using VM = TodoApp.ConsoleApp.Test.ViewModels;
using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            RunApp(host.Services, args);

            host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    services
                        .AddSingleton<ITodoRepository, Xml.TodoRepository>()
                        .AddSingleton<ITodoService, TodoService>()
                        .AddScoped<VM.Todo>()
                        .AddScoped<Views.Home>()
                        .AddScoped<Views.Todo>();

                    Application.AddServices(services);
                });
        }

        static void RunApp(IServiceProvider services, string[] args)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var app = provider.GetRequiredService<Application>();
            app.Start<Views.Home>();
        }
    }
}
