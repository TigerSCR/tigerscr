using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace TigerSCR
{
    class Engine
    {
        //Static var for Singleton pattern
        private static Engine engine;
        private Excel.Worksheet activeWorksheet;


        //Runtime var
        private List<Title> portfolio;
        private Dictionary<string,int> isins=new Dictionary<string,int>();

        private Engine()
        {
            this.portfolio = new List<Title>();
        }

        public static Engine getEngine(Excel.Worksheet ws=null)
        {
            if (engine == null)
                engine = new Engine();
            if (ws != null)
                engine.activeWorksheet = ws;
            return engine;
        }

        public void Update()
        {
            this.getIsin();
            Array temp =this.isins.ToArray();
            foreach (Object o in temp)
            {
                MessageBox.Show(o.ToString());
            }
            //Connector.getConnector().getEquities(portfolio);
        }


        public void getIsin()
        {
            
            //Get all the used cells
            Excel.Range r = activeWorksheet.UsedRange;

            List<String> listisins=new List<string>();
            List<int> listqtty=new List<int>();
            int count=0;

            //Put the used cell in two different list according to their parity
            foreach(Excel.Range cell in r.Cells)
            {
                count++;
                if(count%2==1)
                    listisins.Add(cell.Value2);
                else
                    listqtty.Add((int)cell.Value2);
            }


            //Checking the concordance of the two list, each isin must have a concording qqty
            if(listisins.Count()!=listqtty.Count())
            {
                ArgumentException except=new ArgumentException();
                throw except;
            }

            //filling the global isins dictionary
            for(int i=0;i<listisins.Count();i++)
            {
                if(i%2==1)
                    isins.Add(listisins[i],listqtty[i]);
            }

            MessageBox.Show("Acquisition terminée avec " + this.isins.Count() + " Codes");

            /*foreach (Excel.Range cell in r.Cells)
            {
                if (cell.Value == null)
                {
                    MessageBox.Show("Fin de la feuille en ["+cell.get_Address()+"] - Acquisition terminée avec "+this.isins.Count+" Codes");
                    break;
                }
                else
                {   cell.get_Address();
                    isins.Add(cell.Value2, ;
                }
            }*/
        }

        public override string ToString()
        {
            string result="";
            result += "EQUITIES\n";
            foreach (Equity eq in portfolio)
            {
                result += eq.ToString() + "\n";
            }
            return result;
        }
    }
}
