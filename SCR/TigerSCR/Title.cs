using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
    public enum Security { equity, corp }
    public abstract class Title
    {
        private string isin;
        private Security security;
        private string countrySecurity;
        private string country;
        private string name;
        private double value;
        private string currency;
        private bool ocde;
        private bool ue;

        #region donnees
        List<string> l_ocde = new List<string>{
            "AUSTRALIA",
            "AUSTRIA",
            "BELGIUM",
            "CANADA",
            "CHILE",
            "CZECH REPUBLIC",
            "DENMARK",
            "ESTONIA",
            "FINLAND",
            "FRANCE",
            "GERMANY",
            "GREECE",
            "HUNGARY",
            "ICELAND",
            "IRLAND",
            "ISRAEL",
            "ITALY",
            "JAPAN",
            "KOREA",
            "LUXEMBOURG",
            "MEXICO",
            "NETHERLANDS",
            "NEW ZEALAND",
            "NORWAY",
            "POLAND",
            "PORTUGAL",
            "SLOVAKIA",
            "SLOVENIA",
            "SPAIN",
            "SWEDEN",
            "SWITZERLAND",
            "TURKEY",
            "UNITED KINGDOM",
            "UNITED STATES"};
        List<string> l_ue = new List<string>{
            "AUSTRIA",
            "BELGIUM",
            "BULGARIA",
            "CROATIA",
            "CYPRUS",
            "CZECH REPUBLIC",
            "ESTONIA",
            "FINLAND",
            "FRANCE",
            "GERMANY",
            "GREECE",
            "HUNGARY",
            "IRLAND",
            "ITALY",
            "LATVIA",
            "LITHUANIA",
            "LUXEMBOURG",
            "MALTA",
            "NETHERLANDS",
            "POLAND",
            "PORTUGAl",
            "ROMANIA",
            "SLOVAKIA",
            "SLOVENIA",
            "SPAIN",
            "SWEDEN",
            "UNITED KINDGDOM"};
            #endregion

        public Title(string _isin, string _countrySecurity ,Security _security)
        {
            this.isin = _isin;
            this.countrySecurity = _countrySecurity;
            this.security = _security;
        }

        public Title(string _isin, string _countrySecurity, Security _security, string country, string currency, string name, double value)
        {
            this.isin = _isin;
            this.countrySecurity = _countrySecurity;
            this.security = _security;
            this.country = country;
            this.currency = currency;
            this.name = name;
            this.value = value;
        }

        public Title(Title _t)
        {
            this.isin = _t.isin;
            this.countrySecurity = _t.countrySecurity;
            this.security = _t.security;
            this.country = _t.country;
            this.currency = _t.currency;
            this.name = _t.name;
            this.value = _t.value;
        }

        public Security GetSecurity
        {
            get { return security; }
        }

        override public string ToString()
        {
            return isin + " : Pays : " + country + " Nom : " + name + " = " + value+" "+ currency;
        }

        public string ToSecurities()
        {
            return isin + " " + countrySecurity;
        }
    }
}
