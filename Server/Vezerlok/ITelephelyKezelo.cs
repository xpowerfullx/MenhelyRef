using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Menhely
{
    [ServiceContract]
    public interface ITelephelyKezelo
    {
        //void TelephelyModositas(Telephely telephely);

        [OperationContract]
        void TelephelyFelvetel(string cim);

        [OperationContract]
        void KetrecHozzaadas(Telephely telephely, int ketrecMeret, AllatFaj faj);

        [OperationContract]
        void KetrecTorles(Telephely telephely, Ketrec ketrec);

        [OperationContract]
        void KetrecModositas(Ketrec ketrec);

        [OperationContract]
        void TelephelyTorles(Telephely telephely);

        [OperationContract]
        void AllatMasikTelephelyre(Allat allat, Telephely hovaTelep, Ketrec hovaKetrec);

        [OperationContract]
        Telephely[] TelephelyListazas();

        [OperationContract]
        Telephely[] TelephelyListazasEgy(string cim);

        [OperationContract]
        Ketrec[] KetrecListazas();

        [OperationContract]
        Ketrec[] KetrecListazasEgy(int id);
    }
}
