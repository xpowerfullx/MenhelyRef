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
using System.ServiceModel;

using Admin_Client.MenhelyServiceReference;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BejelentkezesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GondozoKezeloClient client = new GondozoKezeloClient();
                Gondozo belepett = client.Bejelenkezes(nevTB.Text, jelszoTB.Password);
                if (belepett != null && belepett.Beosztas == GondozoBeosztas.Adminisztrátor)
                {
                    FoAblak fa = new FoAblak(this, belepett);
                    fa.Show();
                }
                else
                {
                    hibaLabel.Content = "Nincs ilyen adminisztrátor vagy rossz a jelszó!";
                }
            }
            catch(EndpointNotFoundException)
            {
                MessageBox.Show("HIBA: Nincs elindítva a szerver.");
            }

        }

        private void KilepesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
