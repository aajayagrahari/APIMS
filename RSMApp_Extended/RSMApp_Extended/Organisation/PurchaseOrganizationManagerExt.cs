using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;
using RSMApp_Organization;

namespace RSMApp_Extended
{
    public class PurchaseOrganizationManagerExt : PurchaseOrganizationManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPurchaseOrg, string argClientCode, int iIsDeleted)
        {
            RCBPurchaseOrg.Items.Clear();
            foreach (DataRow dr in GetPurchaseOrganization(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PurchaseOrgCode"].ToString() + " " + dr["PurchaseOrgName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PurchaseOrgCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PurchaseOrgCode", dr["PurchaseOrgCode"].ToString().Trim());
                itemCollection.Attributes.Add("PurchaseOrgName", dr["PurchaseOrgName"].ToString().Trim());


                RCBPurchaseOrg.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
