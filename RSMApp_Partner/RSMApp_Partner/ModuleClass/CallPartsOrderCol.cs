using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallPartsOrder>))]
    public class CallPartsOrderCol
    {
        [DataMember]
        public List<CallPartsOrder> colCallRepairProcess = new List<CallPartsOrder>();
    }
}
