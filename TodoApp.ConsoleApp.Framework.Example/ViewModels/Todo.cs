using P = TodoApp.ConsoleApp.Framework.Examples.Props;

namespace TodoApp.ConsoleApp.Framework.Examples.ViewModels
{
    public class Todo: ViewModel
    {
        public int Id = 1;
        public string Name = "My todo";

        public readonly Navigation Nav;

        public Todo(Navigation nav, P.Todo props)
        {
            Nav = nav;
            Id = props?.Id ?? 0;
        }

        public void Update(string name)
        {
            Name = name;

            NotifyPropertyChanged();
        }
    }
}
