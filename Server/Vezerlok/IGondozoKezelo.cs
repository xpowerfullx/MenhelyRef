using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Menhely
{
    [ServiceContract]
    public interface IGondozoKezelo
    {
        [OperationContract]
        Gondozo Bejelenkezes(string nev, string jelszo);

        [OperationContract]
        Gondozo[] GondozoListazas();

        [OperationContract]
        Gondozo[] GondozoListazasEgy(string nev);

        [OperationContract]
        void GondozoModositas(Gondozo gondozo);

        [OperationContract]
        void GondozoLetrehozas(string nev, GondozoBeosztas beosztas, string jelszo, Telephely munkahely);

        [OperationContract]
        void GondozoTorles(Gondozo gondozo);

        [OperationContract]
        bool BejelentkezesEllenorzesGondozo(Gondozo gondozo);

        [OperationContract]
        void GondozottAllatHozzaadas(Gondozo gondozo, Allat allat);

        [OperationContract]
        void TelephelyGondozohozAdas(Gondozo gondozo, Telephely telephely);

        [OperationContract]
        void GondozottAllatEltavolitas(Gondozo gondozo, Allat allat);

        [OperationContract]
        void TelephelyGondozotolLevetel(Gondozo gondozo, Telephely telephely);


    }
}
