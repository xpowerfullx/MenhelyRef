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
    /// Interaction logic for GondozoAblak.xaml
    /// </summary>
    public partial class GondozoAblak : Window
    {
        FoViewModel vm;
        public bool Valtozas { get; private set; }
        public GondozoAblak(FoViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.DataContext = vm.Felhasznalo;
        }

        private void btOK(object sender, RoutedEventArgs e)
        {
            if (jelszoRegiPB.Password == vm.Felhasznalo.Jelszo)
            {
                if (jelszoUjPB.Password != "")
                {
                    if (jelszoUjPB.Password == jelszoUj2PB.Password)
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Az új jelszavak nem egyeznek!", "Hibás jelszó", MessageBoxButton.OK, MessageBoxImage.Error);
                        jelszoRegiPB.Password = "";
                        jelszoUjPB.Password = "";
                        jelszoUj2PB.Password = "";
                    }
                }
                else
                {
                    MessageBox.Show("Kötelező jelszavat megadni!", "Hibás jelszó", MessageBoxButton.OK, MessageBoxImage.Error);
                    jelszoRegiPB.Password = "";
                    jelszoUjPB.Password = "";
                    jelszoUj2PB.Password = "";
                }
                
            }
            else
            {
                MessageBox.Show("A jelenlegi jelszó helytelen!","Hibás jelszó",MessageBoxButton.OK,MessageBoxImage.Error);
                jelszoRegiPB.Password = "";
                jelszoUjPB.Password = "";
                jelszoUj2PB.Password = "";
            }
        }

        private void btMegse(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
