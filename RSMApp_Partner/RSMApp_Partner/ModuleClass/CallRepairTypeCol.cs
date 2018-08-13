using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallRepairTypeDetail>))]
    public class CallRepairTypeCol
    {
        [DataMember]
        public List<CallRepairTypeDetail> colCallRepairTypeDetail = new List<CallRepairTypeDetail>();
    }
}
