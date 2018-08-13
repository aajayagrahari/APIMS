using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<ReplacementOrderSerialDetail>))]
   public class ReplacementOrderSerialDetailCol
    {
        [DataMember]
        public List<ReplacementOrderSerialDetail> colReplacementOrderSerialDetail = new List<ReplacementOrderSerialDetail>();

    }
}
