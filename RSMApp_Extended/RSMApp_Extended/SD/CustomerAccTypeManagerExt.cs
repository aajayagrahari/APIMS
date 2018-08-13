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
    public class CustomerAccTypeManagerExt : CustomerAccTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCustomerAccType, string argClientCode, int iIsDeleted)
        {
            RCBCustomerAccType.Items.Clear();
            foreach (DataRow dr in GetCustomerAccType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CustomerAccTypeCode"].ToString().Trim() + " " + dr["CustomerAccTypeDesc"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CustomerAccTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CustomerAccTypeCode", dr["CustomerAccTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("CustomerAccTypeDesc", dr["CustomerAccTypeDesc"].ToString().Trim());
                itemCollection.Attributes.Add("NumRange", dr["NumRange"].ToString());

                RCBCustomerAccType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
