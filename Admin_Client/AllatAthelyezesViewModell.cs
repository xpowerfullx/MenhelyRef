using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin_Client;
using Admin_Client.MenhelyServiceReference;

namespace Admin_Client
{
    class AllatAthelyezesViewModell : Binding
    {
        Ketrec[] ketrecek;
        Ketrec jelKetrec;
        Ketrec kivKetrec;
        Telephely[] telephelyek;
        Telephely kivTelephely;
        Allat kivAllat;

        public AllatAthelyezesViewModell(Ketrec jelKetrec)
        {
            this.jelKetrec = jelKetrec;
            ketrecek = new Ketrec[0];
            telephelyek = new Telephely[0];
        }


// -------- TULAJDONSÁGOK -----------------------

        public Ketrec[] Ketrecek
        {
            get { return ketrecek; }
            set { ketrecek = value; OnChange(); }
        }
        
        public Ketrec JelKetrec
        {
            get { return jelKetrec; }
            set { jelKetrec = value; OnChange(); }
        }
        
        public Ketrec KivKetrec
        {
            get { return kivKetrec; }
            set { kivKetrec = value; OnChange(); }
        }

        public Telephely[] Telephelyek
        {
            get { return telephelyek; }
            set { telephelyek = value; OnChange(); }
        }
 
        public Telephely KivTelephely
        {
            get { return kivTelephely; }
            set { kivTelephely = value; OnChange(); }
        }
        public Allat KivAllat
        {
            get { return kivAllat; }
            set { kivAllat = value; OnChange(); }
        }
    }
}
