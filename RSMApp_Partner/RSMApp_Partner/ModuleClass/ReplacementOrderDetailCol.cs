using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;


namespace RSMApp_Partner
{

    [DataContract]
    [KnownType(typeof(List<ReplacementOrderDetail>))]
    public class ReplacementOrderDetailCol
    {
        [DataMember]
        public List<ReplacementOrderDetail> colReplacementOrderDetail = new List<ReplacementOrderDetail>();
    }
}
