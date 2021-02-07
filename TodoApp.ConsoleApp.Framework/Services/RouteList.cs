using System.Collections.Generic;

namespace TodoApp.ConsoleApp.Framework.Services
{
    internal class RouteList
    {
        private const int MAX_CAPACITY = 10;

        private readonly LinkedList<View> List = new LinkedList<View>();

        public int MaxCapacity { get; }

        public RouteList() : this(MAX_CAPACITY) { }

        public RouteList(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
        }

        public void Clear()
        {
            List.Clear();
        }

        public void Push(View v)
        {
            if (List.Count >= MaxCapacity)
            {
                List.RemoveFirst();
            }

            List.AddLast(v);
        }

        public View Pop()
        {
            var view = List.Last.Value;
            List.RemoveLast();
            return view;
        }

        public View Peek()
        {
            return List.Last.Value;
        }
    }
}
