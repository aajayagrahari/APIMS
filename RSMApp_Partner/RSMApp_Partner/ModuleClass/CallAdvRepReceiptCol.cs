using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallAdvReplacementReceipt>))]
   public class CallAdvRepReceiptCol
    {
        [DataMember]
        public List<CallAdvReplacementReceipt> colCallAdvReplacementReceipt = new List<CallAdvReplacementReceipt>();
    }
}
