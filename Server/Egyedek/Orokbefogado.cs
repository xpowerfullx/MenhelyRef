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
    [DataContract(IsReference = true)]
    public class Orokbefogado : IBejelentkezhet
    {
        // -- MEZŐK --
        [Key]
        [DataMember]
        public string Nev { get; set; }

        [DataMember]
        public string Jelszo { get; set; }

        [DataMember]
        public int Adomany { get; set; }

        [DataMember]
        public  List<Allat> OrokbeFogadando { get; set; }

        [DataMember]
        public DateTime UtolsoCselekves { get; set; }

        // -- KONSTRUKTOR(OK) --
        public Orokbefogado(string nev, string jelszo)
        {
            this.Nev = nev;
            this.Jelszo = jelszo;
            this.OrokbeFogadando = new List<Allat>();
            UtolsoCselekves = DateTime.Now;
            Adomany = 0;
        }
        public Orokbefogado()
        {
            Adomany = 0;
            this.OrokbeFogadando = new List<Allat>();
        }


        // -- TULAJDONSÁGOK --

        public bool Bejelentkezhet
        {
            get;
            set;
        }


        // -- METÓDUSOK --
        public void AdomanyHozzaadas(int osszeg) 
        {
            // A kapott adomány hozzáírása
            Adomany += osszeg;
        }
        public void AddOrokbefogadandoAllat(Allat allat)
        {
            // Örökbefogadandó állat hozzáadása a listához
            OrokbeFogadando.Add(allat);
        }


    }
}
