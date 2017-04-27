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

namespace Menhely
{
    /// <summary>
    /// Interaction logic for GondozasAblak.xaml
    /// </summary>
    public partial class GondozasAblak : Window
    {
        public GondozasAblak(string allatNev)
        {
            InitializeComponent();
            this.Title = allatNev + " nevű állat gondozása";
        }

        private void btGondozas(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btMegse(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
