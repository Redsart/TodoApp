using System;

namespace TodoApp.ConsoleApp.Framework
{
    public abstract class ViewModel
    {
        public delegate void PropertyChangedHanlder(ViewModel vm, EventArgs e);
        public event PropertyChangedHanlder PropertyChanged;

        protected void NotifyPropertyChanged()
        {
            PropertyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
