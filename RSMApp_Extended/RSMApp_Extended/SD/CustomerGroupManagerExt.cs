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
    public class CustomerGroupManagerExt : CustomerGroupManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCustomerGrp, string argClientCode, int iIsDeleted)
        {
            RCBCustomerGrp.Items.Clear();
            foreach (DataRow dr in GetCustomerGroup(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CustGroupCode"].ToString().Trim() + " " + dr["CustGroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CustGroupCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CustGroupCode", dr["CustGroupCode"].ToString().Trim());
                itemCollection.Attributes.Add("CustGroupDesc", dr["CustGroupDesc"].ToString());

                RCBCustomerGrp.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
