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
using System.ServiceModel;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for MunkahelyModositasAblak.xaml
    /// </summary>
    public partial class MunkahelyModositasAblak : Window
    {
        FoViewModel vm;
        
        GondozoKezeloClient gondk;
        TelephelyKezeloClient telepk;


        public MunkahelyModositasAblak()
        {
            InitializeComponent();
            vm = FoViewModel.GetVM();
            gondk = new GondozoKezeloClient();
            telepk = new TelephelyKezeloClient();
            this.DataContext = vm.KivGondozo;
            Listafrissites();
        }
        void Listafrissites()
        {

            try
            {
                vm.Felhasznalo = gondk.GondozoListazasEgy(vm.Felhasznalo.Nev)[0];
                vm.KivGondozo = gondk.GondozoListazasEgy(vm.KivGondozo.Nev).First();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Megszakadt a kapcsolat a szerverrel!", "Nincs kapcsolat", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }

        }

        private void btSzuntet_Click(object sender, RoutedEventArgs e)
        {
            if (telephelyekLB.SelectedIndex >= 0)
            {
                if (vm.KivGondozo.Munkahelyek.Count() > 1)
                {
                   
                    gondk.TelephelyGondozotolLevetel(vm.KivGondozo, (Telephely)telephelyekLB.SelectedItem);
                    Listafrissites();
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Legalább egy munkahely kell a gondozónak!");
                }
                
            } 
        }

        private void btUj_Click(object sender, RoutedEventArgs e)
        {
            UjMunkahely uga = new UjMunkahely();
            uga.ShowDialog();
            if (uga.DialogResult==true)
            {
                Listafrissites();
                this.DialogResult = true;
            }
        }
    }
}
