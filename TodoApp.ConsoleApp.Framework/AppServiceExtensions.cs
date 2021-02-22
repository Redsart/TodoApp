using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApp.ConsoleApp.Framework.Services;
using Microsoft.Extensions.Logging;

namespace TodoApp.ConsoleApp.Framework
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddView<T>(this IServiceCollection services, bool homeView = false)
            where T : View
        {
            if (homeView)
            {
                services.AddSingleton(s => new Home(s.GetRequiredService<T>()));
            }

            return services.AddTransient<T>();
        }

        public static IServiceCollection AddProps<T>(this IServiceCollection services)
            where T : class, IProps
        {
            return services.AddTransient(s => (T)s.GetRequiredService<Props<IProps>>().Data);
        }

        public static IServiceCollection AddCommand<T>(this IServiceCollection services)
            where T : Commands.Command
        {
            return services.AddTransient<T>();
        }

        public static IServiceCollection AddViewModel<T>(this IServiceCollection services)
            where T : ViewModel
        {
            return services.AddTransient<T>();
        }

        public static IHostBuilder UseTodoFramework(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureLogging((logging) =>
                {
                    logging
                        .ClearProviders()
                        .AddDebug();
                }).ConfigureServices(services =>
                {
                    services
                        .AddTransient<Commands.CommandList>()
                        .AddScoped<ViewModel>((_) => null)
                        .AddScoped<Props<IProps>>()
                        .AddScoped<Router>()
                        .AddScoped<Renderer>()
                        .AddHostedService<Application>();
                });
        }
    }
}
