using System;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.ConsoleApp.Framework.Services
{
    public class Router
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly Props<IProps> Props;

        private View Active;
        private readonly RouteList History = new RouteList();
        private readonly RouteList Future = new RouteList();

        internal delegate void RouteChangedHanlder(Router r, RouteChangedEventArgs args);
        internal event RouteChangedHanlder RouteChanged;

        public Router(IServiceProvider serviceProvider, Props<IProps> props)
        {
            ServiceProvider = serviceProvider;
            Props = props;
        }

        private void NotifyRouteChanged()
        {
            RouteChanged?.Invoke(this, new RouteChangedEventArgs(Active));
        }

        private TView CreateView<TView>()
            where TView : View
        {
            Props.Data = default;

            var view = ServiceProvider.GetRequiredService<TView>();
            return view;
        }

        private TView CreateView<TView, TProps>(TProps props)
            where TView : View
            where TProps : IProps
        {
            Props.Data = props;

            var view = ServiceProvider.GetRequiredService<TView>();
            return view;
        }

        public void Start<TView>()
            where TView : View
        {
            Open<TView>();
        }

        public void Start<TView, TProps>(TProps props)
            where TView : View
            where TProps : IProps
        {
            Open<TView, TProps>(props);
        }

        public void Open<TView>()
            where TView : View
        {
            History.Push(Active);
            Active = CreateView<TView>();
            Future.Clear();

            NotifyRouteChanged();
        }

        public void Open<TView, TProps>(TProps props)
            where TView : View
            where TProps : IProps
        {
            History.Push(Active);
            Active = CreateView<TView, TProps>(props);
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
