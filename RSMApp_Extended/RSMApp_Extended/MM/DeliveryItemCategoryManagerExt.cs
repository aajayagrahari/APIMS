using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class DeliveryItemCategoryManagerExt : DeliveryItemCategoryManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDItemCategory, string argClientCode, int iIsDeleted)
        {
            RCBDItemCategory.Items.Clear();
            foreach (DataRow dr in GetDeliveryItemCategory(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DItemCategoryCode"].ToString() + " " + dr["DItemCategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DItemCategoryCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DItemCategoryCode", dr["DItemCategoryCode"].ToString().Trim());
                itemCollection.Attributes.Add("DItemCategoryDesc", dr["DItemCategoryDesc"].ToString());

                RCBDItemCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
