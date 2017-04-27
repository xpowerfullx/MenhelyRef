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

namespace Menhely
{
    /// <summary>
    /// Interaction logic for AllatAthelyezes.xaml
    /// </summary>
    public partial class AllatAthelyezesAblak : Window
    {
        Allat allat;
        List<Ketrec> ketrecek;
        public Ketrec KivalasztottKetrec { get; private set; }

        public AllatAthelyezesAblak(Allat allat)
        {
            InitializeComponent();
            this.allat = allat;
            ketrecek = new List<Ketrec>();
            TelephelyKezeloClient telephelyClient = new TelephelyKezeloClient();
            foreach (var ketrec in telephelyClient.TelephelyListazasEgy(allat.Ketrec.Hely.Cim).First().Ketrecek)
            {
                Ketrec k = telephelyClient.KetrecListazasEgy(ketrec.KetrecID).First();
                if (k.Faj == allat.Faj && k.Allatok.Count() < k.Meret)
	            {
                    ketrecek.Add(k);
                    ketreclistaCB.Items.Add("ID: " + k.KetrecID + ", Férőhely: " + k.Allatok.Count() + "/" + k.Meret);
	            }
            }
        }

        private void btAthelyez(object sender, RoutedEventArgs e)
        {
            if (ketreclistaCB.SelectedIndex > -1)
            {
                KivalasztottKetrec = ketrecek[ketreclistaCB.SelectedIndex];
                this.DialogResult = true;
            }

        }

        private void btMegse(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
