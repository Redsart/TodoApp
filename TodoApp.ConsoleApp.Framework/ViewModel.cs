using System;

namespace TodoApp.ConsoleApp.Framework
{    public abstract class ViewModel
    {
        public delegate void PropertyChangeHanlder(ViewModel vm, EventArgs e);
        public event PropertyChangeHanlder PropertyChange;

        protected void NotifyPropertyChange()
        {
            PropertyChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
