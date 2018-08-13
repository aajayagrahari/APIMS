using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class WithHoldTaxTypeManagerExt : WithHoldTaxTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBWHTaxType, string argClientCode, int iIsDeleted)
        {
            RCBWHTaxType.Items.Clear();
            foreach (DataRow dr in GetWithHoldTaxType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["WHTaxTypeCode"].ToString() + " " + dr["WHTaxTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["WHTaxTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("WHTaxTypeCode", dr["WHTaxTypeCode"].ToString());
                itemCollection.Attributes.Add("WHTaxTypeDesc", dr["WHTaxTypeDesc"].ToString());

                RCBWHTaxType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
