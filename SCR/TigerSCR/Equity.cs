using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
    public class Equity : Title
    {
        public Equity(string _isin, int _qtty)
            : base(_isin, _qtty, 1) //nominale = 1
        {}

        public Equity(string _isin, int _qtty, string country, string currency, string name, double value)
            : base(_isin, _qtty,1, country, currency, name, value) //nominale = 1
        {}

        override public string ToCSV()
        {
            return "Equity;" +base.ToCSV();
        }

        /*public Equity(Title _t)
            : base(_t)
        {}*/
    }
}
