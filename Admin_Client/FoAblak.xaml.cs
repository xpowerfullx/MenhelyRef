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

using System.ServiceModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Admin_Client.MenhelyServiceReference;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for FoAblak.xaml
    /// </summary>
    public partial class FoAblak : Window
    {
        Window elozo;
        TelephelyKezeloClient telephelyClient;
        GondozoKezeloClient gondozoClient;
        FoViewModel vm;
        AllatKezeloClient allatClient;

        public FoAblak(Window elozo, Gondozo bejelentkezettAdmin)
        {
            InitializeComponent();

            vm = FoViewModel.GetVM();
            this.DataContext = vm;

            this.elozo = elozo;
            vm.Felhasznalo = bejelentkezettAdmin;

            telephelyClient = new TelephelyKezeloClient();
            gondozoClient = new GondozoKezeloClient();
            allatClient = new AllatKezeloClient();
            ListaFrissites();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            elozo.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tablazatTab.SelectedIndex = gombokTab.SelectedIndex;
        }

        private void tablazatTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gombokTab.SelectedIndex = tablazatTab.SelectedIndex;
        }


#region Telephely-Fül

        private void TelephelyFelveteleButton_Click(object sender, RoutedEventArgs e)
        {
            TelephelyFelveteleAblak tfa = new TelephelyFelveteleAblak();
            if (tfa.ShowDialog() == true)
            {
                ListaFrissites();
            }
        }

        private void TelephelyMegszunteteseButton_Click(object sender, RoutedEventArgs e)
        {
            if (vm.KivTelephely.Dolgozok.Count() == 0 && vm.KivTelephely.Ketrecek.Count() == 0)
            {
                MessageBoxResult megerosit = MessageBox.Show("Biztosan törölni szeretné a(z) " + vm.KivTelephely.Cim + " nevű telephelyet a rendszerből?\nA művelet nem visszavonható!", "Biztosan törli?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (megerosit == MessageBoxResult.Yes)
                {
                    telephelyClient.TelephelyTorles(vm.KivTelephely);
                }
            }
            else
                MessageBox.Show("Nem törölhet olyan Telephelyet, aminek van Ketrece vagy van Dolgozója", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
            ListaFrissites();
        }

        private void KetrecFelveteleButton_Click(object sender, RoutedEventArgs e)
        {
            KetrecFelveteleAblak kfa = new KetrecFelveteleAblak();
            if (kfa.ShowDialog() == true)
            {
                telephelyClient.KetrecHozzaadas(vm.KivTelephely, int.Parse(kfa.MeretTextBox.Text), (AllatFaj)kfa.AllatFajComboBox.SelectedItem);
                ListaFrissites();
            }
        }

        private void KetrecModositasaButton_Click(object sender, RoutedEventArgs e)
        {
            KetrecModositasaAblak kma = new KetrecModositasaAblak(vm.kivKetrec);
            if (kma.ShowDialog() == true)
                telephelyClient.KetrecModositas(vm.KivKetrec);

            //vm.KivKetrec = null;
            ListaFrissites();
        }

        private void KetrecTorleseButton_Click(object sender, RoutedEventArgs e)
        {
            if (vm.KivKetrec.Allatok.Count() == 0)
            {
                MessageBoxResult megerosit = MessageBox.Show("Biztosan törölni szeretné a(z) " + vm.KivKetrec.KetrecID + " ID ketrecet a rendszerből?\nA művelet nem visszavonható!", "Biztosan törli?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (megerosit == MessageBoxResult.Yes)
                {
                    telephelyClient.KetrecTorles(vm.KivTelephely, vm.KivKetrec);
                }
                ListaFrissites();
            }
            else
                MessageBox.Show("Foglalt ketrecet nem lehet törölni!", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AllatAthelyezeseButton_Click(object sender, RoutedEventArgs e)
        {
            AllatAthelyezeseAblak aaa = new AllatAthelyezeseAblak(vm.kivKetrec);
            if (aaa.ShowDialog() == true)
            {
                telephelyClient.AllatMasikTelephelyre(aaa.Vm.KivAllat, aaa.Vm.KivTelephely, aaa.Vm.KivKetrec);
            }
            ListaFrissites();
        }

#endregion

#region Gondozo-Fül
        private void GondozoFelveteleButton_Click(object sender, RoutedEventArgs e)
        {
            GondozoFelveteleAblak gfa = new GondozoFelveteleAblak();
            if (gfa.ShowDialog() == true)
            {
                gondozoClient.GondozoLetrehozas(gfa.nev_box.Text, (GondozoBeosztas)gfa.BeosztasComboBox.SelectedItem, gfa.jelszo_box.Password, gfa.Vm.KivTelephely);
                ListaFrissites();
            }
            ListaFrissites();
        }

        private void GondozoModositasaButton_Click(object sender, RoutedEventArgs e)
        {
            GondozoModositasAblak gma = new GondozoModositasAblak();
            if (gma.ShowDialog() == true)
            {
                gondozoClient.GondozoModositas(vm.KivGondozo);
                ListaFrissites();
            }
            
            ListaFrissites();
        }

        private void GondozoTorleseButton_Click(object sender, RoutedEventArgs e)
        {
// ----------------- NEM TUTI HOGY OKÉS --------------------
            List<Allat> gondozottAllatok = vm.KivGondozo.GondozottAllatok.ToList();
            bool egyvaneneki = false;

            foreach (Allat allata in gondozottAllatok)
            {
                if(allata.Gondozok.Count() == 1)
                    egyvaneneki = true;
            }

            if (egyvaneneki == true)
                MessageBox.Show("Gondozott állatának nincs több gondozója\nElőbb róla gondoskodjon!", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                MessageBoxResult megerosit = MessageBox.Show("Biztosan törölni szeretné a(z) " + vm.KivGondozo.Nev + " nevű gondozót a rendszerből?\nA művelet nem visszavonható!", "Biztosan törli?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (megerosit == MessageBoxResult.Yes)
                {
                    gondozoClient.GondozoTorles(vm.KivGondozo);
                }
            }

            ListaFrissites();
        }

        private void AllatMOdositasButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllatmodositasAblak aa = new AllatmodositasAblak();
                if (aa.ShowDialog() == true)
                    ListaFrissites();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
        private void MunkahelymodositasaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MunkahelyModositasAblak ma = new MunkahelyModositasAblak();
                if (ma.ShowDialog() == true)
                    ListaFrissites();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

#endregion

        private void KilepesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void ListaFrissites()
        {
            try
            {
                if (vm.Felhasznalo.Beosztas == GondozoBeosztas.Adminisztrátor)
                {
                    vm.Telephelyek = telephelyClient.TelephelyListazas().ToArray();
                    vm.Gondozok = gondozoClient.GondozoListazas().ToArray();
                }
                else
                {
                    List<Telephely > telephelyTempLista = new List<Telephely >();
                    foreach (var telep in vm.Felhasznalo.Munkahelyek )
                    {
                        telephelyTempLista.Add(telephelyClient.TelephelyListazasEgy(telep.Cim).First());
                        
                    }
                    vm.Telephelyek = telephelyTempLista.ToArray();
                 //   vm.Gondozok[0]= vm.Felhasznalo;
                }
                vm.KivKetrec = null;
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Megszakadt a kapcsolat a szerverrel!", "Nincs kapcsolat", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow mw = new MainWindow();
                mw.Show();
            }
        }

        private void btFrissites(object sender, RoutedEventArgs e)
        {
            ListaFrissites();
        }



    }
}
