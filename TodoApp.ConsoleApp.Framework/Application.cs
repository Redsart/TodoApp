using Microsoft.Extensions.DependencyInjection;
using System;

namespace TodoApp.ConsoleApp.Framework
{
    public class Application
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly Router Router;
        private readonly Renderer Renderer;

        public Application(IServiceProvider serviceProvider, Router router, Renderer renderer)
        {
            ServiceProvider = serviceProvider;
            Router = router;
            Renderer = renderer;
        }

        public void Start<T>() where T : View
        {
            Renderer.Start();
            Router.Start<T>();
        }

        public static void AddServices(IServiceCollection services)
        {
            services
                .AddScoped<ViewModel>((s) => null)
                .AddScoped<Router>()
                .AddScoped<Renderer>()
                .AddScoped<Application>();
        }
    }
}
