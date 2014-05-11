using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
    public class Corp : Title
    {
        private string dateEmit;
        private string dateBack;
        private string name;
        public Corp(string _isin, int _qtty, int _nominale, string dateEmit, string dateBack, string name)
            : base(_isin,_qtty, _nominale)
        {
            this.dateEmit = dateEmit;
            this.dateBack = dateBack;
            this.name = name;
        }

        public Corp(string _isin, int _qtty, int _nominale, string country, string currency, string name, double value)
            : base(_isin, _qtty, _nominale, country, currency, name, value)
        {}

        public override string ToString()
        {
            return name + " DateEmit : " + dateEmit + " DateBack: " + dateBack;
        }

        override public string ToCSV()
        {
            return "Corp;" + base.ToCSV() + ";" + dateEmit + ";" + dateBack + ";" + name;
        }
    }
}
