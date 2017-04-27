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
    public class Telephely
    {
        // -- MEZŐK --
        [Key]
        [DataMember]
        public string Cim { get; set; }

        [DataMember]
        public  List<Gondozo> Dolgozok { get; set; }

        [DataMember]
        public  List<Ketrec> Ketrecek { get; set; }


        // -- KONSTRUKTOR(OK) --
        public Telephely(string cim)
        {
            this.Cim = cim;
            this.Dolgozok = new List<Gondozo>();
            this.Ketrecek = new List<Ketrec>();
        }
        public Telephely()
        {
            this.Dolgozok = new List<Gondozo>();
            this.Ketrecek = new List<Ketrec>();
        }


        // -- TULAJDONSÁGOK --
        //public string Cim
        //{
        //    get { return cim; }
        //    set { this.cim = value; }
        //}

        //public List<Gondozo> Dolgozok
        //{
        //    get { return dolgozok; }
        //    set { dolgozok = value; }
        //}

        //public List<Ketrec> Ketrecek
        //{
        //    get { return ketrecek; }
        //    set { ketrecek = value; }
        //}


        // -- METÓDUSOK --
        public void AddGondozo(Gondozo gondozo)
        {
            // Gondozó hozzáadása a telephelyhez
            Dolgozok.Add(gondozo);
        }
        //void
        public void RemoveGondozo(Gondozo gondozo)
        {
            // Gondozó levétele a telephelyről
            Dolgozok.Remove(gondozo);
        }

        public void AddKetrec(Ketrec ketrec)
        {
            Ketrecek.Add(ketrec);
        }

        public void SetKetrec(Ketrec ketrec)
        {
            // Ketrec módosítása
        }

        public void RemoveKetrec(Ketrec ketrec)
        {
            // Ketrec törlése
            Ketrecek.Remove(ketrec);
        }

        public bool UresETelephely()
        {
            // Igaz, ha a telephelyen már nem dolgozik senki és nincsenek ketrecek
            if (Dolgozok.Count == 0 && Ketrecek.Count == 0)
            {
                return true;
            }
            return false;
        }

    }
}
