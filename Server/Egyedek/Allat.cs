using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Menhely
{
    [DataContract]
    public enum AllatFaj { 
        [EnumMember(Value="Kutya")]
        Kutya,
        [EnumMember(Value = "Macska")]
        Macska,
        [EnumMember(Value = "Róka")]
        Róka,
        [EnumMember(Value = "Süni")]
        Süni,
        [EnumMember(Value = "Egyéb")]
        Egyéb 
    }

    [DataContract]
    public enum OrokbefogadasAllapot {
            
        [EnumMember]
        Szabad,
        [EnumMember]
        Foglalt,
        [EnumMember]
        ÖrökbeAdva
    }

    [DataContract(IsReference = true)]
    public class Allat
    {
        // -- MEZŐK --
        [Key]
        [DataMember]
        public string Nev { get; set; }

        [DataMember]
        public string Leiras { get; set; }

        [DataMember]
        public int Kor { get; set; }

        [DataMember]
        public AllatFaj Faj { get; set; }

        [DataMember]
        public string AlFaj { get; set; }

        [DataMember]
        public Ketrec Ketrec { get; set; }

        [DataMember]
        public List<Gondozo> Gondozok { get; set; }

        [DataMember]
        public List<string> Gondozasok { get; set; }

        [DataMember]
        public OrokbefogadasAllapot Allapot { get; set; }

        [DataMember]
        public Orokbefogado Orokbefogado { get; set; }


        // -- KONSTRUKTOR(OK) --
        public Allat(string nev, string leiras, int kor, AllatFaj faj, string alFaj, Ketrec ketrec, Gondozo gondozo)
        {
            this.Nev = nev;
            this.Leiras = leiras;
            this.Kor = kor;
            this.Faj = faj;
            this.AlFaj = alFaj;
            this.Ketrec = ketrec;
            //this.Ketrec.AddAllat(this);
            this.Gondozok = new List<Gondozo>();
            this.Gondozok.Add(gondozo);
            //gondozo.GondozottAllatok.Add(this);
            this.Gondozasok = new List<string>();

            Allapot = OrokbefogadasAllapot.Szabad;
        }

        public Allat()
        {
            this.Gondozok = new List<Gondozo>();
            this.Gondozasok = new List<string>();

        }

        // -- TULAJDONSÁGOK --
        //public string Nev
        //{
        //    get { return nev; }
        //    set { nev = value; }
        //}
        //public string Leiras
        //{
        //    get { return leiras; }
        //    set { leiras = value; }
        //}
        //public int Kor
        //{
        //    get { return kor; }
        //    set { kor = value; }
        //}
        //public AllatFaj Faj
        //{
        //    get { return faj; }
        //    set { faj = value; }
        //}
        //public string AlFaj
        //{
        //    get { return alFaj; }
        //    set { alFaj = value; }
        //}
        //public Ketrec Ketrec
        //{
        //    get { return ketrec; }
        //    set { ketrec = value; }
        //}
        //public List<Gondozo> Gondozok
        //{
        //    get { return gondozok; }
        //    set { gondozok = value; }
        //}
        //public List<string> Gondozasok
        //{
        //    get { return gondozasok; }
        //    set { gondozasok = value; }
        //}
        //public OrokbefogadasAllapot Allapot
        //{
        //    get { return allapot; }
        //    set { allapot = value; }
        //}
        //public Orokbefogado Orokbefogado
        //{
        //    get { return orokbefogado; }
        //    set { orokbefogado = value; }
        //}

        // -- METÓDUSOK --
        public bool GondozojaE(Gondozo gondozo)
        {
            // A paraméterként kapott gondozó az gondozója-e az állatnak
            // Ha admin, akkor igen!
            if (Gondozok.Contains(gondozo) || gondozo.Beosztas == GondozoBeosztas.Admin)
            {
                return true;
            }
            return false;
        }

        public void Gondozas(string jegyzokonyv)
        {
            // Az állat gondozása
            Gondozasok.Add(jegyzokonyv);
        }

        public void Eltavolitas()
        {
            // Állat törlése a rendszerből
            Ketrec.RemoveAllat(this);
            foreach (Gondozo g in Gondozok)
            {
                g.GondozottAllatok.Remove(this);
            }
            if (Orokbefogado != null)
            {
                Orokbefogado.OrokbeFogadando.Remove(this);
            }
        }

        public void SzabaddaTetel()
        {
            // Az állat örökbeadási állapotának Szabad-á tétele (örökbefogadás elutasítása)
            Allapot = OrokbefogadasAllapot.Szabad;
            Orokbefogado.OrokbeFogadando.Remove(this);
            Orokbefogado = null;
        }

        public void OrokbeAdas()
        {
            // Az állat örökbeadási állapotának ÖrökbeAdvá-vá tétele (örökbeadás elfogadása)
            Allapot = OrokbefogadasAllapot.ÖrökbeAdva;
        }
        public void Lefoglal(Orokbefogado orokbefogado)
        {
            // Az állat örökbeadási állapotának Foglalt-vá tétele (örökbeadási igény benyújtása)
            Allapot = OrokbefogadasAllapot.Foglalt;
            Orokbefogado = orokbefogado;
            orokbefogado.AddOrokbefogadandoAllat(this);
        }
    }
}
