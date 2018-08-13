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
    public class SOTypeManagerExt : SOTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSOType, string argClientCode, int iIsDeleted)
        {
            RCBSOType.Items.Clear();
            foreach (DataRow dr in GetSOType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SOTypeCode"].ToString().Trim() + " " + dr["SOTypeDesc"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SOTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SOTypeCode", dr["SOTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("SOCategoryCode", dr["SOCategoryCode"].ToString());
                itemCollection.Attributes.Add("ItemCategoryCode", dr["ItemCategoryCode"].ToString().Trim());
                itemCollection.Attributes.Add("SOTypeDesc", dr["SOTypeDesc"].ToString().Trim());
                itemCollection.Attributes.Add("SaveMode", dr["SaveMode"].ToString().Trim());
                itemCollection.Attributes.Add("itemNoIncr", dr["itemNoIncr"].ToString().Trim());

                RCBSOType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
