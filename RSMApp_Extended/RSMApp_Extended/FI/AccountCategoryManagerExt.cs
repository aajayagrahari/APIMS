using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using Telerik.Web.UI;
using System.Data;

namespace RSMApp_Extended
{
    public class AccountCategoryManagerExt : AccountCategoryManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBGLAccountCategory, string argClientCode, int iIsDeleted)
        {
            RCBGLAccountCategory.Items.Clear();
            foreach (DataRow dr in GetAccountCategory(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AccCategoryCode"].ToString() + " " + dr["CategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AccCategoryCode"].ToString()
                };

                itemCollection.Attributes.Add("AccCategoryCode", dr["AccCategoryCode"].ToString());
                itemCollection.Attributes.Add("CategoryDesc", dr["CategoryDesc"].ToString());

                RCBGLAccountCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
