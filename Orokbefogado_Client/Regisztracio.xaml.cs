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
using System.Windows.Shapes;

using Orokbefogado_Client.MenhelyServiceReference;
using System.ServiceModel;

namespace Orokbefogado_Client
{
    /// <summary>
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        public Regisztracio()
        {
            InitializeComponent();
        }

        private void button_regisztracio_reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrokbefogadoKezeloClient client = new OrokbefogadoKezeloClient();
                if (password_reg1.Password == password_reg2.Password)
                {

                    if (client.Regisztracio(textbox_felhasznalonev_reg.Text, password_reg1.Password))
                    {
                        MessageBox.Show("A regisztráció sikeres!");
                        
                        MainWindow w = new MainWindow();
                        this.Close();

                        w.ShowDialog();
                    }
                    else
                    {
                        label_hibauzenet_reg.Content = "Létezik a megadott felhasználónév!";
                    }
                }
                else
                {
                    label_hibauzenet_reg.Content = "Nem egyezik meg a két jelszó.";
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}