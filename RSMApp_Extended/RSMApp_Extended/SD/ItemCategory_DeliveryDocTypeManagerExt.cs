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
    public class ItemCategory_DeliveryDocTypeManagerExt : ItemCategory_DeliveryDocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBItemCategory, string argDeliveryDocTypeCode, string argItemCatGroupCode, string argHLItemCategoryCode, string argClientCode)
        {
            RCBItemCategory.Items.Clear();
            foreach (DataRow dr in GetItemCategory_DeliveryDocType4Combo(argDeliveryDocTypeCode, argItemCatGroupCode, argHLItemCategoryCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DItemCategoryCode"].ToString() + " " + dr["DItemCategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DItemCategoryCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DItemCategoryCode", dr["DItemCategoryCode"].ToString());
                itemCollection.Attributes.Add("DItemCategoryDesc", dr["DItemCategoryDesc"].ToString());
                itemCollection.Attributes.Add("HLItemCategoryCode", dr["HLItemCategoryCode"].ToString());
                itemCollection.Attributes.Add("BillingRelevant", dr["BillingRelevant"].ToString());
                itemCollection.Attributes.Add("IsReturn", dr["IsReturn"].ToString());
                itemCollection.Attributes.Add("ItemCatGroupCode", dr["ItemCatGroupCode"].ToString());

                RCBItemCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
