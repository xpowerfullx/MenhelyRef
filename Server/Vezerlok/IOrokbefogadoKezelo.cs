using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Menhely
{
    [ServiceContract]
    public interface IOrokbefogadoKezelo
    {
        [OperationContract]
        void Adomanyozas(Orokbefogado orokbefogado,int adomanyOsszeg);

        [OperationContract]
        Orokbefogado Bejelentkezes(string nev, string jelszo);

        [OperationContract]
        bool BejelentkezesEllenorzesOrokbefogado(Orokbefogado orokbefogado);

        [OperationContract]
        bool Regisztracio(string nev, string jelszo);

        [OperationContract]
        Orokbefogado[] OrokbefogadoListazas();

        [OperationContract]
        Orokbefogado[] OrokbefogadoListazasEgy(string nev);
    }
}
