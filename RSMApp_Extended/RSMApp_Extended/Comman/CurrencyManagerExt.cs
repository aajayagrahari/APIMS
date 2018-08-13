using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class CurrencyManagerExt : CurrencyManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCurrency, int iIsDeleted)
        {
            RCBCurrency.Items.Clear();
            foreach (DataRow dr in GetCurrency(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["CurrencyCode"].ToString().Trim() + " " + dr["CurrencyName"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CurrencyCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CurrencyCode", dr["CurrencyCode"].ToString().Trim());
                itemCollection.Attributes.Add("CurrencyName", dr["CurrencyName"].ToString().Trim());

                RCBCurrency.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
        
    }
}
