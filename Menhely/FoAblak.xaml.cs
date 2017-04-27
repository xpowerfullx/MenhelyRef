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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Menhely
{
    /// <summary>
    /// Interaction logic for FoAblak.xaml
    /// </summary>
    public partial class FoAblak : Window
    {
        Window loginAblak;
        AllatKezeloClient allatClient;
        GondozoKezeloClient gondozoClient;

        FoViewModel vm;

        Allat[] Allatok { get; set; }

        public FoAblak(Window loginAblak, Gondozo bejelentkezettGondozo)
        {
            InitializeComponent();
            vm = new FoViewModel();
            this.DataContext = vm;
            this.loginAblak = loginAblak;
            gondozoClient = new GondozoKezeloClient();
            vm.Felhasznalo = gondozoClient.GondozoListazasEgy(bejelentkezettGondozo.Nev)[0];
            if (vm.Felhasznalo.Beosztas != GondozoBeosztas.Gondozó)
            {
                ujAllatBtn.IsEnabled = true;
            }
            gondozoNevLabel.Content = vm.Felhasznalo.Nev + " (" + vm.Felhasznalo.Beosztas.ToString() + ")";
            allatClient = new AllatKezeloClient();
            
            ListaFrissites();
        }

        private void btAllatGondozas(object sender, RoutedEventArgs e)
        {
            GondozasAblak ga = new GondozasAblak(vm.KivalsztottAllat.Nev);
            if (ga.ShowDialog() == true)
            {
                allatClient.AllatGondozas(vm.KivalsztottAllat, vm.Felhasznalo, ga.jegyzokonyvTB.Text);
                ListaFrissites();
            }
        }

        private void btOrokbefogadasok(object sender, RoutedEventArgs e)
        {
            OrokbefogadasAblak oa = new OrokbefogadasAblak(vm.Felhasznalo);
            if (oa.ShowDialog() == true)
            {
                ListaFrissites();
            }
        }

        private void btAllatModositas(object sender, RoutedEventArgs e)
        {
            AllatAblak aa = new AllatAblak(vm,false);
            if (aa.ShowDialog() == true)
            {
                allatClient.AllatModositas(vm.KivalsztottAllat, vm.Felhasznalo);
                ListaFrissites();
            }
        }

        private void btAllatMasikKetrecbe(object sender, RoutedEventArgs e)
        {
            AllatAthelyezesAblak aaa = new AllatAthelyezesAblak(vm.KivalsztottAllat);
            if (aaa.ShowDialog() == true)
            {
                try
                {
                    allatClient.AllatMasikKetrecbe(vm.KivalsztottAllat, aaa.KivalasztottKetrec);
                    ListaFrissites();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Megszakadt a kapcsolat a szerverrel!", "Nincs kapcsolat", MessageBoxButton.OK, MessageBoxImage.Error);
                    MainWindow mw = new MainWindow(this);
                    mw.Show();
                }
            }
        }

        private void btAllatTorles(object sender, RoutedEventArgs e)
        {
            MessageBoxResult megerosit = MessageBox.Show("Biztosan törölni szeretné a(z) "+vm.KivalsztottAllat.Nev+" nevű állatot a rendszerből?\nA művelet nem visszavonható!","Biztosan törli?",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (megerosit == MessageBoxResult.Yes)
            {
                allatClient.AllatTorlese(vm.KivalsztottAllat, vm.Felhasznalo);
            }
            ListaFrissites();
        }

        private void btUjAllat(object sender, RoutedEventArgs e)
        {
            UjAllatAblak uaa = new UjAllatAblak(vm.Felhasznalo);
            if (uaa.ShowDialog() == true)
            {
                ListaFrissites();
            }
        }

        private void btGondozoAdatmodositas(object sender, RoutedEventArgs e)
        {
            GondozoAblak ga = new GondozoAblak(vm);
            if (ga.ShowDialog() == true)
            {
                vm.Felhasznalo.Jelszo = ga.jelszoUjPB.Password;
                gondozoClient.GondozoModositas(vm.Felhasznalo);
                // Visszaellenőrzünk, hogy sikeres-e
                if (gondozoClient.GondozoListazasEgy(vm.Felhasznalo.Nev).First().Jelszo == ga.jelszoUjPB.Password)
                {
                    MessageBox.Show("Sikeresen megváltoztatta jelszavát!", "Sikeres jelszóváltozás", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Sikertelen jelszóváltoztatás! Próbálja később!", "Sikertelen jelszóváltozás", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loginAblak.Close();
        }

        private void btKijelentkezes(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this);
            mw.Show();
        }

        private void allatokDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (vm.KivalsztottAllat != null)
            {
                AllatAblak aa = new AllatAblak(vm, false);
                if (aa.ShowDialog() == true)
                {
                    allatClient.AllatModositas(vm.KivalsztottAllat, vm.Felhasznalo);
                    ListaFrissites();
                }
            }
        }

        void ListaFrissites()
        {
            try
            {
                vm.Felhasznalo = gondozoClient.GondozoListazasEgy(vm.Felhasznalo.Nev)[0];
                if (vm.Felhasznalo.Beosztas == GondozoBeosztas.Adminisztrátor)
                {
                    vm.Allatok = allatClient.AllatListazas().ToArray();
                }
                else
                {
                    List<Allat> allatTempLista = new List<Allat>();
                    foreach (var allat in vm.Felhasznalo.GondozottAllatok)
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

        private void btFrissites(object sender, RoutedEventArgs e)
        {
            ListaFrissites();
        }

    }
}
