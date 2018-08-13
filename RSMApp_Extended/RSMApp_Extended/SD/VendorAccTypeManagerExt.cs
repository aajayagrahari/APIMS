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
    public class VendorAccTypeManagerExt : VendorAccTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBVendorAccType, string argClientCode, int iIsDeleted)
        {
            RCBVendorAccType.Items.Clear();
            foreach (DataRow dr in GetVendorAccType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["VendorAccTypeCode"].ToString() + " " + dr["VendorAccTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["VendorAccTypeCode"].ToString()
                };

                itemCollection.Attributes.Add("VendorAccTypeCode", dr["VendorAccTypeCode"].ToString());
                itemCollection.Attributes.Add("VendorAccTypeDesc", dr["VendorAccTypeDesc"].ToString());
                itemCollection.Attributes.Add("NumRange", dr["NumRange"].ToString());

                RCBVendorAccType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
