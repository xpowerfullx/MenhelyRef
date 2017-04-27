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
using System.Text.RegularExpressions;

using Menhely.MenhelyServiceReference;

namespace Menhely
{
    /// <summary>
    /// Interaction logic for AllatAblak.xaml
    /// </summary>
    public partial class AllatAblak : Window
    {
        FoViewModel vm;

        public AllatAblak(FoViewModel vm, bool zaroltAblakE)
        {
            InitializeComponent();
            this.vm = vm;
            vm.ZaroltAblak = zaroltAblakE;
            this.DataContext = vm.KivalsztottAllat;
        }

        private void btOK(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
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
    }
}
