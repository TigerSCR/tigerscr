using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerAppWPF
{
    public abstract class Title
    {
        private string isin;
        private int qtty;
        private string country;
        private string name;
        private double value;
        private string currency;
        private bool oecd;
        private bool eu;
        private bool strategic = false;


        #region donnees
        private List<string> l_oecd = new List<string>{
            "AU",
            "AT",
            "BE",
            "CA",
            "CL",
            "CZ",
            "DK",
            "EE",
            "FI",
            "FR",
            "DE",
            "GR",
            "HU",
            "IS",
            "IE",
            "IL",
            "IT",
            "JP",
            "KR",
            "LU",
            "MX",
            "NL",
            "NZ",
            "NO",
            "PL",
            "PT",
            "SK",
            "SI",
            "ES",
            "SE",
            "CH",
            "TR",
            "GB",
            "US"};
        private List<string> l_eu = new List<string>{
            "AU",
            "BE",
            "BG",
            "HR",
            "CY",
            "CZ",
            "EE",
            "FI",
            "FR",
            "DE",
            "GR",
            "HU",
            "IE",
            "IT",
            "LV",
            "LT",
            "LU",
            "MT",
            "NL",
            "PL",
            "PT",
            "RO",
            "SK",
            "SI",
            "ES",
            "SE",
            "GB"};
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
            if (l_oecd.Contains(this.country))
                this.oecd = true;
            if (l_eu.Contains(this.country))
                this.eu = true;
        }

        #region Accesseurs
        public string Isin
        {
            get{return this.isin;}
        }

        public int Qtty
        {
            get { return this.qtty; }
            set { this.qtty = value; }
        }

        public double Value
        { get { return this.value; } }
        
        public bool Oecd
        { get { return this.oecd; } }
        public bool Eu
        { get { return this.eu; } }
        public bool Strategic
        { get { return this.strategic; } }
        #endregion

        override public string ToString()
        {
            return isin + " : Pays : " + country + " Nom : " + name + " = "+qtty+ "(" + value+" "+ currency+")";
        }
    }
}
