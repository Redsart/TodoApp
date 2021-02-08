﻿using TodoApp.ConsoleApp.Framework.Commands;

namespace TodoApp.ConsoleApp.Framework
{
    public abstract class View
    {
        internal ViewModel Ds { get;  }

        public CommandList Commands { get; }

        public View() : this(null)
        { }

        internal View(ViewModel ds)
        {
            Ds = ds;
            Commands = new CommandList();
        }

        abstract public void Render();

        abstract public void SetupCommands();
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
