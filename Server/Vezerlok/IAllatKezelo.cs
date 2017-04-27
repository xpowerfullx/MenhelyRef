using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Menhely
{
    [ServiceContract]
    public interface IAllatKezelo
    {
        [OperationContract]
        void AllatMasikKetrecbe(Allat allat, Ketrec hova);

        [OperationContract]
        Allat[] AllatListazas();

        [OperationContract]
        Allat[] AllatListazasEgy(string nev);

        [OperationContract]
        void AllatModositas(Allat allat, Gondozo gondozo);

        [OperationContract]
        void AllatTorlese(Allat allat, Gondozo gondozo);

        [OperationContract]
        void AllatGondozas(Allat allat, Gondozo gondozo, string jegyzokonyv);

        [OperationContract]
        void AllatFelvetel(string nev, string leiras, int kor, AllatFaj faj, string alFaj, Ketrec ketrec, Gondozo gondozo);
   
    }
}
