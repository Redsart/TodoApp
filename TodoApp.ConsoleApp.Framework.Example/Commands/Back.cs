using VM = TodoApp.ConsoleApp.Framework.Examples.ViewModels;

namespace TodoApp.ConsoleApp.Framework.Examples.Commands
{
    public class Back : Command<VM.Navigation>
    {
        public Back()
            : base("Go Back.", "b")
        { }

        protected override bool CanExecute()
        {
            return DataSource.CanGoBack();
        }

        protected override void Execute(string input)
        {
            DataSource.GoBack();
        }
    }
}
