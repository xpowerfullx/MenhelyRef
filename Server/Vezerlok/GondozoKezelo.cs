using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace Menhely
{
    public partial class Vezerlo : IGondozoKezelo
    {
        // -- KONSTRUKTOR(OK) --

        // -- TULAJDONSÁGOK --

        // -- METÓDUSOK --
        public Gondozo Bejelenkezes(string nev, string jelszo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var gondozoTemp = DB.Gondozok.Where(x => x.Nev == nev && x.Jelszo == jelszo);
                if (gondozoTemp.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return gondozoTemp.Single();
                }
            }
        }

        public Gondozo[] GondozoListazas()
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Gondozok.Include(x => x.GondozottAllatok).Include(x => x.Munkahelyek).ToArray();
            }
        }

        public Gondozo[] GondozoListazasEgy(string nev)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Gondozok.Where(x => x.Nev == nev).Include(x => x.GondozottAllatok).Include(x => x.Munkahelyek).ToArray();
            }
        }

        public void GondozoTorles(Gondozo gondozo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                try
                {
                    var q = DB.Gondozok.Where(x => x.Nev == gondozo.Nev).Single();
                   /* var telephelyTemp = DB.Telephelyek.Where(x => x.Dolgozok.All(y=>y.Nev==q.Nev));
                    foreach (var item in telephelyTemp)
                    {
                        item.RemoveGondozo(q);
                    }
                    DB.Gondozok.Remove(q);*/
                    q.Eltavolitas();
                    DB.Gondozok.Remove(q);
                    DB.SaveChanges();

                }
                catch(Exception)
                {
                }
            }
        }

        public void GondozoModositas(Gondozo gondozo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var adatbazisGondozo = DB.Gondozok.Where(x => x.Nev == gondozo.Nev).Single();
                if (adatbazisGondozo != null)
                {
                    adatbazisGondozo.Jelszo = gondozo.Jelszo;
                    adatbazisGondozo.Beosztas = gondozo.Beosztas;
                    DB.SaveChanges();
                }
            }
        }

        public void GondozottAllatHozzaadas(Gondozo gondozo, Allat allat)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var adatbazisGondozo = DB.Gondozok.Include(x => x.GondozottAllatok).Where(x => x.Nev == gondozo.Nev).Single();
                var adatbazisAllat = DB.Allatok.Include(x => x.Gondozok).Where(x => x.Nev == allat.Nev).Single();
                if (adatbazisGondozo != null && adatbazisAllat != null)
                {
                    adatbazisGondozo.GondozottAllatok.Add(adatbazisAllat);
                    //adatbazisAllat.Gondozok.Add(adatbazisGondozo);
                    //DB.Entry(adatbazisAllat).State = EntityState.Added;
                    DB.SaveChanges();
                }
            }
        }

        public void TelephelyGondozohozAdas(Gondozo gondozo, Telephely telephely)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var adatbazisGondozo = DB.Gondozok.Where(x => x.Nev == gondozo.Nev).Single();
                var adatbazisTelephely = DB.Telephelyek.Where(x => x.Cim == telephely.Cim).Single();
                if (adatbazisGondozo != null && adatbazisTelephely != null)
                {
                    adatbazisGondozo.Munkahelyek.Add(adatbazisTelephely);
                    adatbazisTelephely.Dolgozok.Add(adatbazisGondozo);
                    DB.SaveChanges();
                }
            }
        }

        public void GondozoLetrehozas(string nev, GondozoBeosztas beosztas, string jelszo, Telephely munkahely)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                
                var munkahelyTemp = DB.Telephelyek.Where(x => x.Cim == munkahely.Cim);
                var gondozoTemp = DB.Gondozok.Where(x => x.Nev == nev);
                if (gondozoTemp.Count() == 0 && munkahelyTemp.Count() != 0)
                {
                    Gondozo ujGondozo = new Gondozo(nev, beosztas, jelszo, munkahelyTemp.First());

                    DB.Gondozok.Add(ujGondozo);
                    munkahelyTemp.First().AddGondozo(ujGondozo);

                    DB.SaveChanges();
                }
                
                /*
                try
                {
                    Gondozo uj = new Gondozo();
                    uj.Nev = nev;
                    uj.Beosztas = beosztas;
                    uj.Jelszo = jelszo;
                    var q = DB.Telephelyek.Where(x => x.Cim == munkahely.Cim).Single();

                    uj.Munkahelyek.Add(q);
                    munkahely.AddGondozo(uj);

                    DB.Gondozok.Add(uj);
                    DB.SaveChanges();

                }
                catch (Exception x)
                {
                    Console.WriteLine("Hiba a gondozó felvételekor!" + x.Message);
                }
                */
            }
        }

        public bool BejelentkezesEllenorzesGondozo(Gondozo gondozo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var q = DB.Gondozok.Where(x => x.Nev == gondozo.Nev);
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

        public void GondozottAllatEltavolitas(Gondozo gondozo, Allat allat)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var adatbazisGondozo = DB.Gondozok.Include(x => x.GondozottAllatok).Where(x => x.Nev == gondozo.Nev).Single();
                var adatbazisAllat = DB.Allatok.Include(x => x.Gondozok).Where(x => x.Nev == allat.Nev).Single();
                if (adatbazisGondozo != null && adatbazisAllat != null && adatbazisAllat.Gondozok.Count > 1)
                {
                    adatbazisGondozo.GondozottAllatok.Remove(adatbazisAllat);
                    adatbazisAllat.Gondozok.Remove(adatbazisGondozo);
                    DB.SaveChanges();
                }
            }
        }

        public void TelephelyGondozotolLevetel(Gondozo gondozo, Telephely telephely)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var adatbazisGondozo = DB.Gondozok.Include(x => x.Munkahelyek).Where(x => x.Nev == gondozo.Nev).Single();
                var adatbazisTelephely = DB.Telephelyek.Include(x => x.Dolgozok).Where(x => x.Cim == telephely.Cim).Single();
                if (adatbazisGondozo != null && adatbazisTelephely != null && adatbazisGondozo.Munkahelyek.Count > 1)
                {
                    adatbazisGondozo.Munkahelyek.Remove(adatbazisTelephely);
                    adatbazisTelephely.Dolgozok.Remove(adatbazisGondozo);
                    DB.SaveChanges();
                }
            }
        }
    }
}
