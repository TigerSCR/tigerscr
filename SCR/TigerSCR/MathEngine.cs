using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TigerSCR
{
    class MathEngine
    {
        private static MathEngine engine;

        //DATA
        private double chocEquity=0.39;
        private double chocOtherEquity=0.49;
        private double symAdjust=-0.07;


        private MathEngine()
        { }

        public static MathEngine getEngine()
        {
            if (engine == null)
                engine = new MathEngine();
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
            List<double> temp = new List<double>();
            double somme = 0;
            foreach (Title t in portfolio)
            {
                if (inEquityModule(t))
                {
                    if (!t.Strategic)
                    {
                        if (t.Oecd || t.Eu)
                            temp.Add(t.Value * (this.chocEquity + this.symAdjust) * t.Qtty);
                        else
                            temp.Add(t.Value * (this.chocOtherEquity + this.symAdjust) * t.Qtty);
                    }
                    else
                        temp.Add(t.Value * 0.22 * t.Qtty);
                }
            }
            foreach (double d in temp)
            {
                MessageBox.Show("" + d);
                somme += d;
            }
            MessageBox.Show("Total : " + somme);
        }
    }
}
