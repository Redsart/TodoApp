using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.ConsoleApp.Framework
{
    public abstract class ViewModel
    {
        internal delegate void PropertyChangeHanlder(ViewModel vm, EventArgs e);
        internal event PropertyChangeHanlder PropertyChange;

        protected void NotifyPropertyChange()
        {
            PropertyChange(this, EventArgs.Empty);
        }
    }
}
