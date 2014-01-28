using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
    public enum Security {equity,corp}
    class Title
    {
        private string isin;
        private Security security;
        private string countrySecurity;
        private string name;
        private double value;
        private string currency;
        private bool ocde;
        private bool ue;

        public Title(string _isin, Security _security, string _countrySecurity)
        {
            this.isin = _isin;
            this.security = _security;
            this.countrySecurity = _countrySecurity;
        }

        public string ToString()
        {
            return isin + " : " + name + " = " + value;
        }
    }
}
