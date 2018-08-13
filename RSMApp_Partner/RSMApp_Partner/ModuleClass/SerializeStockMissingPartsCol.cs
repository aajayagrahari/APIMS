using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_Partner
{
    [DataContract]
    [KnownType(typeof(List<SerializeStockMissingParts>))]
    public class SerializeStockMissingPartsCol
    {
        [DataMember]
        public List<SerializeStockMissingParts> colSerializeStockMissingParts = new List<SerializeStockMissingParts>();
    }
}
