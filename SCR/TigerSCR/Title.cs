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

        #region donnees
        List<string> oecd = new List<string>{
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
        List<string> ue = new List<string>{
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
