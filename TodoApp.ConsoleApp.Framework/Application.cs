using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.ConsoleApp.Framework.Services;


namespace TodoApp.ConsoleApp.Framework
{
    public class Application: IHostedService
    {
        private readonly Router Router;
        private readonly Renderer Renderer;
        private readonly Home Home;

        public Application(Router router, Renderer renderer, Home home)
        {
            Router = router;
            Renderer = renderer;
            Home = home;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Renderer.Start();
            Router.Start(Home.View);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Renderer.Stop();
            Router.Stop();
            return Task.CompletedTask;
        }
    }
}
