using System;

namespace TodoApp.ConsoleApp.Framework
{
    public class Renderer
    {
        private Router Router;

        private View _view;
        private View View
        {
            get => _view;
            set
            {
                if (_view != null && _view.Vm != null)
                {
                    _view.Vm.PropertyChanged -= OnVmChange;
                }

                _view = value;

                if (_view != null && _view.Vm != null)
                {
                    _view.Vm.PropertyChanged += OnVmChange;
                }
            }
        }

        public Renderer(Router router)
        {
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

        public void Render(View v)
        {
            View = v;
            Console.Clear();
            View.Draw();
        }

        public void Refresh()
        {
            Console.Clear();
            View.Draw();
        }
    }
}
