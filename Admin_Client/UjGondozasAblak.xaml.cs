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
    /// Interaction logic for UjGondozasAblak.xaml
    /// </summary>
    public partial class UjGondozasAblak : Window
    {
        FoViewModel vm;
        GondozoKezeloClient gk;
        public UjGondozasAblak()
        {
            InitializeComponent();
            vm = FoViewModel.GetVM();
            gk = new GondozoKezeloClient();
            DataContext = vm;
            TelephelyKezeloClient telephelyClient = new TelephelyKezeloClient();
            AllatKezeloClient allatClint = new AllatKezeloClient();
            List<Allat> allatok = new List<Allat>();
            foreach (var a in vm.KivGondozo.Munkahelyek)
            {
                Telephely temp = telephelyClient.TelephelyListazasEgy(a.Cim).First(); ;
                foreach (var ket in temp.Ketrecek)
                {
                    Ketrec temp2 = telephelyClient.KetrecListazasEgy(ket.KetrecID).First();
                    foreach (var al in temp2.Allatok)
                    {
                        Allat temp3 = allatClint.AllatListazasEgy(al.Nev).First();
                        bool mehet = true;
                        foreach (Gondozo gond in temp3.Gondozok)
                        {
                            if (gond.Nev == vm.KivGondozo.Nev)
                            {
                                mehet = false;
                            }
                        }
                        if (mehet)
                        {
                            allatok.Add(temp3);
                        }
                    }
                }
            }
            comboBox.ItemsSource = allatok;
            comboBox.DisplayMemberPath = "Nev";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (vm.KivAllat != null)
            {
                gk.GondozottAllatHozzaadas(vm.KivGondozo, vm.KivAllat);
                DialogResult = true;
            }
            else
                MessageBox.Show("Nincs állat kiválasztva!");
        }
    }
}
