using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<DCanabalizeDetails>))]
    public class DCanabalizeDetailsCol
    {
        [DataMember]
        public List<DCanabalizeDetails> colDCanabalizeDetails = new List<DCanabalizeDetails>();
    }
}
