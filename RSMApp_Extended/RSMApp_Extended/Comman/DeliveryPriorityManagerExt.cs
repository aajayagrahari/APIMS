using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class DeliveryPriorityManagerExt : DeliveryPriorityManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDlvPriority, string argClientCode, int iIsDeleted)
        {
            RCBDlvPriority.Items.Clear();
            foreach (DataRow dr in GetDeliveryPriority(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DlvPriorityCode"].ToString() + " " + dr["DlvPriorityDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DlvPriorityCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DlvPriorityCode", dr["DlvPriorityCode"].ToString());
                itemCollection.Attributes.Add("DlvPriorityDesc", dr["DlvPriorityDesc"].ToString());

                RCBDlvPriority.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
