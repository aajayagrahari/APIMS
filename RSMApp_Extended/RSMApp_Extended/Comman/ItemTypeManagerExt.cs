using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;


namespace RSMApp_Extended
{
    public class ItemTypeManagerExt : ItemTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBItemType, int iIsDeleted)
        {
            RCBItemType.Items.Clear();
            foreach (DataRow dr in GetItemType().Tables[0].Rows)
            {
                String itemText = dr["ItemTypeCode"].ToString().Trim() + " " + dr["ItemTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ItemTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ItemTypeCode", dr["ItemTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("ItemTypeDesc", dr["ItemTypeDesc"].ToString().Trim());

                RCBItemType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
