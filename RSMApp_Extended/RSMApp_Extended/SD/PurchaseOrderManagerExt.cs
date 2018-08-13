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
    public class PurchaseOrderManagerExt : PurchaseOrderManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPurchaseOrder, string argClientCode, string argInBDeliveryDocTypeCode, string argToDocType)
        {
            RCBPurchaseOrder.Items.Clear();
            foreach (DataRow dr in GetPurchaseOrder4InBDC(argClientCode, argInBDeliveryDocTypeCode, argToDocType).Tables[0].Rows)
            {
                String itemText = dr["PODocCode"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PODocCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PODocCode", dr["PODocCode"].ToString().Trim());
                itemCollection.Attributes.Add("PODocDate", dr["PODocumentDate"].ToString());
                itemCollection.Attributes.Add("POTypeCode", dr["POTypeCode"].ToString());
                itemCollection.Attributes.Add("VendorCode", dr["VendorCode"].ToString());
                itemCollection.Attributes.Add("VendorName", dr["Name1"].ToString());

                itemCollection.Attributes.Add("PurchaseOrgCode", dr["PurchaseOrgCode"].ToString());
                itemCollection.Attributes.Add("CompanyCode", dr["CompanyCode"].ToString());
                itemCollection.Attributes.Add("SourcePlantCode", dr["SourcePlantCode"].ToString());
                itemCollection.Attributes.Add("POStatus", dr["POStatus"].ToString());

                RCBPurchaseOrder.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }



        


    }
}
