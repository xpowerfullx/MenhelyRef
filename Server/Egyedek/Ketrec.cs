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
    public class Ketrec
    {
        // -- MEZŐK --
        [Key]
        [DataMember]
        public int KetrecID { get; set; }

        [DataMember]
        public int Meret { get; set; }

        [DataMember]
        public List<Allat> Allatok { get; set; }

        [DataMember]
        public  Telephely Hely { get; set; }

        [DataMember]
        public AllatFaj Faj { get; set; }



        // -- KONSTRUKTOR(OK) --
        public Ketrec(int meret, AllatFaj faj, Telephely hely)
        {
            this.Meret = meret;
            this.Faj = faj;
            this.Hely = hely;
            this.Allatok = new List<Allat>();
        }

        public Ketrec()
        {
            Allatok = new List<Allat>();
        }

        // -- METÓDUSOK --
        public void AddAllat(Allat allat)
        {
            // Állat hozzáadása a ketrechez
            if (Allatok.Count < Meret)
            {
                if (allat.Faj == Faj)
                {
                    Allatok.Add(allat);
                }
                
            }
        }

        public void RemoveAllat(Allat allat)
        {
            // Állat kivétele a ketrecből
            Allatok.Remove(allat);
        }

        public bool UresE()
        {
            // Igaz, ha ures a ketrec
            if (Allatok.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
