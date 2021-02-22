using Microsoft.Extensions.DependencyInjection;
using System;
using TodoApp.ConsoleApp.Framework.Commands;
using Microsoft.Extensions.Logging;

namespace TodoApp.ConsoleApp.Framework.Services
{
    public class Renderer
    {
        private readonly ILogger<Renderer> Logger;
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

        public Renderer(ILogger<Renderer> logger, IServiceProvider serviceProvider, Router router)
        {
            Logger = logger;
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

        private void RenderView()
        {
            Logger.LogInformation("Render View: {0}", View.GetType().FullName);
            Logger.LogInformation("    with VM: {0}", View.Ds.GetType().FullName);

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

        internal void Render(View v)
        {
            View = v;
            RenderView();
        }

        internal void Refresh()
        {
            RenderView();
        }

        internal void Start()
        {
            Router.RouteChanged += OnRouteChange;
        }

        internal void Stop()
        {
            Router.RouteChanged -= OnRouteChange;
            
            if (_view != null && _view.Ds != null)
            {
                _view.Ds.PropertyChanged -= OnVmChange;
            }
        }
    }
}
