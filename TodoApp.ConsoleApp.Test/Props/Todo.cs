using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Test.Props
{
    public class Todo : IProps
    {
        public int? Id;

        public Todo()
        {
            Id = null;
        }

        public Todo(int id)
        {
            Id = id;
        }
    }
}
