using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.ConsoleApp.Framework
{
    public class Renderer
    {
        private Router Router;

        private View<ViewModel> _view;
        private View<ViewModel> View
        {
            get => _view;
            set
            {
                if (_view != null && _view.ViewModel != null)
                {
                    _view.ViewModel.PropertyChange -= OnVmChange;
                }

                _view = value;
                
                if (_view != null && _view.ViewModel != null)
                {
                    _view.ViewModel.PropertyChange += OnVmChange;
                }
            }
        }

        public Renderer(Router router)
        {
            Router = router;
            Router.RouteChange += OnRouteChange;
        }

        private void OnRouteChange(Router r, RouteChangeEventArgs args)
        {
            Render(args.View);
        }

        private void OnVmChange(ViewModel vm, EventArgs args)
        {
            Refresh();
        }

        public void Render(View<ViewModel> v)
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
