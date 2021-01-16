using System;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.ConsoleApp.Framework
{
    public class Router
    {
        private readonly IServiceProvider ServiceProvider;

        private readonly RouteList History = new RouteList();
        private View Active;
        private readonly RouteList Future = new RouteList();

        internal delegate void RouteChangeHanlder(Router r, RouteChangeEventArgs args);
        internal event RouteChangeHanlder RouteChange;

        public Router(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private void NotifyRouteChange()
        {
            RouteChange?.Invoke(this, new RouteChangeEventArgs(Active));
        }

        private T CreateView<T>() where T: View
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        public void Start<T>() where T: View
        {
            Open<T>();
        }

        public void Open<T>() where T: View
        {
            History.Push(Active);
            Active = CreateView<T>();
            Future.Clear();

            NotifyRouteChange();
        }

        public void GoTo(int viewCount)
        {
            if (viewCount == 0)
            {
                return;
            }

            while (viewCount < 0)
            {
                Future.Push(Active);
                Active = History.Pop();
            }

            while (viewCount > 0)
            {
                History.Push(Active);
                Active = Future.Pop();
            }

            NotifyRouteChange();
        }
    }

    internal class RouteChangeEventArgs : EventArgs
    {
        public View View { get; }

        public RouteChangeEventArgs(View v)
        {
            View = v;
        }
    }
}
