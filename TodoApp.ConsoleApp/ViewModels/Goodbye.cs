using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.ConsoleApp.Framework;
using Microsoft.Extensions.Hosting;

namespace TodoApp.ConsoleApp.ViewModels
{
    class GoodBye : ViewModel
    {
        IHostApplicationLifetime AppLifetime;

        public Goodbye(IHostApplicationLifetime appLifetime)
        {
            AppLifetime = appLifetime;
        }

        public void Exit()
        {

            AppLifetime.StopApplication();
        }
    }
}
