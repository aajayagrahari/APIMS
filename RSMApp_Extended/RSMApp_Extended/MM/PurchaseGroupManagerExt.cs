using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class PurchaseGroupManagerExt : PurchaseGroupManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPurchaseGrpCode, string argClientCode, int iIsDeleted)
        {
            RCBPurchaseGrpCode.Items.Clear();
            foreach (DataRow dr in GetPurchaseGroup(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PurchaseGroupCode"].ToString().Trim() + " " + dr["PurchaseGroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PurchaseGroupCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PurchaseGroupCode", dr["PurchaseGroupCode"].ToString().Trim());
                itemCollection.Attributes.Add("PurchaseGroupDesc", dr["PurchaseGroupDesc"].ToString());


                RCBPurchaseGrpCode.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }
    }
}
