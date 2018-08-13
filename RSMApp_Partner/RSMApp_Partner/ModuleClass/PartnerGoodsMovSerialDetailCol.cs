using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<PartnerGoodsMovSerialDetail>))]
    public class PartnerGoodsMovSerialDetailCol
    {
        [DataMember]
        public List<PartnerGoodsMovSerialDetail> colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();
    }
}
