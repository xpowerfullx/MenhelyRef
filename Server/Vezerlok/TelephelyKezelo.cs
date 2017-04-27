using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Menhely
{
    public partial class Vezerlo : ITelephelyKezelo
    {
        // -- MEZŐK --


        // -- KONSTRUKTOR(OK) --

        // -- TULAJDONSÁGOK --


        // -- METÓDUSOK --

        //public void TelephelyModositas(Telephely telephely)
        //{
        //    //  NINCS ÉRTELME, TÖRÖLNI KELL
        //    // throw new NotImplementedException();
        //}

        public void TelephelyFelvetel(string cim)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var q = DB.Telephelyek.Where(x => x.Cim == cim);
                if (q.Count() == 0)
                {
                    Telephely ujTelephely = new Telephely(cim);
                    DB.Telephelyek.Add(ujTelephely);
                    DB.SaveChanges();
                }
            }
        }

        public void KetrecHozzaadas(Telephely telephely, int ketrecMeret, AllatFaj faj)
        {
            using(Menhelyek DB = new Menhelyek())
            {
                var telephelyTemp = DB.Telephelyek.Where(x => x.Cim == telephely.Cim).Single();
                if (telephelyTemp != null)
                {
                    //Ketrec ujKetrec = new Ketrec();
                    //ujKetrec.KetrecID = Ketrec.kovetkezoAzon++;
                    //ujKetrec.Meret = ketrecMeret;
                    //ujKetrec.Faj = faj;
                    //ujKetrec.Hely = telephelyTemp;

                    Ketrec ujKetrec = new Ketrec(ketrecMeret,faj,telephelyTemp);

                    DB.Ketrecek.Add(ujKetrec);
                    //telephelyTemp.AddKetrec(ujKetrec);

                    DB.SaveChanges();
                }
            }
        }

        public void KetrecTorles(Telephely telephely, Ketrec ketrec)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var telephelyTemp = DB.Telephelyek.Where(x => x.Cim == telephely.Cim).Single();
                var KetrecTemp = DB.Ketrecek.Where(x => x.KetrecID == ketrec.KetrecID).Single();
                if (telephelyTemp != null && KetrecTemp != null && KetrecTemp.UresE())
                {
                    telephelyTemp.RemoveKetrec(KetrecTemp);
                    DB.Ketrecek.Remove(KetrecTemp);
                    DB.SaveChanges();
                }
            }
        }

        public void TelephelyTorles(Telephely telephely)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var telephelyTemp = DB.Telephelyek.Where(x => x.Cim == telephely.Cim).Single();
                if (telephelyTemp != null && telephelyTemp.UresETelephely())
                {
                    DB.Telephelyek.Remove(telephelyTemp);
                    DB.SaveChanges();
                }
            }
        }


        public void KetrecModositas(Ketrec ketrec)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var KetrecTemp = DB.Ketrecek.Include(x => x.Allatok).Where(x => x.KetrecID == ketrec.KetrecID).Single();
                if (KetrecTemp != null && KetrecTemp.UresE())
                {
                    KetrecTemp.Faj = ketrec.Faj;
                    KetrecTemp.Meret = ketrec.Meret;
                    DB.SaveChanges();
                }
            }
        }

        public void AllatMasikTelephelyre(Allat allat, Telephely hovaTelep, Ketrec hovaKetrec)
        {
            // -- TESZTELNI!!! --
            using (Menhelyek DB = new Menhelyek())
            {
                var telephelyHovaTemp = DB.Telephelyek.Where(x => x.Cim == hovaTelep.Cim).Single();
                var ketrecHovaTemp = DB.Ketrecek.Where(x => x.KetrecID == hovaKetrec.KetrecID).Single();
                var allatTemp = DB.Allatok.Where(x => x.Nev == allat.Nev).Single();
                if (telephelyHovaTemp != null && ketrecHovaTemp != null && allatTemp != null && ketrecHovaTemp.Allatok.Count < ketrecHovaTemp.Meret && ketrecHovaTemp.Faj == allatTemp.Faj)
                {
                    hovaKetrec.AddAllat(allatTemp);
                    //allatTemp.Ketrec.RemoveAllat(allatTemp);
                    allatTemp.Ketrec = ketrecHovaTemp;
                    DB.SaveChanges();
                }
            }
        }

        public Telephely[] TelephelyListazas()
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Telephelyek.Include(x => x.Ketrecek).Include(x => x.Dolgozok).ToArray();
                //return DB.Telephelyek.ToArray();
            }
        }

        public Telephely[] TelephelyListazasEgy(string cim)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Telephelyek.Where(x=> x.Cim == cim).Include(x => x.Ketrecek).Include(x => x.Dolgozok).ToArray();
                //return DB.Telephelyek.ToArray();
            }
        }

        public Ketrec[] KetrecListazas()
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Ketrecek.Include(x => x.Allatok).Include(x => x.Hely).ToArray();
            }
        }

        public Ketrec[] KetrecListazasEgy(int id)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Ketrecek.Where(x => x.KetrecID == id).Include(x => x.Allatok).Include(x => x.Hely).ToArray();
            }
        }
        
    }
}
