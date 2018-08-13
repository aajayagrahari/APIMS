using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
    public class InBDeliverySerializeList : List<InBDeliverySerializeDetail>
    {
        public InBDeliverySerializeList(string argInBDeliveryDocCode, string argClientCode)
        {
            LoadAllInBDeliverySerialize(argInBDeliveryDocCode, argClientCode);
        }

        private void LoadAllInBDeliverySerialize(string argInBDeliveryDocCode, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            InBDeliverySerializeDetailManager objInBDeliverySerializeManager = new InBDeliverySerializeDetailManager();

            objInBDeliverySerializeManager.colGetInBDeliverySerializeDetail(argInBDeliveryDocCode, argClientCode, this);
            
        }

        public InBDeliverySerializeDetail GetInBDeliverySerializeByID(string argInBDeliveryDocCode, string argItemNo)
        {
            foreach (InBDeliverySerializeDetail argInBDeliverySerializeDetail in this)
            {
                if (argInBDeliverySerializeDetail.InBDeliveryDocCode.Trim() == argInBDeliveryDocCode.Trim() && argInBDeliverySerializeDetail.ItemNo.Trim() == argItemNo.Trim())
                {
                    return argInBDeliverySerializeDetail;
                }
            }
            return null;
        }

        public InBDeliverySerializeDetail GetInBDeliverySerializeByID(string argInBDeliveryDocCode, string argItemNo, string argSerialNo)
        {
            foreach (InBDeliverySerializeDetail argInBDeliverySerializeDetail in this)
            {
                if (argInBDeliverySerializeDetail.InBDeliveryDocCode.Trim() == argInBDeliveryDocCode.Trim() && argInBDeliverySerializeDetail.ItemNo.Trim() == argItemNo.Trim() && argInBDeliverySerializeDetail.SerialNo.Trim() == argSerialNo.Trim())
                {
                    return argInBDeliverySerializeDetail;
                }
            }
            return null;
        }
    }
}
