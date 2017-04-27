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
using System.ServiceModel;

namespace Menhely
{
    /// <summary>
    /// Interaction logic for OrokbefogadasAblak.xaml
    /// </summary>
    public partial class OrokbefogadasAblak : Window
    {
        FoViewModel vm;
        AllatKezeloClient allatClient;
        OrokbefogadasKezeloClient orokbefogadasClient;

        public OrokbefogadasAblak(Gondozo gondozo)
        {
            InitializeComponent();
            this.vm = new FoViewModel();
            this.DataContext = vm;
            vm.Felhasznalo = gondozo;
            this.allatClient = new AllatKezeloClient();
            this.orokbefogadasClient = new OrokbefogadasKezeloClient();
            ListaLetoltes();
        }

        private void btElfogadas(object sender, RoutedEventArgs e)
        {
            if (vm.KivalsztottAllat != null)
            {
                orokbefogadasClient.OrokbefogadasElfogadasa(vm.KivalsztottAllat);
                this.DialogResult = true;
            }

        }

        private void btVisszautasitas(object sender, RoutedEventArgs e)
        {
            if (vm.KivalsztottAllat != null)
            {
                orokbefogadasClient.OrokbefogadasVisszautasitas(vm.KivalsztottAllat);
                this.DialogResult = true;
            }
        }

        private void btMegse(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        void ListaLetoltes()
        {
            try
            {
                if (vm.Felhasznalo.Beosztas == GondozoBeosztas.Adminisztrátor)
                {
                    vm.Allatok = allatClient.AllatListazas().Where(x => x.Allapot == OrokbefogadasAllapot.Foglalt).ToArray();
                }
                else
                {
                    List<Allat> allatTempLista = new List<Allat>();
                    var szurtAllatok = vm.Felhasznalo.GondozottAllatok.Where(x => x.Allapot == OrokbefogadasAllapot.Foglalt);
                    foreach (var allat in szurtAllatok)
                    {
                        allatTempLista.Add(allatClient.AllatListazasEgy(allat.Nev).First());
                    }
                    vm.Allatok = allatTempLista.ToArray();

                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Megszakadt a kapcsolat a szerverrel!", "Nincs kapcsolat", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow mw = new MainWindow(this);
                mw.Show();
            }
        }

        private void allatokDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (vm.KivalsztottAllat != null)
            {
                AllatAblak aa = new AllatAblak(vm, true);
                aa.ShowDialog();
            }
        }
    }
}
