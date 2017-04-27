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

using Orokbefogado_Client.MenhelyServiceReference;
using System.ServiceModel;

namespace Orokbefogado_Client
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


        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                FoAblak f = new FoAblak(null);
                f.label_udvlozlo.Content = "Bejelentkezve vendégként";
                f.tabcontrol_orokbefogado.Items.Remove(f.tabitem_kerelmek);
                f.tabcontrol_orokbefogado.Items.Remove(f.tabitem_adomanyozas);
                f.button_kerelemBenyujtasa.IsEnabled = false;
                f.ShowDialog();
                this.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_regisztracio_Click(object sender, RoutedEventArgs e)
        {
           
                Regisztracio r = new Regisztracio();
             //   this.Close();
                this.Close();
                r.ShowDialog();
            
           
           }

        private void button_bejelentkezes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrokbefogadoKezeloClient client = new OrokbefogadoKezeloClient();
                Orokbefogado belepett = client.Bejelentkezes(textbox_felhasznalonev.Text, password_bejelentkezes.Password);
                if (belepett != null)
                {

                    FoAblak f = new FoAblak(belepett);
                    f.label_udvlozlo.Content = string.Format("Bejelentkezve {0} felhasználóként", belepett.Nev);
                    this.Close();

                    f.ShowDialog();
                }
                else
                {
                    label_hibauzenet.Content = "Hibás a megadott adat";
                }

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}