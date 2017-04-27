using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menhely.MenhelyServiceReference;

namespace Menhely
{
    public class FoViewModel : Binding
    {
        Allat[] allatok;
        Gondozo felhasznalo;
        Allat kivalsztottAllat;
        bool zaroltAblak;

        public bool ZaroltAblak
        {
            get { return zaroltAblak; }
            set { zaroltAblak = value; OnChange(); }
        }

        public Allat KivalsztottAllat
        {
            get { return kivalsztottAllat; }
            set { kivalsztottAllat = value; OnChange(); OnChange("Lock"); }
        }
        
        public FoViewModel()
        {
            allatok = new Allat[0];
        }

        public bool Lock
        {
            get
            {
                if (kivalsztottAllat != null)
                {
                    return true;
                }
                return false;
            }
        }

        public Allat[] Allatok
        {
            get
            {
                return allatok;
            }
            set
            {
                this.allatok = value;
                OnChange();
            }
        }

        public Gondozo Felhasznalo
        {
            get
            {
                return felhasznalo;
            }
            set
            {
                this.felhasznalo = value;
                OnChange();
            }
        }

    }
}
