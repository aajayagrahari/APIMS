using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallEstimation>))]                                                                                                                                                                                                                                                                                                                                 
    public class CallEstimationCol
    {
        [DataMember]
        public List<CallEstimation> colCallEstimation = new List<CallEstimation>();
    }
}
