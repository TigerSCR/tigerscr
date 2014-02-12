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
            this.acquisition = this.Factory.CreateRibbonGroup();
            this.init = this.Factory.CreateRibbonButton();
            this.affichage = this.Factory.CreateRibbonGroup();
            this.portfolio = this.Factory.CreateRibbonButton();
            this.calculs = this.Factory.CreateRibbonGroup();
            this.go = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.readme.SuspendLayout();
            this.acquisition.SuspendLayout();
            this.affichage.SuspendLayout();
            this.calculs.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.readme);
            this.tab1.Groups.Add(this.acquisition);
            this.tab1.Groups.Add(this.affichage);
            this.tab1.Groups.Add(this.calculs);
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
            // acquisition
            // 
            this.acquisition.Items.Add(this.init);
            this.acquisition.Label = "Acquisition";
            this.acquisition.Name = "acquisition";
            // 
            // init
            // 
            this.init.Label = "Init";
            this.init.Name = "init";
            this.init.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.init_Click);
            // 
            // affichage
            // 
            this.affichage.Items.Add(this.portfolio);
            this.affichage.Label = "Affichage";
            this.affichage.Name = "affichage";
            // 
            // portfolio
            // 
            this.portfolio.Label = "portfolio";
            this.portfolio.Name = "portfolio";
            this.portfolio.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.portfolio_Click);
            // 
            // calculs
            // 
            this.calculs.Items.Add(this.go);
            this.calculs.Label = "Calculs";
            this.calculs.Name = "calculs";
            // 
            // go
            // 
            this.go.Label = "GO";
            this.go.Name = "go";
            this.go.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.go_Click);
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
            this.acquisition.ResumeLayout(false);
            this.acquisition.PerformLayout();
            this.affichage.ResumeLayout(false);
            this.affichage.PerformLayout();
            this.calculs.ResumeLayout(false);
            this.calculs.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup acquisition;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton init;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup readme;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel notice;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup affichage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton portfolio;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup calculs;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton go;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
