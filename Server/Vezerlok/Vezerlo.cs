using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;


namespace Menhely
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,IncludeExceptionDetailInFaults=true)]
    public partial class Vezerlo
    {
        public Vezerlo()
        {

        }
    }
}
