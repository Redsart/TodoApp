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
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseTodoFramework()
                .ConfigureServices((_, services) =>
                {
                    services
                        // Views
                        .AddView<Views.Home>(true)
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
                        .AddSingleton<Xml.Utils.IXmlContext, Xml.Utils.XmlContext>();
                })
                ;
        }
    }
}
