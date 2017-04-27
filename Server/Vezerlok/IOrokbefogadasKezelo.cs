using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Menhely
{
    [ServiceContract]
    public interface IOrokbefogadasKezelo
    {
        [OperationContract]
        void OrokbefogadasElfogadasa(Allat allat);

        [OperationContract]
        void OrokbefogadasVisszautasitas(Allat allat);

        [OperationContract]
        void KerelemLeadas(Orokbefogado orokbefogado, Allat allat);
    }
}
