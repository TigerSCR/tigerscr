using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerAppWPF
{
    class Printer : IObserver
    {
        private static Printer _instance;
        public List<Title> portfolioAffichage;

        private Printer()
        {
            this.portfolioAffichage = new List<Title>();
            Engine.getEngine().registerObserver(this); 
        }

        public static Printer getPrinter()
        {
            if (_instance != null)
                return _instance;
            else
                _instance = new Printer();
            return _instance;
        }

        public void notify()
        {
        }
    }
}
