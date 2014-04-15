using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace TigerAppWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public MainWindow()
        {
            InitializeComponent();
            //Pour être sur que les singleton soit instanciés
            Engine.getEngine().registerObserver(this);
            //Printer.getPrinter();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog temp = new Microsoft.Win32.OpenFileDialog();
            Stream myStream = null;

            List<Tuple<string, int>> resultat = new List<Tuple<string, int>>();

            temp.Filter = "CSV files (*.csv)|*.csv";
            Nullable<bool> result = temp.ShowDialog();
            if (result==true)
            {
                try
                {
                    if ((myStream = temp.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            StreamReader sr = new StreamReader(myStream);
                            String s = sr.ReadLine();
                            String[] temps;
                            while (s != null)
                            {
                                temps = s.Split(';');
                                resultat.Add(new Tuple<string, int>(temps[0], int.Parse(temps[1])));
                                s = sr.ReadLine();
                            }
                            sr.Close();
                            Engine.getEngine().setIsins(resultat);
                        }
                    }
                    myStream.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public void notify()
        { test.ItemsSource = Engine.getEngine().Portfolio; }

        private void Outils_Calculer_Equity_Click(object sender, RoutedEventArgs e)
        {
            Engine.getEngine().calculate();
        }
    }
}
