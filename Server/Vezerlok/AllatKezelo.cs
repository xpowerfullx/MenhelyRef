using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Menhely
{
    public partial class Vezerlo : IAllatKezelo
    {
        // -- MEZŐK --

        // -- KONSTRUKTOR(OK) --


        // -- TULAJDONSÁGOK --


        // -- METÓDUSOK --


        public void AllatMasikKetrecbe(Allat allat, Ketrec hova)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var allatTemp = DB.Allatok.Include(x => x.Ketrec).Where(x => x.Nev == allat.Nev);
                var hovaTemp = DB.Ketrecek.Include(x => x.Allatok).Where(x => x.KetrecID == hova.KetrecID);
                if (allatTemp.Count() != 0 && hovaTemp.Count() != 0 && hovaTemp.First().Allatok.Count < hovaTemp.First().Meret && allatTemp.First().Faj == hovaTemp.First().Faj)
                {
                    hovaTemp.First().AddAllat(allatTemp.First());
                    allatTemp.First().Ketrec.RemoveAllat(allatTemp.First());
                    allatTemp.First().Ketrec = hovaTemp.First();
                    DB.SaveChanges();
                }
            }

            if (hova.UresE() == true && hova.Faj==allat.Faj)
            {
                allat.Ketrec = hova;
            }
        }

        public void AllatModositas(Allat allat, Gondozo gondozo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var allatTemp = DB.Allatok.Include( x => x.Gondozok).Where(x => x.Nev == allat.Nev);
                var gondozoTemp = DB.Gondozok.Where(x => x.Nev == gondozo.Nev);
                if (allatTemp.Count() != 0 && gondozoTemp.Count() != 0 && allatTemp.First().GondozojaE(gondozoTemp.First()))
                {
                    allatTemp.First().AlFaj = allat.AlFaj;
                    allatTemp.First().Kor = allat.Kor;
                    allatTemp.First().Leiras = allat.Leiras;
                    // Többit nem bántjuk, mert máshol kezeljük!
                    DB.SaveChanges();
                }
            }
        }

        public void AllatTorlese(Allat allat, Gondozo gondozo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var allatTemp = DB.Allatok.Include(x => x.Gondozok).Where(x => x.Nev == allat.Nev);
                var gondozoTemp = DB.Gondozok.Where(x => x.Nev == gondozo.Nev);
                if (allatTemp.Count() != 0 && gondozoTemp.Count() != 0 && allatTemp.First().GondozojaE(gondozoTemp.First()))
                {
                    allatTemp.First().Eltavolitas();
                    DB.Allatok.Remove(allatTemp.First());
                    DB.SaveChanges();
                }
            }
        }

        public void AllatGondozas(Allat allat, Gondozo gondozo, string jegyzokonyv)
        {
            using(Menhelyek DB = new Menhelyek())
            {
                var allatTemp = DB.Allatok.Where(x => x.Nev == allat.Nev);
                var gondozoTemp = DB.Gondozok.Where(x => x.Nev == gondozo.Nev);
                if (allatTemp.Count() != 0 && gondozoTemp.Count() != 0 && allatTemp.First().GondozojaE(gondozoTemp.First()))
                {
                    allatTemp.First().Gondozas(jegyzokonyv);
                    DB.SaveChanges();
                }
            }
        }

        public void AllatFelvetel(string nev, string leiras, int kor, AllatFaj faj, string alFaj, Ketrec ketrec, Gondozo gondozo)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                var ketrecTemp = DB.Ketrecek.Include( x => x.Hely).Where(x => x.KetrecID == ketrec.KetrecID);
                var gondozoTemp = DB.Gondozok.Where(x => x.Nev == gondozo.Nev);
                var allatTemp = DB.Allatok.Where(x => x.Nev == nev);
                if (ketrecTemp.Count() != 0 && gondozoTemp.Count() != 0 && allatTemp.Count() == 0 && ketrecTemp.First().Allatok.Count < ketrecTemp.First().Meret)
                {
                    Allat ujAllat = new Allat(nev, leiras, kor, faj, alFaj, ketrecTemp.First(), gondozoTemp.First());

                    DB.Allatok.Add(ujAllat);
                    ketrecTemp.First().AddAllat(ujAllat);
                    gondozoTemp.First().GondozottAllatok.Add(ujAllat);

                    DB.SaveChanges();
                }
            }
        }

        public Allat[] AllatListazas()
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Allatok.Include(x => x.Gondozok).Include(x => x.Ketrec.Hely).Include(x => x.Orokbefogado).ToArray();
            }
        }

        public Allat[] AllatListazasEgy(string nev)
        {
            using (Menhelyek DB = new Menhelyek())
            {
                return DB.Allatok.Where(x => x.Nev == nev).Include(x => x.Gondozok).Include(x => x.Ketrec.Hely).Include(x => x.Orokbefogado).ToArray();
            }
        }
    }
}
