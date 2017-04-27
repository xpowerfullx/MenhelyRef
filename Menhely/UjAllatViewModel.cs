using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Menhely.MenhelyServiceReference;

namespace Menhely
{
    class UjAllatViewModel : Binding
    {
        private Telephely kivTelephely;
        private Telephely[] telephelyek;

        private Ketrec kivKetrec;
        private Ketrec[] ketrecek;

        private Gondozo kivGondozo;
        private Gondozo[] gondozok;

        public UjAllatViewModel()
        {
            telephelyek = new Telephely[0];
            ketrecek = new Ketrec[0];
            gondozok = new Gondozo[0];
        }

        public Telephely KivTelephely
        {
            get { return kivTelephely; }
            set { kivTelephely = value; OnChange(); }
        }

        public Telephely[] Telephelyek
        {
            get { return telephelyek; }
            set { telephelyek = value; OnChange(); }
        }

        public Ketrec KivKetrec
        {
            get { return kivKetrec; }
            set { kivKetrec = value; OnChange(); }
        }

        public Ketrec[] Ketrecek
        {
            get { return ketrecek; }
            set { ketrecek = value; OnChange(); }
        }

        public Gondozo KivGondozo
        {
            get { return kivGondozo; }
            set { kivGondozo = value; OnChange(); }
        }

        public Gondozo[] Gondozok
        {
            get { return gondozok; }
            set { gondozok = value; OnChange(); }
        }
        
        
    }
}
