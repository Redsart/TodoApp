using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.ConsoleApp.Framework
{
    public interface IRouter
    {
        void Open<TView>()
            where TView : View;

        void Open<TView, TProps>(TProps props)
            where TView : View
            where TProps : IProps;

        bool CanGoTo(int count);

        void GoTo(int count);
    }
}
