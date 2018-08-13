using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallRepairProcess>))]
    public class CallRepairProcessCol
    {
        [DataMember]
        public List<CallRepairProcess> colCallRepairProcess = new List<CallRepairProcess>();
    }
}
