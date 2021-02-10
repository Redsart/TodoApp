using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TodoApp.ConsoleApp.Framework.Services;

namespace TodoApp.ConsoleApp.Framework
{
    public class Application
    {
        private readonly Router Router;
        private readonly Renderer Renderer;
        private readonly Task AppTask;
        private readonly IHostApplicationLifetime AppLifetime;

        public Application(Router router, Renderer renderer, IHostApplicationLifetime appLifetime)
        {
            Router = router;
            Renderer = renderer;
            AppTask = new Task(() => { });
            AppLifetime = appLifetime;
        }

        private void Start<TView>()
            where TView : View
        {
            Renderer.Start();
            Router.Start<TView>();
        }

        private void Start<TView, TProps>(TProps props)
            where TView : View
            where TProps : IProps
        {
            Renderer.Start();
            Router.Start<TView, TProps>(props);
        }

        private void Stop()
        {
            AppTask.Start();
        }

        public async Task RunAsync<TView>()
            where TView : View
        {
            Start<TView>();

            AppLifetime.ApplicationStopping.Register(Stop);

            await AppTask;
        }
        public async Task RunAsync<TView, TProps>(TProps props)
            where TView : View
            where TProps : IProps
        {
            Start<TView, TProps>(props);

            AppLifetime.ApplicationStopping.Register(Stop);

            await AppTask;
        }
    }
}
