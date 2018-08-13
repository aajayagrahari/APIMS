using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace RSMApp_MM
{
    [DataContract]
    [KnownType(typeof(List<BillsofMaterial>))]
    public class BillsofMaterialCol
    {
        [DataMember]
        public List<BillsofMaterial> colBillsofMaterial = new List<BillsofMaterial>();
    }
}