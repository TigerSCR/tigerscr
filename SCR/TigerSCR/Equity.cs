using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloomberg
{
    public class Equity : Title
    {
        public Equity(string _isin, string _countrySecurity)
            : base(_isin, _countrySecurity, Security.equity)
        {}

        public Equity(string _isin, string _countrySecurity, string country, string currency, string name, double value)
            : base(_isin, _countrySecurity, Security.equity, country, currency, name, value)
        {}
    }
}
