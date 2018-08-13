using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class WithHoldTaxCodeManagerExt : WithHoldTaxCodeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBWHTaxCode, string argClientCode, int iIsDeleted)
        {
            RCBWHTaxCode.Items.Clear();
            foreach (DataRow dr in GetWithHoldTaxCode(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["WHTaxCode"].ToString() + " " + dr["WHTaxCodeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["WHTaxCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("WHTaxCode", dr["WHTaxCode"].ToString());
                itemCollection.Attributes.Add("WHTaxCodeDesc", dr["WHTaxCodeDesc"].ToString());

                RCBWHTaxCode.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
