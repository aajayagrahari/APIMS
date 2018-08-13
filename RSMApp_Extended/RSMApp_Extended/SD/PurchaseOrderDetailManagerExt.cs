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
    public class PurchaseOrderDetailManagerExt : PurchaseOrderDetailManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPurchaseOrderDetailItemNo, string argClientCode, string argPODocCode)
        {
            RCBPurchaseOrderDetailItemNo.Items.Clear();
            foreach (DataRow dr in GetPurchaseOrderDetailsItemNo(argPODocCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["POItemNo"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["POItemNo"].ToString().Trim()
                };

                itemCollection.Attributes.Add("POItemNo", dr["POItemNo"].ToString().Trim());
                RCBPurchaseOrderDetailItemNo.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
