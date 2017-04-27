using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orokbefogado_Client.MenhelyServiceReference;


namespace Orokbefogado_Client
{
    class ViewModel:Bindable
    {
        Allat[] allatok;
        Allat kivalasztott;
        Allat[] orokbefogadandoList;

        public Allat[] OrokbefogadandoList
        {
            get { return orokbefogadandoList; }
            set { orokbefogadandoList = value; OnPropertyChanged(); }
        }
        private Allat[] elfogadottAllatList;

        public Allat[] ElfogadottAllatList
        {
            get { return elfogadottAllatList; }
            set { elfogadottAllatList = value; OnPropertyChanged(); }
        }

        public Allat Kivalasztott
        {
            get { return kivalasztott; }
            set { kivalasztott = value; OnPropertyChanged(); }
        }
        public Allat[] Allatok
        {
            get { return allatok; }
            set { this.allatok = value;
            OnPropertyChanged();
            }
        }
        Orokbefogado felhasznalo;
        
        public Orokbefogado Felhasznalo
        {
            get { return felhasznalo; }
            set { felhasznalo = value; OnPropertyChanged(); }
        }
        public ViewModel()
        {
            allatok = new Allat[0];
            orokbefogadandoList=new Allat[0];
            
        }

    }
}
