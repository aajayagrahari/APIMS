using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class BillingRelevantManagerExt : BillingRelevantManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBBillingRelevant, int iIsDeleted)
        {
            RCBBillingRelevant.Items.Clear();
            foreach (DataRow dr in GetBillingRelevant().Tables[0].Rows)
            {
                String itemText = dr["BillingRelevantCode"].ToString().Trim() + " " + dr["BRelevantDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["BillingRelevantCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("BillingRelevantCode", dr["BillingRelevantCode"].ToString().Trim());
                itemCollection.Attributes.Add("BRelevantDesc", dr["BRelevantDesc"].ToString().Trim());

                RCBBillingRelevant.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
