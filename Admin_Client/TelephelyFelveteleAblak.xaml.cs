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
    /// Interaction logic for TelephelyFelveteleAblak.xaml
    /// </summary>
    public partial class TelephelyFelveteleAblak : Window
    {
        TelephelyKezeloClient telephelyClient;

        public TelephelyFelveteleAblak()
        {
            InitializeComponent();
            telephelyClient = new TelephelyKezeloClient();
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            string telephelyCm = TelephelyCimTextBox.Text;

            if (telephelyCm != "")
                if (telephelyClient.TelephelyListazasEgy(telephelyCm).Count() == 0)
                {
                    telephelyClient.TelephelyFelvetel(telephelyCm);
                    this.DialogResult = true;
                }
                else
                    MessageBox.Show("Van már ilyen című telephely!", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Írjon be egy címet, vagy kattinson a Mégse gombra!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void MegseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}
