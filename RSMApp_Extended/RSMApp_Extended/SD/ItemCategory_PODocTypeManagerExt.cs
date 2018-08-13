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
    public class ItemCategory_PODocTypeManagerExt : ItemCategory_PODocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBItemCategory, string argPODocType, string argItemCatGroupCode, string argHLPOItemCategoryCode, string argClientCode)
        {
            RCBItemCategory.Items.Clear();
            foreach (DataRow dr in GetItemCategory_PODocType4Combo(argPODocType, argItemCatGroupCode, argHLPOItemCategoryCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ItemCategoryCode"].ToString() + " " + dr["ItemCategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ItemCategoryCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ItemCategoryCode", dr["ItemCategoryCode"].ToString());
                itemCollection.Attributes.Add("ItemCategoryDesc", dr["ItemCategoryDesc"].ToString());
                itemCollection.Attributes.Add("HLPOItemCategoryCode", dr["HLPOItemCategoryCode"].ToString());
                itemCollection.Attributes.Add("ItemCatGroupCode", dr["ItemCatGroupCode"].ToString());

                RCBItemCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
