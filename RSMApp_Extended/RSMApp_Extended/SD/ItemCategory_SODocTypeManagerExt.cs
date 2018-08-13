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
    public class ItemCategory_SODocTypeManagerExt : ItemCategory_SODocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBItemCategory, string argSODocType, string argItemCatGroupCode, string argHLItemCategoryCode, string argClientCode)
        {
            RCBItemCategory.Items.Clear();
            foreach (DataRow dr in GetItemCategory_SODocType4Combo(argSODocType, argItemCatGroupCode, argHLItemCategoryCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ItemCategoryCode"].ToString() + " " + dr["ItemCategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ItemCategoryCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ItemCategoryCode", dr["ItemCategoryCode"].ToString());
                itemCollection.Attributes.Add("ItemCategoryDesc", dr["ItemCategoryDesc"].ToString());
                itemCollection.Attributes.Add("ScheduleLineAllowed", dr["ScheduleLineAllowed"].ToString());
                itemCollection.Attributes.Add("IsBusinessItem", dr["IsBusinessItem"].ToString());
                itemCollection.Attributes.Add("ItemTypeCode", dr["ItemTypeCode"].ToString());
                itemCollection.Attributes.Add("HLItemCategoryCode", dr["HLItemCategoryCode"].ToString());
                itemCollection.Attributes.Add("ItemCatGroupCode", dr["ItemCatGroupCode"].ToString());

                RCBItemCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
