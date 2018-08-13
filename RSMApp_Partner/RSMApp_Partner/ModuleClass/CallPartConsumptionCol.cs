using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallPartsConsumption>))]
    public class CallPartConsumptionCol
    {
        [DataMember]
        public List<CallPartsConsumption> colCallPartsConsumption = new List<CallPartsConsumption>();
    }
}
