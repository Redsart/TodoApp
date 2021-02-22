using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.ConsoleApp.Framework.Services
{
    public class Home
    {
        public View View { get; }

        public Home(View homeView)
        {
            View = homeView;
        }
    }
}
