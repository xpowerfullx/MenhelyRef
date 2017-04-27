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
    public partial class GondozoFelveteleAblak : Window
    {
        TelephelyKezeloClient telephelyClient;
        GondozoKezeloClient gondozoKezelo;
        GondozoBeosztas ujBeoszt;
        FoViewModel vm;


        public GondozoFelveteleAblak()
        {
            InitializeComponent();
            
            gondozoKezelo = new GondozoKezeloClient();
            telephelyClient = new TelephelyKezeloClient();
            vm = FoViewModel.GetVM();
            this.DataContext = vm;

            BeosztasComboBox.ItemsSource = Enum.GetValues(typeof(GondozoBeosztas)).Cast<GondozoBeosztas>();
            vm.Telephelyek = telephelyClient.TelephelyListazas();
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            if (nev_box.Text != "" && BeosztasComboBox.SelectedItem != null && jelszo_box.Password != "" && TelephelyComboBox.SelectedItem != null)
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

        private void BeosztasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BeosztasComboBox.SelectedItem!=null)
            {
                ujBeoszt =(GondozoBeosztas)BeosztasComboBox.SelectedItem ;
            }
        }

        public FoViewModel Vm
        {
            get { return vm; }
            set { vm = value; }
        }
    }
}
