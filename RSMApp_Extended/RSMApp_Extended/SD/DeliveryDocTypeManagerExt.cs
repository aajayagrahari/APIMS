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
    public class DeliveryDocTypeManagerExt : DeliveryDocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDeliveryDocType, string argClientCode, int iIsDeleted)
        {
            RCBDeliveryDocType.Items.Clear();
            foreach (DataRow dr in GetDeliveryDocType(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DeliveryDocTypeCode"].ToString().Trim() + " " + dr["DeliveryTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DeliveryDocTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DeliveryDocTypeCode", dr["DeliveryDocTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("ItemCategoryCode", dr["ItemCategoryCode"].ToString().Trim());
                itemCollection.Attributes.Add("DeliveryTypeDesc", dr["DeliveryTypeDesc"].ToString());
                itemCollection.Attributes.Add("ItemNoIncr", dr["ItemNoIncr"].ToString());
                itemCollection.Attributes.Add("SaveMode", dr["SaveMode"].ToString());

                RCBDeliveryDocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
