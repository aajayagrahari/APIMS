using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<AsgTechnicianCallDetail>))]
    public class AsgTechnicianCallDetailCol
    {
        [DataMember]
        public List<AsgTechnicianCallDetail> colAsgTechnicianCallDetail = new List<AsgTechnicianCallDetail>();
    }
}
