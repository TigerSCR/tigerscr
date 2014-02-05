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
        private Dictionary<string,int> isins;

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
            //Connector.getConnector().getEquities(portfolio);
        }


        public void getIsin()
        {
            Excel.Range r = activeWorksheet.get_Range("A:A");

            foreach (Excel.Range cell in r.Cells)
            {
                if (cell.Value == null)
                {;
                    MessageBox.Show("Fin de la feuille en ["+cell.get_Address()+"] - Acquisition terminée avec "+this.isins.Count+" Codes");
                    break;
                }
               /* else
                {   cell.get_Address();
                    isins.Add(cell.Value2, ;
                }*/
            }
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
