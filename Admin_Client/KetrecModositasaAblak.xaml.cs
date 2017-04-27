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
using System.Text.RegularExpressions;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for KetrecModositasaAblak.xaml
    /// </summary>
    public partial class KetrecModositasaAblak : Window
    {
        Ketrec ketrec;

        public KetrecModositasaAblak(Ketrec ketrec)
        {
            InitializeComponent();

            this.ketrec = ketrec;
            this.DataContext = ketrec;
            /*
            MeretTextBox.Text = ketrec.Meret.ToString();
            AllatFajLabel.Content = ketrec.Faj;
            TelephelyLabel.Content = ketrec.Hely.Cim;

            AllataliListBox.ItemsSource = ketrec.Allatok;
            AllataliListBox.DisplayMemberPath = "Nev";
            */
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            if (ketrec.Allatok.Count() == 0)
                this.DialogResult = true;
            else
                MessageBox.Show("Csak üres ketrecet lehet módosítani!", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MegseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
