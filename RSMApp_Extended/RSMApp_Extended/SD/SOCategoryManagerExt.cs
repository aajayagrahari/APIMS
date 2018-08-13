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
    public class SOCategoryManagerExt : SOCategoryManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSOCategory, string argClientCode, int iIsDeleted)
        {
            RCBSOCategory.Items.Clear();
            foreach (DataRow dr in GetSOCategory(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SOCategoryCode"].ToString() + " " + dr["SOCategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SOCategoryCode"].ToString()
                };

                itemCollection.Attributes.Add("SOCategoryCode", dr["SOCategoryCode"].ToString());
                itemCollection.Attributes.Add("SOCategoryDesc", dr["SOCategoryDesc"].ToString());

                RCBSOCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
