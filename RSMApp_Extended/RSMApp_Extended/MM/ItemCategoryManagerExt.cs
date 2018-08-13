using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ItemCategoryManagerExt : ItemCategoryManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBItemCategory, string argClientCode, int iIsDeleted)
        {
            RCBItemCategory.Items.Clear();
            foreach (DataRow dr in GetItemCategory(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ItemCategoryCode"].ToString() + " " + dr["ItemCategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ItemCategoryCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ItemCategoryCode", dr["ItemCategoryCode"].ToString().Trim());
                itemCollection.Attributes.Add("ItemCategoryDesc", dr["ItemCategoryDesc"].ToString());

                RCBItemCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
