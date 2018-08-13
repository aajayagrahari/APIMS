using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<CanabalizeDetails>))]
    public class CanabalizeDetailsCol
    {
        [DataMember]
        public List<CanabalizeDetails> colCanabalizeDetails = new List<CanabalizeDetails>();
    }
}
