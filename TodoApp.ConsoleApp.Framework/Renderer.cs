using System;

namespace TodoApp.ConsoleApp.Framework
{
    public class Renderer
    {
        private Router Router;
        private View View;

        public Renderer(Router router)
        {
            Router = router;
        }

        private void OnRouteChange(Router r, RouteChangeEventArgs args)
        {
            Render(args.View);
        }

        public void Start()
        {
            Router.RouteChange += OnRouteChange;
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
