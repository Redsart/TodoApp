using System;

namespace TodoApp.ConsoleApp.Framework
{
    internal delegate void PropertyChangedHanlder(ViewModel vm, EventArgs e);

    public abstract class ViewModel
    {
        private event PropertyChangedHanlder propertyChanged;

        internal event PropertyChangedHanlder PropertyChanged
        {
            add
            {
                propertyChanged += value;
            }

            remove
            {
                propertyChanged -= value;
            }
        }

        protected void NotifyPropertyChanged()
        {
            propertyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
