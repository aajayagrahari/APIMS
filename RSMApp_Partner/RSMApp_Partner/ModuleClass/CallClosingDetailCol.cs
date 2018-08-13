﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CallClosingDetails>))]
    public class CallClosingDetailCol
    {
        [DataMember]
        public List<CallClosingDetails> colCallClosingDetail = new List<CallClosingDetails>();
    }
}
