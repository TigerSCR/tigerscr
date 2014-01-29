using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
    public class Corp : Title
    {
        public Corp(string _isin, string _countrySecurity)
            : base(_isin, _countrySecurity, Security.corp)
        {}

        public Corp(string _isin, string _countrySecurity, string country, string currency, string name, double value)
            : base(_isin, _countrySecurity, Security.corp, country, currency, name, value)
        {}

        public Corp(Title _t)
            : base(_t)
        {}
    }
}
