using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class OrderReasonManagerExt : OrderReasonManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBOrderReason, string argClientCode)
        {
            RCBOrderReason.Items.Clear();
            foreach (DataRow dr in GetOrderReason(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ReasonCode"].ToString() + " " + dr["ReasonDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ReasonCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ReasonCode", dr["ReasonCode"].ToString());
                itemCollection.Attributes.Add("ReasonDesc", dr["ReasonDesc"].ToString());

                RCBOrderReason.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
