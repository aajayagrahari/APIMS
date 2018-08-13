using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_Partner;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace RSMApp_Extended
{
    public class RepairStatusMasterManagerExt : RepairStatusMasterManager
    {

        public void FillCombobox(RadComboBox RCBRepairStatus, string argClientCode, int iIsDeleted)
        {
            RCBRepairStatus.Items.Clear();
            foreach (DataRow dr in GetRepairStatusMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["RepairStatusCode"].ToString() + " " + dr["RepairStatusDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["RepairStatusCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("RepairStatusCode", dr["RepairStatusCode"].ToString().Trim());
                itemCollection.Attributes.Add("RepairStatusDesc", dr["RepairStatusDesc"].ToString().Trim());

                RCBRepairStatus.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
