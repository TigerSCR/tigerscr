namespace TigerSCR
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.readme = this.Factory.CreateRibbonGroup();
            this.notice = this.Factory.CreateRibbonLabel();
            this.equity = this.Factory.CreateRibbonGroup();
            this.init = this.Factory.CreateRibbonButton();
            this.toString = this.Factory.CreateRibbonGroup();
            this.portfolio = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.readme.SuspendLayout();
            this.equity.SuspendLayout();
            this.toString.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.readme);
            this.tab1.Groups.Add(this.equity);
            this.tab1.Groups.Add(this.toString);
            this.tab1.Label = "Tiger SCR";
            this.tab1.Name = "tab1";
            // 
            // readme
            // 
            this.readme.Items.Add(this.notice);
            this.readme.Label = "Readme";
            this.readme.Name = "readme";
            // 
            // notice
            // 
            this.notice.Label = "1. Cliquez sur Init";
            this.notice.Name = "notice";
            // 
            // equity
            // 
            this.equity.Items.Add(this.init);
            this.equity.Label = "Equity";
            this.equity.Name = "equity";
            // 
            // init
            // 
            this.init.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.init.Label = "Init";
            this.init.Name = "init";
            this.init.ShowImage = true;
            this.init.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.init_Click);
            // 
            // toString
            // 
            this.toString.Items.Add(this.portfolio);
            this.toString.Label = "toString";
            this.toString.Name = "toString";
            // 
            // portfolio
            // 
            this.portfolio.Label = "portfolio";
            this.portfolio.Name = "portfolio";
            this.portfolio.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.portfolio_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.readme.ResumeLayout(false);
            this.readme.PerformLayout();
            this.equity.ResumeLayout(false);
            this.equity.PerformLayout();
            this.toString.ResumeLayout(false);
            this.toString.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup equity;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton init;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup readme;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel notice;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup toString;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton portfolio;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
