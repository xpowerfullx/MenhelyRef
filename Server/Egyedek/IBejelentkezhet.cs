using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menhely   
{
    public interface IBejelentkezhet
    {
        string Nev { get; set; }
        string Jelszo { get; }
        bool Bejelentkezhet { get; set; }
        DateTime UtolsoCselekves { get; set; }
    }
}
