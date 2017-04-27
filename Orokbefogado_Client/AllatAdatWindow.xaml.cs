using Orokbefogado_Client.MenhelyServiceReference;
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


namespace Orokbefogado_Client
{
    /// <summary>
    /// Interaction logic for AllatAdatWindow.xaml
    /// </summary>
    public partial class AllatAdatWindow : Window
    {
        public AllatAdatWindow(Allat kivalasztott)
        {
            InitializeComponent();
            this.DataContext = kivalasztott;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
