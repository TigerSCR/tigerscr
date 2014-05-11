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
        private int nominale;
        private string country;
        private string name;
        private double value;
        private string currency;
        private bool oecd = false;
        private bool eu= false;
        private bool strategic = false;
        private bool volume_valide;
        private DataConfig config;

        public Title(string _isin, int _qtty, int _nominale)
        {
            this.isin = _isin;
            this.qtty = _qtty;
            this.nominale = _nominale;
            VolumeValide();
        }

        public Title(string _isin, int _qtty, int _nominale, string country, string currency, string name, double value)
        {
            this.isin = _isin;
            this.qtty = _qtty;
            this.nominale = _nominale;
            this.country = country;
            this.currency = currency;
            this.name = name;
            this.value = value;

            config = DataConfig.getDataConfig();
            if (config.ListOCDE.Contains(this.country))
                this.oecd = true;
            if (config.ListUE.Contains(this.country))
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

        public long Volume()
        {
            return nominale*qtty;
        }

        private void VolumeValide()
        {
            if (Volume() > 5000000)
                volume_valide = false;
            else
                volume_valide = true;
        }

        public bool GetVolumeValide
        {
            get { return volume_valide; }
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

        virtual public string ToCSV()
        {
            if (name != null)
                return isin + ";" + qtty + ";" + country + ";" + currency + ";" + name + ";" + value;
            else
                return isin + ";" + qtty;
        }
    }
}
