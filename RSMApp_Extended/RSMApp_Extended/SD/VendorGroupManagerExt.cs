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
    public class VendorGroupManagerExt : VendorGroupManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBVendorGroup, string argClientCode, int iIsDeleted)
        {
            RCBVendorGroup.Items.Clear();
            foreach (DataRow dr in GetVendorGroup(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["VendGroupCode"].ToString() + " " + dr["VendGroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["VendGroupCode"].ToString()
                };

                itemCollection.Attributes.Add("VendGroupCode", dr["VendGroupCode"].ToString());
                itemCollection.Attributes.Add("VendGroupDesc", dr["VendGroupDesc"].ToString());

                RCBVendorGroup.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }
    }
}
