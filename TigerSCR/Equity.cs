using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
    class Equity : Title
    {
        public Equity(string _isin, string _name="")
            : base(_isin,Security.equity,_name)
        {}
    }
}
