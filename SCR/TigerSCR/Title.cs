using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerSCR
{
    public abstract class Title
    {
        private string isin;
        private int qtty;
        private string country;
        private string name;
        private double value;
        private string currency;

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

        public Title(string _isin, int _qtty)
        {
            this.isin = _isin;
            this.qtty = _qtty;
        }

        public Title(string _isin, int _qtty, string country, string currency, string name, double value)
        {
            this.isin = _isin;
            this.qtty = _qtty;
            this.country = country;
            this.currency = currency;
            this.name = name;
            this.value = value;
        }


        public string Isin
        {
            get{return this.isin;}
        }

        public void setQtty(int qtty)
        {
            this.qtty = qtty;
        }

        override public string ToString()
        {
            return isin + " : Pays : " + country + " Nom : " + name + " = " + value+" "+ currency;
        }
    }
}
