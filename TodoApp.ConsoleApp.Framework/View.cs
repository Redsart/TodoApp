namespace TodoApp.ConsoleApp.Framework
{
    public abstract class View
    {
        internal ViewModel Ds { get;  }

        public View() : this(null)
        { }

        internal View(ViewModel ds)
        {
            Ds = ds;
        }

        abstract public void Draw();
    }

    public abstract class View<TVm> : View
        where TVm : ViewModel
    {
        public TVm DataSource { get; set; }

        public View(TVm vm)
            : base(vm)
        {
            DataSource = vm;
        }
    }
}
