using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;

namespace RSMApp_Extended
{
    public class InBDeliveyDocTypeManagerExt : InBDeliveyDocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBInbDeliveryDocType, string argClientCode, int iIsDeleted)
        {
            RCBInbDeliveryDocType.Items.Clear();
            foreach (DataRow dr in GetInBDeliveyDocType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["InBDeliveryDocTypeCode"].ToString() + " " + dr["InBDeliveryTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["InBDeliveryDocTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("InBDeliveryDocTypeCode", dr["InBDeliveryDocTypeCode"].ToString());
                itemCollection.Attributes.Add("InBDeliveryTypeDesc", dr["InBDeliveryTypeDesc"].ToString());
                itemCollection.Attributes.Add("NumRange", dr["NumRange"].ToString());


                RCBInbDeliveryDocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
