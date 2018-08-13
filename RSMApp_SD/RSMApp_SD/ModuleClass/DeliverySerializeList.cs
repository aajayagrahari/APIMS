using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_SD
{
    public class DeliverySerializeList : List<DeliverySerializeDetail>
    {
        public DeliverySerializeList(string argDeliveryDocCode, string argClientCode)
        {
            LoadAllDeliverySerialize(argDeliveryDocCode, argClientCode);
        }

        private void LoadAllDeliverySerialize(string argDeliveryDocCode, string argClientCode)
        {
            if (this.Count > 0)
                this.Clear();

            DeliverySerializeDetailManager objDeliverySerializeManager = new DeliverySerializeDetailManager();

            objDeliverySerializeManager.colGetDeliverySerializeDetail(argDeliveryDocCode, argClientCode, this);
            
        }

        public DeliverySerializeDetail GetDeliverySerializeByID(string argDeliveryDocCode, string argItemNo)
        {
            foreach (DeliverySerializeDetail argDeliverySerializeDetail in this)
            {
                if (argDeliverySerializeDetail.DeliveryDocCode.Trim() == argDeliveryDocCode.Trim() && argDeliverySerializeDetail.ItemNo.Trim() == argItemNo.Trim())
                {
                    return argDeliverySerializeDetail;
                }
            }
            return null;
        }

        public DeliverySerializeDetail GetDeliverySerializeByID(string argDeliveryDocCode, string argItemNo, string argSerialNo)
        {
            foreach (DeliverySerializeDetail argDeliverySerializeDetail in this)
            {
                if (argDeliverySerializeDetail.DeliveryDocCode.Trim() == argDeliveryDocCode.Trim() && argDeliverySerializeDetail.ItemNo.Trim() == argItemNo.Trim() && argDeliverySerializeDetail.SerialNo.Trim() == argSerialNo.Trim())
                {
                    return argDeliverySerializeDetail;
                }
            }
            return null;
        }
    }
}
