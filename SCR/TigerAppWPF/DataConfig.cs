using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TigerAppWPF
{
    public class DataConfig
    {
        private static DataConfig _instance;
        static readonly object instanceLock = new object();
        private SortedSet<string> l_OCDE = new SortedSet<string>();
        private SortedSet<string> l_UE = new SortedSet<string>();

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

        public SortedSet<string> ListOCDE
        {
            get { return l_OCDE; }
        }

        public SortedSet<string> ListUE
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
