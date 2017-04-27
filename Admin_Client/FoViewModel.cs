using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin_Client.MenhelyServiceReference;

namespace Admin_Client
{
    public class FoViewModel : Binding
    {
        static FoViewModel vm;
        Gondozo[] gondozok;
        Gondozo felhasznalo;
        
        Telephely[] telephelyek;   
        Telephely kivTelephely;
        List<Allat> allatok;
        Allat kivAllat;
        
        Gondozo kivGondozo;

        Ketrec[] ketrecek;
        TelephelyKezeloClient telephelyKliens;

        public Ketrec kivKetrec;

        bool zaroltAblak;
        public Telephely[] Telephelyek { get { return telephelyek; } set { telephelyek = value; OnChange(); } }
        public Gondozo[] Gondozok
        {
            get
            {
                return gondozok;
            }

            set
            {
                gondozok = value; OnChange();
            }

        }
        public bool ZaroltAblak
        {
            get { return zaroltAblak; }
            set { zaroltAblak = value; OnChange(); }
        }

        //public Allat KivalsztottAllat
        //{
        //    get { return kivalsztottAllat; }
        //    set { kivalsztottAllat = value; OnChange(); OnChange("Lock"); }
        //}
        
         FoViewModel()
        {
            gondozok = new Gondozo[0];
            allatok = new List<Allat>();
            telephelyKliens = new TelephelyKezeloClient();
        }

        public bool LockTelephely
        {
            get
            {
                if (kivTelephely != null)
                {
                    return true;
                }
                return false;
            }
        }

        public bool LockKetrec
        {
            get
            {
                if (kivKetrec != null)
                {
                    return true;
                }
                return false;
            }
        }
        //public bool LockAllat
        //{
        //    get
        //    {
        //        if (vm.KivGondozo != null)
        //            return true;
                
        //        return false;
        //    }
        //}

        public List<Allat> Allatok
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
        
        public Telephely KivTelephely 
        { 
            get 
            { 
                return kivTelephely; 
            } 
            set 
            { 
                kivTelephely = value;
                if (kivTelephely != null)
                    Ketrecek = telephelyKliens.KetrecListazas().Where(x => x.Hely.Cim == kivTelephely.Cim).ToArray();
                else
                    Ketrecek = null;
                OnChange(); OnChange("LockTelephely"); //OnChange("Ketrecek");
            } 
        }
   
       

        // TULAJDONSÁGOK 
        #region - Tulajdoságok
        public Ketrec KivKetrec
        {
            get
            {
                return kivKetrec;
            }
            set
            {
                kivKetrec = value; OnChange(); OnChange("LockKetrec");
            }
        }

        public Gondozo KivGondozo
        {
            get
            {
                return kivGondozo;
            }

            set
            {
                kivGondozo = value; OnChange();
            }
        }

        public Ketrec[] Ketrecek
        {
            get { return ketrecek; }
            set { ketrecek = value; OnChange(); }
        }

        public Allat KivAllat
        {
            get
            {
                return kivAllat;
            }

            set
            {
                kivAllat = value;
                OnChange();
            }
        }


        #endregion
        static   public FoViewModel GetVM()
        {
            if (vm == null)
                vm = new FoViewModel();
            return vm;
               
        }
    }
}
