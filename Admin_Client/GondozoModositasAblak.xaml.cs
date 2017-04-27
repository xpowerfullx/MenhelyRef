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
using Admin_Client.MenhelyServiceReference;
namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for GondozoModositasAblak.xaml
    /// </summary>
    public partial class GondozoModositasAblak : Window
    {
        FoViewModel vm;
        public GondozoModositasAblak()
        {
            InitializeComponent();
            vm = FoViewModel.GetVM();
            DataContext = vm.KivGondozo;
            BeosztasComboBox.ItemsSource = Enum.GetValues(typeof(GondozoBeosztas)).Cast<GondozoBeosztas>();
        }
        private void BeosztasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BeosztasComboBox.SelectedItem != null)
            {
                vm.KivGondozo.Beosztas = (GondozoBeosztas)BeosztasComboBox.SelectedItem;
            }
        }
        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            if (BeosztasComboBox.SelectedItem != null )
            {
                this.DialogResult = true;
            }
            else
                MessageBox.Show("Egy mező se maradhat üresen!", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MegseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        void Barnannak()
        {
            /*
            csinálj gombot-ablakot az állatmódosításnak meg a telephelybódosításnak
            a gondozó fülön
            szedd ki a státusz módosítást
            */
            AllatKezeloClient allatk = new AllatKezeloClient();
            GondozoKezeloClient gondk = new GondozoKezeloClient();
            List<Allat> allatok = new List<Allat>();
            vm = FoViewModel.GetVM();
            var user = gondk.GondozoListazasEgy(vm.KivGondozo.Nev).First();
            var temp = allatk.AllatListazas();
            foreach (var item in temp)
            {
                if (!item.Gondozok.Contains(user) && user.Munkahelyek.Contains(item.Ketrec.Hely))
                {
                    allatok.Add(item);
                }
            }

        }

    }
}
