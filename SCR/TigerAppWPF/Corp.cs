using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerAppWPF
{
    public class Corp : Title
    {
        private string dateEmit;
        private string dateBack;
        private string name;
        public Corp(string _isin, int _qtty, string dateEmit, string dateBack, string name)
            : base(_isin,_qtty)
        {
            this.dateEmit = dateEmit;
            this.dateBack = dateBack;
            this.name = name;
        }

        public Corp(string _isin, int _qtty, string country, string currency, string name, double value)
            : base(_isin, _qtty, country, currency, name, value)
        {}

        public override string ToString()
        {
            return name + " DateEmit : " + dateEmit + " DateBack: " + dateBack;
        }
    }
}
