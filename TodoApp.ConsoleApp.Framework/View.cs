using System;

namespace TodoApp.ConsoleApp.Framework
{

    public abstract class View
    {
        protected Renderer Renderer;
        protected Router Router;

        public View(Renderer renderer, Router router)
        {
            Renderer = renderer;
            Router = router;
        }

        abstract public void Draw();
    }

    public abstract class View<T> : View where T : ViewModel
    {
        public T ViewModel { get; }

        public View(Renderer renderer, Router router, T vm): base(renderer, router)
        {
            ViewModel = vm;
            if (vm != null)
            {
                vm.PropertyChange += OnVmChange;
            }
        }

        private void OnVmChange(ViewModel vm, EventArgs args)
        {
            Renderer.Refresh();
        }
    }
}
