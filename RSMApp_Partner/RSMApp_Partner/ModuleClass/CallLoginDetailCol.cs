using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallLoginDetail>))]
    public class CallLoginDetailCol
    {
        [DataMember]
        public List<CallLoginDetail> colCallLoginDetail = new List<CallLoginDetail>();
    }
}
