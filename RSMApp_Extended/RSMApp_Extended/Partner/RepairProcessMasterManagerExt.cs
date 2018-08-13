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
    public class RepairProcessMasterManagerExt : RepairProcessMasterManager
    {

        public void FillCombobox(RadComboBox RCBRepairProcess, string argClientCode, int iIsDeleted)
        {
            RCBRepairProcess.Items.Clear();
            foreach (DataRow dr in GetRepairProcessMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["RepairProcessCode"].ToString() + " " + dr["RepairProcessName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["RepairProcessCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("RepairProcessCode", dr["RepairProcessCode"].ToString().Trim());
                itemCollection.Attributes.Add("RepairProcessName", dr["RepairProcessName"].ToString().Trim());

                RCBRepairProcess.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
