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

        internal delegate void RouteChangedHanlder(Router r, RouteChangedEventArgs args);
        internal event RouteChangedHanlder RouteChanged;

        public Router(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private void NotifyRouteChanged()
        {
            RouteChanged?.Invoke(this, new RouteChangedEventArgs(Active));
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

            NotifyRouteChanged();
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

            NotifyRouteChanged();
        }
    }

    internal class RouteChangedEventArgs : EventArgs
    {
        public View View { get; }

        public RouteChangedEventArgs(View v)
        {
            View = v;
        }
    }
}
