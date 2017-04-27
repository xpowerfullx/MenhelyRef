using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Admin_Client.MenhelyServiceReference;
using System.ServiceModel;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for AllatmodositasAblak.xaml
    /// </summary>
    public partial class AllatmodositasAblak : Window
    {
        FoViewModel vm;
        
        AllatKezeloClient allatk;
        GondozoKezeloClient gondk;

      

        public AllatmodositasAblak()
        {
            InitializeComponent();
            vm = FoViewModel.GetVM();
            allatk = new AllatKezeloClient();
            gondk = new GondozoKezeloClient();
            DataContext = vm;
            Listafrissites();
        }
        void Listafrissites()
        {

            try
            {

                vm.Felhasznalo = gondk.GondozoListazasEgy(vm.Felhasznalo.Nev)[0];
                
                   // vm.Allatok = allatk.AllatListazas().ToList<Allat>();

                List<Allat> allatTempLista = new List<Allat>();
                foreach (var allat in vm.KivGondozo.GondozottAllatok)
                {
                    allatTempLista.Add(allatk.AllatListazasEgy(allat.Nev).First());
                }
                vm.Allatok = allatTempLista;


            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Megszakadt a kapcsolat a szerverrel!", "Nincs kapcsolat", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }

        }

        private void btSzuntet_Click(object sender, RoutedEventArgs e)
        {
            if (vm.KivAllat != null && vm.KivGondozo != null)
            {
                gondk.GondozottAllatEltavolitas(vm.KivGondozo, vm.KivAllat);
                Listafrissites();
                this.DialogResult = true;
            }
            else
                MessageBox.Show("Nincs Állat kijelölve!");
           
        }

        private void btUj_Click(object sender, RoutedEventArgs e)
        {
            UjGondozasAblak uga = new UjGondozasAblak();
            uga.ShowDialog();
            if (uga.DialogResult==true)
            {
                Listafrissites();
                this.DialogResult = true;
            }
        }
    }
   }

