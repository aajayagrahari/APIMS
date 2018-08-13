using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class TaxCategoryManagerExt : TaxCategoryManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBTaxCategory, string argClientCode, int iIsDeleted)
        {
            RCBTaxCategory.Items.Clear();
            foreach (DataRow dr in GetTaxCategory(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["TaxCategoryCode"].ToString() + " " + dr["TaxCategoryDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["TaxCategoryCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("TaxCategoryCode", dr["TaxCategoryCode"].ToString());
                itemCollection.Attributes.Add("TaxCategoryDesc", dr["TaxCategoryDesc"].ToString());

                RCBTaxCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
