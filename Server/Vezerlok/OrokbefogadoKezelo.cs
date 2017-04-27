using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Menhely
{
    public partial class Vezerlo : IOrokbefogadoKezelo
    {
        // -- MEZŐK --
        
        // -- KONSTRUKTOR(OK) --
        
        // -- TULAJDONSÁGOK --
        
        // -- METÓDUSOK --
        public Orokbefogado[] OrokbefogadoListazas()
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Orokbefogadok.Include(x => x.OrokbeFogadando).ToArray();
            }
        }

        public Orokbefogado[] OrokbefogadoListazasEgy(string nev)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Orokbefogadok.Where(x => x.Nev == nev).Include(x => x.OrokbeFogadando).ToArray();
            }
        }

        // bővítettem a paraméterlistát Orokbefogado orokbefogado -val,
        // hogy tudjuk, kihez kell hozzáadni az adományt
        public void Adomanyozas(Orokbefogado orokbefogado, int adomanyOsszeg)
        {
            using(Menhelyek DB= new Menhelyek())
            {
                var q = DB.Orokbefogadok.Where(x => x.Nev == orokbefogado.Nev);
                q.Single().AdomanyHozzaadas(adomanyOsszeg);
                DB.SaveChanges();
            }
        }


        public Orokbefogado Bejelentkezes(string nev, string jelszo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var orokbefogadoTemp = DB.Orokbefogadok.Where(x => x.Nev == nev && x.Jelszo == jelszo);
                if (orokbefogadoTemp.Count() ==0)
                {
                    return null;
                }
                else
                {
                    return orokbefogadoTemp.Single();
                }
            }
        }

        public bool BejelentkezesEllenorzesOrokbefogado(Orokbefogado orokbefogado)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var q = DB.Orokbefogadok.Where(x => x.Nev == orokbefogado.Nev);
                //ha az elmúlt 10 percben volt aktivitása, akkor true, egyébként false
                DateTime d = q.Single().UtolsoCselekves.Add(new TimeSpan(0, 10, 0));

                if (d < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    // utolsó cselekvéskor a dátum frissítése
                    q.Single().UtolsoCselekves = DateTime.Now;
                    DB.SaveChanges();
                    return true;
                }
            }
        }


        //átírtam a void-ot bool-ra, mert jelezni kell, hogy sikeres-e a reg vagy sem 

        public bool Regisztracio(string nev, string jelszo)
        {
            using (Menhelyek DB = new Menhelyek())
            {

                var orokbefogadoTemp = DB.Orokbefogadok.Where(x => x.Nev == nev);
                if (orokbefogadoTemp.Count() != 0)
                {
                    return false;
                }
                else
                {
                    Orokbefogado uj = new Orokbefogado(nev, jelszo);
                    uj.Nev = nev;
                    uj.Jelszo = jelszo;
                    uj.Adomany = 0;
                    uj.UtolsoCselekves = DateTime.Now;
                    //uj.Bejelentkezhet = false;
                    DB.Orokbefogadok.Add(uj);

                    DB.SaveChanges();
                }
            }
            return true;
        }
    }
}

