using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerAppWPF
{
    interface ISubject
    {
        //private List<IObserver> observerCollection;
        void registerObserver(IObserver observer);
        void unregisterObserver(IObserver observer);
        void notifyObservers();
    }
}
