using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Framework.Examples.Props
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
