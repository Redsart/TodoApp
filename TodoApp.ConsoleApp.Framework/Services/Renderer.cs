using Microsoft.Extensions.DependencyInjection;
using System;
using TodoApp.ConsoleApp.Framework.Commands;

namespace TodoApp.ConsoleApp.Framework.Services
{
    public class Renderer
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly Router Router;

        private View _view;

        private View View
        {
            get => _view;
            set
            {
                if (_view != null && _view.Ds != null)
                {
                    _view.Ds.PropertyChanged -= OnVmChange;
                }

                _view = value;

                if (_view != null && _view.Ds != null)
                {
                    _view.Ds.PropertyChanged += OnVmChange;
                }
            }
        }

        public Renderer(IServiceProvider serviceProvider, Router router)
        {
            ServiceProvider = serviceProvider;
            Router = router;
        }

        private void OnRouteChange(Router r, RouteChangedEventArgs args)
        {
            Render(args.View);
        }

        private void OnVmChange(ViewModel vm, EventArgs args)
        {
            Refresh();
        }

        public void Start()
        {
            Router.RouteChanged += OnRouteChange;
        }

        private void RenderView()
        {
            Console.Clear();

            var cmds = ServiceProvider.GetRequiredService<CommandList>();
            View.Commands = cmds;
            View.Commands.Reset();
            View.Render();
            View.SetupCommands();

            RenderCommands();
        }

        private void RenderCommands()
        {
            if (!View.Commands.Available)
            {
                return;
            }

            Console.WriteLine();
            Console.Write(View.Commands);

            bool success = false;
            while (!success)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                View.Commands.TryRun(input, out success);
                if (!success)
                {
                    Console.WriteLine(View.Commands.InvalidMessage);
                }
            }
        }

        public void Render(View v)
        {
            View = v;
            RenderView();
        }

        public void Refresh()
        {
            RenderView();
        }
    }
}
