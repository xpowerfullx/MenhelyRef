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

using Orokbefogado_Client.MenhelyServiceReference;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Orokbefogado_Client
{
    /// <summary>
    /// Interaction logic for FoAblak.xaml
    /// </summary>
    public partial class FoAblak : Window
    {
        AllatKezeloClient allatClient;
        OrokbefogadasKezeloClient orokbefogadasClient;
        OrokbefogadoKezeloClient orokbefogadoClient;
        ViewModel VM;

        public FoAblak(Orokbefogado belepett)
        {
            InitializeComponent();
            VM = new ViewModel();
            this.DataContext = VM;
            orokbefogadasClient = new OrokbefogadasKezeloClient();
            allatClient = new AllatKezeloClient();
            orokbefogadoClient = new OrokbefogadoKezeloClient();
            VM.Felhasznalo = belepett;
            ListaFrissites();
            if (belepett != null)
            {
                KerelemListaFrissites();
                ElfogadottAllatListaFrissit();
            }
            }

        void KerelemListaFrissites()
        {
            VM.OrokbefogadandoList = allatClient.AllatListazas().Where(x => x.Orokbefogado != null && x.Orokbefogado.Nev == VM.Felhasznalo.Nev && x.Allapot!=OrokbefogadasAllapot.ÖrökbeAdva).ToArray();
        }

        void ListaFrissites()
        {
            try
            {
                //VM.Allatok = allatClient.AllatListazas().ToArray();
                VM.Allatok = allatClient.AllatListazas().Where(x => x.Allapot == OrokbefogadasAllapot.Szabad).ToArray();
            }
            catch (Exception)
            {
                MessageBox.Show("Megszakadt a kapcsolat a szerverrel!", "Nincs kapcsolat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_adatokMegtekintese_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridAllatlistazas.SelectedItem != null)
                {
                    var allat = datagridAllatlistazas.SelectedItem;
                    VM.Kivalasztott = (Allat)allat;
                    AllatAdatWindow w = new AllatAdatWindow(VM.Kivalasztott);
                    w.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Kérem válasszon ki egy állatot!");
                }
                }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.Felhasznalo = null;
            VM.Kivalasztott = null;

            MainWindow m = new MainWindow(); 
            this.Close();
            m.Show();
        }
        private void button_kerelembenyujtas_click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.Kivalasztott = (Allat)datagridAllatlistazas.SelectedItem;
                if ((VM.Kivalasztott != null))
                {
                    if (VM.Kivalasztott.Orokbefogado == null)
                    {
                        orokbefogadasClient.KerelemLeadas(VM.Felhasznalo, VM.Kivalasztott);
                        MessageBox.Show("Sikeres kérelem benyújtás!");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Már benyújtott kérelmet a {0} nevű állatra.", VM.Kivalasztott.Nev));
                    }
                }
                else
                {
                    MessageBox.Show("Nincs kiválasztva egyetlen elem sem.");
                }
                KerelemListaFrissites();
                ListaFrissites();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = textbox_bankknev.Text;
                if (textbox_bankknev.Text != null)
                {
                    s = "";
                    foreach (var item in s)
                    {
                        if (item != ' ')
                        {
                            s += item;
                        }
                    }
                }
                if (s!=null &&
                    textbox_bankkszam.Text!=null &&
                    textbox_osszeg.Text!=null &&
                    s.All(char.IsLetter) &&
                    (textbox_bankkszam.Text.Count() == 16 ||
                    textbox_bankkszam.Text.Count() == 24 ) &&
                    textbox_bankkszam.Text.All(char.IsDigit) &&
                    textbox_osszeg.Text.All(char.IsDigit))
                {
                    InputBox.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Hiba a megadott adatok között!", "Hibaüzenet");
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                String input = InputTextBox.Text;
                orokbefogadoClient.Adomanyozas(VM.Felhasznalo, int.Parse(textbox_osszeg.Text));
                MessageBox.Show("Köszönjük a támogatást! Állataink hálásak érte!");
                textbox_bankknev.Text = string.Empty;
                textbox_bankkszam.Text = string.Empty;
                textbox_osszeg.Text = string.Empty;
                VM.Felhasznalo.Adomany = orokbefogadoClient.OrokbefogadoListazasEgy(VM.Felhasznalo.Nev).Single().Adomany;
                InputTextBox.Text = String.Empty;
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                textbox_bankknev.Text = string.Empty;
                textbox_bankkszam.Text = string.Empty;
                textbox_osszeg.Text = string.Empty;
                InputTextBox.Text = String.Empty;
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_ListaFrissit_allatok(object sender, RoutedEventArgs e)
        {
            try
            {
                ListaFrissites();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }

        private void button_listakfrissitese(object sender, RoutedEventArgs e)
        {
            try
            {
                KerelemListaFrissites();
                VM.ElfogadottAllatList = allatClient.AllatListazas().Where(x => x.Orokbefogado != null && x.Orokbefogado.Nev == VM.Felhasznalo.Nev && x.Allapot == OrokbefogadasAllapot.ÖrökbeAdva).ToArray();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ElfogadottAllatListaFrissit()
        {
            try
            {
                VM.ElfogadottAllatList = allatClient.AllatListazas().Where(x => x.Orokbefogado != null && x.Orokbefogado.Nev == VM.Felhasznalo.Nev && x.Allapot == OrokbefogadasAllapot.ÖrökbeAdva).ToArray();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_KerelemTorles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagrid_kerelmek.SelectedItem != null)
                {
                    orokbefogadasClient.OrokbefogadasVisszautasitas((Allat)datagrid_kerelmek.SelectedItem);
                    KerelemListaFrissites();
                    ListaFrissites();
                }
                else
                {
                    MessageBox.Show("Kérem válassza ki a törlendő kérelmet!");
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("A program nem tudott kapcsolatot kezdeményezni a szerverrel.\n Próbálja meg később!\n\nAmennyiben ez a hiba többször jelentkezik, keresse fel a rendszergazdát!", "Nincs kapcsolat a szerverrel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    
    }
}
