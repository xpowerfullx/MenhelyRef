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

using Menhely.MenhelyServiceReference;
using System.Text.RegularExpressions;

namespace Menhely
{
    /// <summary>
    /// Interaction logic for UjAllatAblak.xaml
    /// </summary>
    public partial class UjAllatAblak : Window
    {
        UjAllatViewModel vm;
        AllatKezeloClient allatClient;
        TelephelyKezeloClient telephelyClient;

        public UjAllatAblak(Gondozo gondozo)
        {
            InitializeComponent();
            fajCB.ItemsSource = Enum.GetValues(typeof(AllatFaj)).Cast<AllatFaj>();
            telephelyClient = new TelephelyKezeloClient();
            vm = new UjAllatViewModel();
            vm.Telephelyek = gondozo.Munkahelyek;
            allatClient = new AllatKezeloClient();
            this.DataContext = vm;
        }

        private void btFelvetel(object sender, RoutedEventArgs e)
        {
            if (
                    nevTB.Text != "" &&
                    fajCB.SelectedItem != null &&
                    korTB.Text != "" &&
                    vm.KivTelephely != null &&
                    vm.KivKetrec != null &&
                    vm.KivGondozo != null
                )
            {
                if (allatClient.AllatListazasEgy(nevTB.Text).Count() == 0)
                {
                    allatClient.AllatFelvetel(nevTB.Text, leirasTB.Text, int.Parse(korTB.Text), (AllatFaj)(fajCB.SelectedItem), alfajTB.Text, vm.KivKetrec, vm.KivGondozo);
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Van már ilyen nevű állat az adatbázisban! Az állat nevének egyedinek kell legyen, válasszon másik nevet!", "Foglalt állatnév", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("A következő adatokat kötelező megadni:\n\t-Állat neve\n\t-Állat faja\n\t-Állat kora\n\t-Telephely\n\t-Ketrec\n\t-Első gondozó\nAmennyiben az állat kora ismeretlen, akkor értéknek válassza a 0-át!", "Hiányzó adatok", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btMegse(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void telephelyCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Telephely temp = telephelyClient.TelephelyListazasEgy(vm.KivTelephely.Cim).First();
            if (temp != null)
            {
                if (fajCB.SelectedItem != null)
                {
                    vm.Ketrecek = temp.Ketrecek.Where(x => x.Faj == (AllatFaj)(fajCB.SelectedItem) && x.Meret > x.Allatok.Count()).ToArray();
                }
                vm.Gondozok = temp.Dolgozok;
            }
        }

        private void fajCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.KivTelephely != null)
            {
                Telephely temp = telephelyClient.TelephelyListazasEgy(vm.KivTelephely.Cim).First();
                if (temp != null)
                {
                    vm.Ketrecek = temp.Ketrecek.Where(x => x.Faj == (AllatFaj)(fajCB.SelectedItem) && x.Meret > x.Allatok.Count()).ToArray();
                }
            }
        }
    }
}
