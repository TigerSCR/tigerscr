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
        private string name;
        private double value;

        public Title(string _isin, Security _security, string _name="", double _value=0)
        {
            this.isin = _isin;
            this.security = _security;
            this.name = _name;
            this.value = _value;
        }

        public string ToString()
        {
            return isin + " : " + name + " = " + value;
        }
    }
}
