using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
        public class Govt : Title
        {
            public Govt(string _isin, int _qtty)
                : base(_isin, _qtty)
            { }

            public Govt(string _isin, int _qtty, string country, string currency, string name, double value)
                : base(_isin, _qtty, country, currency, name, value)
            { }

            override public string ToCSV()
            {
                return "Govt;" + base.ToCSV();
            }

            /*public Govt(Title _t)
                : base(_t)
            {}*/
        }
}
