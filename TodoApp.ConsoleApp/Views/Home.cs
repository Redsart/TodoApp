using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.ConsoleApp.Framework;
using TodoApp.ConsoleApp.Components;
using VM = TodoApp.ConsoleApp.ViewModels;

namespace TodoApp.ConsoleApp.Views
{
    public class Home : View<VM.Navigation>
    {
        public Home(VM.Navigation vm)
            :base(vm) 
        { }

        public override void Render()
        {
            Output.WriteTitle("Todo app");

            Output.WriteParagraph("Welcome to your personal task manager. You can use it to create Todo's and track their progress.");
        }

        public override void SetupCommands()
        {
            throw new NotImplementedException();
        }
    }
}
