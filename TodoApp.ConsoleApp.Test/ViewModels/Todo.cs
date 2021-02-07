using TodoApp.ConsoleApp.Framework.Services;

namespace TodoApp.ConsoleApp.Test.ViewModels
{
    public class Todo : Navigation
    {
        public int Id = 1;
        public string Name = "My todo";

        public Todo(Router router, Props.Todo props)
            : base(router)
        {
            Id = props.Id.HasValue ? props.Id.Value : 0;
        }

        public void Update(string name)
        {
            Name = name;

            NotifyPropertyChanged();
        }
    }
}
