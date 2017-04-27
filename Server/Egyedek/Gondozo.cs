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
    public enum GondozoBeosztas 
    {
        [EnumMember(Value = "Adminisztrátor")]
        Admin,
        [EnumMember(Value = "Gondozó")]
        Gondozo,
        [EnumMember(Value = "Befogó")]
        Befogo 
    }

    [DataContract(IsReference = true)]
    public class Gondozo : IBejelentkezhet
    {
        // -- MEZŐK --
        [Key]
        [DataMember]
        public string Nev { get; set; }

        [DataMember]
        public string Jelszo { get; set; }

        [DataMember]
        public GondozoBeosztas Beosztas { get; set; }

        [DataMember]
        public  List<Telephely> Munkahelyek { get; set; }

        [DataMember]
        public  List<Allat> GondozottAllatok { get; set; }

        [DataMember]
        public DateTime UtolsoCselekves { get; set; }


        // -- KONSTRUKTOR(OK) --
        public Gondozo(string nev, GondozoBeosztas beosztas, string jelszo, Telephely munkahely)
        {
            this.Nev = nev;
            this.Beosztas = beosztas;
            this.Jelszo = jelszo;
            Munkahelyek = new List<Telephely>();
            this.Munkahelyek.Add(munkahely);
            UtolsoCselekves = DateTime.Now;
            GondozottAllatok = new List<Allat>();
        }

        public Gondozo()
        {
            Munkahelyek = new List<Telephely>();
            UtolsoCselekves = DateTime.Now;
            GondozottAllatok = new List<Allat>();

        }


        // -- TULAJDONSÁGOK --
        
        public bool Bejelentkezhet
        {
            get;
            set;
        }


        // -- METÓDUSOK --
        public void Eltavolitas()
        {
            // Gondozó eltávolítása a rendszerből
            foreach (var item in Munkahelyek)
            {
                item.RemoveGondozo(this);
            }
            foreach (var item in GondozottAllatok)
            {
                item.Gondozok.Remove(this);
            }
            
        }


    }
}
