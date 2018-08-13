using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallDefectTypeDetail>))]
    public class CallDefectTypeCol
    {
        [DataMember]
        public List<CallDefectTypeDetail> colCallDefectTypeDetail = new List<CallDefectTypeDetail>();
    }
}
