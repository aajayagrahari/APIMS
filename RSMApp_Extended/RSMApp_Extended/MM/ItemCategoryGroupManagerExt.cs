using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ItemCategoryGroupManagerExt : ItemCategoryGroupManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBItemCategoryGrp, string argClientCode, int iIsDeleted)
        {
            RCBItemCategoryGrp.Items.Clear();
            foreach (DataRow dr in GetItemCategoryGroup(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ItemCatGroupCode"].ToString().Trim() + " " + dr["ItemCatGroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ItemCatGroupCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ItemCatGroupCode", dr["ItemCatGroupCode"].ToString().Trim());
                itemCollection.Attributes.Add("ItemCatGroupDesc", dr["ItemCatGroupDesc"].ToString());


                RCBItemCategoryGrp.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }
    }
}
