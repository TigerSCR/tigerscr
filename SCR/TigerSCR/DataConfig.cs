using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TigerSCR
{
    public class DataConfig
    {
        private static DataConfig _instance;
        static readonly object instanceLock = new object();
        private List<string> l_OCDE = new List<string>();
        private List<string> l_UE = new List<string>();
        private DataConfig()
        {
            this.Load();
        }

        public static DataConfig getDataConfig()
        {
            lock (instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new DataConfig();
                }
            }
            return _instance;
        }

        public List<string> ListOCDE
        {
            get { return l_OCDE; }
        }

        public List<string> ListUE
        {
            get { return l_UE; }
        }

        private void Load()
        {
            XmlDocument unxml = new XmlDocument();
            try
            {
                unxml.Load("config.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur");
                Console.WriteLine(ex);
            }

            XmlNodeList myChildNode = unxml.GetElementsByTagName("pays");
            foreach (XmlNode unNode in myChildNode)
            {
                if(unNode.ParentNode.Name == "ocde")
                    l_OCDE.Add(unNode.InnerText);
                else if (unNode.ParentNode.Name == "ue")
                    l_UE.Add(unNode.InnerText);
            }

            Console.WriteLine("liste OCDE");
            foreach (string s in l_OCDE)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("liste UE");
            foreach (string s in l_UE)
            {
                Console.WriteLine(s);
            }
        }

    }
}
