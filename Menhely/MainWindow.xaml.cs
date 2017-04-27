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

using Menhely.MenhelyServiceReference;
using System.ServiceModel;

namespace Menhely
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window elozo;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Window elozo)
        {
            InitializeComponent();
            this.elozo = elozo;
        }

        private void btBejelentkezes(object sender, RoutedEventArgs e)
        {
            GondozoKezeloClient client = new GondozoKezeloClient();
            try
            {
                Gondozo belepett = client.Bejelenkezes(nevTB.Text, jelszoTB.Password);
                if (belepett != null)
                {
                    FoAblak fa = new FoAblak(this, belepett);
                    fa.Show();
                }
                else
                {
                    hibaLabel.Content = "Nincs ilyen felhasználó vagy rossz jelszó!";
                    nevTB.Clear();
                    jelszoTB.Clear();
                }
            
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btKilepes(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (elozo != null)
            {
                elozo.Close();
            }
        }
    }
}
