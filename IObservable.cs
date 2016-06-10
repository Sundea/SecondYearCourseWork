using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public delegate void NotifyObserver();

    interface IObservable
    {
        event NotifyObserver Send;
        void Notify();
    }
}
