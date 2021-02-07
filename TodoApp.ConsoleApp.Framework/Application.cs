using Microsoft.Extensions.DependencyInjection;
using TodoApp.ConsoleApp.Framework.Services;

namespace TodoApp.ConsoleApp.Framework
{
    public class Application
    {
        private readonly Router Router;
        private readonly Renderer Renderer;

        public Application(Router router, Renderer renderer)
        {
            Router = router;
            Renderer = renderer;
        }

        public void Start<TView>() 
            where TView : View
        {
            Renderer.Start();
            Router.Start<TView>();
        }

        public void Start<TView, TProps>(TProps props)
            where TView : View
            where TProps : IProps
        {
            Renderer.Start();
            Router.Start<TView, TProps>(props);
        }
    }
}
