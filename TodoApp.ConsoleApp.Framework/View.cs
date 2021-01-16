using System;

namespace TodoApp.ConsoleApp.Framework
{

    public abstract class View
    {
        protected readonly Router Router;
        internal readonly ViewModel Vm;

        public View(Router router, ViewModel vm = null)
        {
            Router = router;
            Vm = vm;
        }

        abstract public void Draw();
    }

    public abstract class View<T> : View where T : ViewModel
    {
        public T ViewModel => Vm as T;

        public View(Router router, T vm) : base(router, vm)
        { }

    }
}
