using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.ConsoleApp.Framework
{
    public class Application
    {
        private readonly Router Router;
        private readonly Renderer Renderer;

        public Application()
        {
            Router = new Router();
            Renderer = new Renderer(Router);
        }

        public void Start(View<ViewModel> homepage)
        {
            Router.Open(homepage);
        }
    }
}
