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
    /// Interaction logic for KetrecFelveteleAblak.xaml
    /// </summary>
    public partial class KetrecFelveteleAblak : Window
    {
        public KetrecFelveteleAblak()
        {
            InitializeComponent();

            AllatFajComboBox.ItemsSource = Enum.GetValues(typeof(AllatFaj)).Cast<AllatFaj>();
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            if (MeretTextBox.Text != 0.ToString() && AllatFajComboBox.SelectedItem != null)
            {
                this.DialogResult = true;
            }
            else
                MessageBox.Show("Semmit sem hagyhat üresen!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            
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
