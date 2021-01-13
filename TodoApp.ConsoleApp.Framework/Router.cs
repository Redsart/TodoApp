using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.ConsoleApp.Framework
{
    public class Router
    {
        private readonly Stack<View<ViewModel>> History = new Stack<View<ViewModel>>();
        private View<ViewModel> Active;
        private readonly Stack<View<ViewModel>> Future = new Stack<View<ViewModel>>();

        internal delegate void RouteChangeHanlder(Router r, RouteChangeEventArgs args);
        internal event RouteChangeHanlder RouteChange;

        private void NotifyRouteChange()
        {
            RouteChange(this, new RouteChangeEventArgs(Active));
        }

        public void Open(View<ViewModel> v)
        {
            History.Push(Active);
            Active = v;
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
        public View<ViewModel> View { get; }

        public RouteChangeEventArgs(View<ViewModel> v)
        {
            View = v;
        }
    }
}
