using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Menhely
{
    public partial class Vezerlo : IOrokbefogadasKezelo
    {
        // -- MEZŐK --
        
        // -- KONSTRUKTOR(OK) --

        // -- TULAJDONSÁGOK --

        // -- METÓDUSOK --

        public void KerelemLeadas(Orokbefogado orokbefogado, Allat allat)
        {
            using(Menhelyek DB= new Menhelyek())
            {
                // Módosítottam - Dani
                var orokbefogadoTemp = DB.Orokbefogadok.Where(x=>x.Nev==orokbefogado.Nev).Single();
                var allatTemp = DB.Allatok.Where(x => x.Nev == allat.Nev).Single();
                if (orokbefogadoTemp != null && allatTemp != null && allatTemp.Orokbefogado == null)
                {
                    allatTemp.Lefoglal(orokbefogadoTemp);
                    orokbefogadoTemp.AddOrokbefogadandoAllat(allatTemp);//
                    DB.SaveChanges();
                }
            }
        }

        public void OrokbefogadasElfogadasa(Allat allat)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                // Módosítottam - Dani
                var allatTemp = DB.Allatok.Include(x => x.Orokbefogado).Where(x => x.Nev == allat.Nev).Single();
                if (allatTemp != null)
                {
                    allatTemp.OrokbeAdas();
                    DB.SaveChanges();
                }
            }
        }

        public void OrokbefogadasVisszautasitas(Allat allat)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                // Módosítottam - Dani
                var allatTemp = DB.Allatok.Include(x => x.Orokbefogado ).Where(x => x.Nev == allat.Nev).Single();
                if (allatTemp != null)
                {
                    allat.Orokbefogado = null;
                    allatTemp.SzabaddaTetel();
                    DB.SaveChanges();
                }
            }
        }
    }
}
