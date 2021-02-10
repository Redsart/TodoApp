using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.ConsoleApp.Framework.Services;

namespace TodoApp.ConsoleApp.Framework
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddView<T>(this IServiceCollection services)
            where T : View
        {
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

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            return services
                .AddTransient<Commands.CommandList>()
                .AddScoped<ViewModel>((_) => null)
                .AddScoped<Router>()
                .AddScoped<Renderer>()
                .AddScoped<Application>()
                .AddScoped<Props<IProps>>();
        }
    }
}
