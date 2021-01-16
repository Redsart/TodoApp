using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Test.ViewModels
{
    public class Todo: ViewModel
    {
        public int Id = 1;
        public string Name = "My todo";

        public void Update(string name)
        {
            Name = name;

            NotifyPropertyChange();
        }
    }
}
