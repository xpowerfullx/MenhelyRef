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
    /// Interaction logic for AllatAthelyezeseAblak.xaml
    /// </summary>
    public partial class AllatAthelyezeseAblak : Window
    {

        AllatAthelyezesViewModell vm;
        TelephelyKezeloClient telephelyClient;
        public AllatAthelyezeseAblak(Ketrec kivKetrec)
        {
            InitializeComponent();

            //jelTelephelyLabel
            //jelKetrecLabel
            //AllatNeveComboBox
            this.vm = new AllatAthelyezesViewModell(kivKetrec);
            this.DataContext = vm;
            telephelyClient = new TelephelyKezeloClient();
            vm.Telephelyek = telephelyClient.TelephelyListazas();

        }

        private void MegseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            if (vm.KivAllat != null && vm.KivKetrec != null && vm.KivTelephely != null)
                this.DialogResult = true;
            else
                MessageBox.Show("Mindenhol legyen valami kijelölve", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void AllatNeveComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.KivTelephely != null)
            {
                vm.Ketrecek = vm.KivTelephely.Ketrecek.Where(x => x.Faj == vm.JelKetrec.Faj && x.Meret > x.Allatok.Count()).ToArray();
            }
        }

        private void TelephelyeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.KivAllat != null)
            {
                vm.Ketrecek = vm.KivTelephely.Ketrecek.Where(x => x.Faj == vm.JelKetrec.Faj && x.Meret > x.Allatok.Count()).ToArray();
            }
        }

        internal AllatAthelyezesViewModell Vm
        {
            get { return vm; }
            set { vm = value; }
        }
    }
}
