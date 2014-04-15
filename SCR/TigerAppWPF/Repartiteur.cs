using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TigerAppWPF
{
    class Repartiteur
    {
        private static Repartiteur engine;

        //modules list
        private ModuleEquity modEqu;

        private Repartiteur()
        { }

        public static Repartiteur getEngine()
        {
            if (engine == null)
                engine = new Repartiteur();
            return engine;
        }

#region Checkers
        private static bool inEquityModule(Title t)
        {
            return t is Equity;
        }
#endregion

        public void equity(List<Title> portfolio)
        {
            List<Title> temp=new List<Title>();
            foreach(Title t in portfolio)
            {
                if (inEquityModule(t))
                    temp.Add(t);
            }
            this.modEqu = new ModuleEquity(temp);
            MessageBox.Show(this.modEqu.ToString());
        }
    }
}
