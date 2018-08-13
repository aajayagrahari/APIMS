using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallAdvReplacementIssue>))]
   public class CallAdvRepIssueCol
    {
        [DataMember]
        public List<CallAdvReplacementIssue> colCallAdvReplacementIssue = new List<CallAdvReplacementIssue>();
    }
}
