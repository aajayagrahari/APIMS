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
    public class BillingDocTypeManagerExt : BillingDocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBBillingDocType, string argClientCode, int iIsDeleted)
        {
            RCBBillingDocType.Items.Clear();
            foreach (DataRow dr in GetBillingDocType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["BillingDocTypeCode"].ToString().Trim() + " " + dr["BillingTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["BillingDocTypeCode"].ToString().Trim()
                };
                itemCollection.Attributes.Add("BillingDocTypeCode", dr["BillingDocTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("BillingTypeDesc", dr["BillingTypeDesc"].ToString());
                itemCollection.Attributes.Add("BasedOn", dr["BasedOn"].ToString());
                itemCollection.Attributes.Add("SaveMode", dr["SaveMode"].ToString());

                RCBBillingDocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
