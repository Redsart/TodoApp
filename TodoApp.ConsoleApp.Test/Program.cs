using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApp.Repositories.Interfaces;
using TodoApp.Services;
using Xml = TodoApp.Repositories.XmlRepository;
using VM = TodoApp.ConsoleApp.Test.ViewModels;
using P = TodoApp.ConsoleApp.Test.Props;
using Cmd = TodoApp.ConsoleApp.Test.Commands;
using TodoApp.ConsoleApp.Framework;
using System.Threading.Tasks;

namespace TodoApp.ConsoleApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            RunApp(host.Services, args).Wait();

            host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    services
                        // Views
                        .AddView<Views.Home>()
                        .AddView<Views.TodoDetails>()
                        .AddView<Views.Goodbye>()
                        // Props
                        .AddProps<P.Todo>()
                        // Commands
                        .AddCommand<Cmd.Back>()
                        .AddCommand<Cmd.Exit>()
                        // View Models
                        .AddViewModel<VM.Navigation>()
                        .AddViewModel<VM.Todo>()
                        .AddViewModel<VM.Goodbye>()
                        // Services
                        .AddSingleton<ITodoService, TodoService>()
                        // Repositories
                        .AddSingleton<ITodoRepository, Xml.TodoRepository>()
                        .AddSingleton<Xml.Utils.IXmlContext, Xml.Utils.XmlContext>()
                        .AddAppServices();
                });
        }

        static async Task RunApp(IServiceProvider services, string[] args)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var app = provider.GetRequiredService<Application>();
            await app.RunAsync<Views.Home>();
        }
    }
}
