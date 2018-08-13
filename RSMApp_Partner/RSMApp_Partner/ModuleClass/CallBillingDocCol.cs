using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallBillingDoc>))]
    public class CallBillingDocCol
    {
        [DataMember]
        public List<CallBillingDoc> colCallBillingDoc = new List<CallBillingDoc>();
    }
}
