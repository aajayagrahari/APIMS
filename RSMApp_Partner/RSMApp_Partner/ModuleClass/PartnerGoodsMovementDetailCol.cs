using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<PartnerGoodsMovementDetail>))]
    public class PartnerGoodsMovementDetailCol
    {
        [DataMember]
        public List<PartnerGoodsMovementDetail> colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
    }
}
