using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;

namespace TigerSCR
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void init_Click(object sender, RibbonControlEventArgs e)
        {
            Engine.getEngine().Update();
        }

        private void portfolio_Click(object sender, RibbonControlEventArgs e)
        {
            MessageBox.Show(Engine.getEngine().ToString());
        }
    }
}
