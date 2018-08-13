using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<ErrorHandler>))]
    public class PartnerErrorResult
    {

        [DataMember]
        public Collection<ErrorHandler> colErrorHandler = new Collection<ErrorHandler>();
    }
}
