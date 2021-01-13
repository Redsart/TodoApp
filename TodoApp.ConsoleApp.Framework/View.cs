using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.ConsoleApp.Framework
{
    public abstract class View<T> where T: ViewModel
    {
        protected Router Router;

        public T ViewModel { get; }

        public View(T vm)
        {
            ViewModel = vm;
        }

        abstract public void Draw();
    }
}
