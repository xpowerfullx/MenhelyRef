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
    /// Interaction logic for UjMunkahely.xaml
    /// </summary>
    public partial class UjMunkahely : Window
    {
        FoViewModel vm;
        GondozoKezeloClient gk;
        public UjMunkahely()
        {
            InitializeComponent();
            vm = FoViewModel.GetVM();
            gk = new GondozoKezeloClient();
            TelephelyKezeloClient telephelyClient = new TelephelyKezeloClient();
            List<Telephely> telepek = new List<Telephely>();
            foreach (var item in telephelyClient.TelephelyListazas())
            {
                bool mehet = true;
                foreach (var item2 in vm.KivGondozo.Munkahelyek)
                {
                    if (item.Cim == item2.Cim )
                    {
                        mehet = false;
                    }
                }
                if (mehet)
                {
                    telepek.Add(item); 
                }
            }
            comboBox.ItemsSource = telepek;
            comboBox.DisplayMemberPath = "Cim";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex >= 0)
            {
                gk.TelephelyGondozohozAdas(vm.KivGondozo, (Telephely)comboBox.SelectedItem);
                DialogResult = true;
            }
            
        }
    }
}
