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
    public class RepairTypeMasterManagerExt : RepairTypeMasterManager
    {

        public void FillCombobox(RadComboBox RCBRepairType, string argClientCode, int iIsDeleted)
        {
            RCBRepairType.Items.Clear();
            foreach (DataRow dr in GetRepairTypeMaster(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["RepairTypeCode"].ToString() + " " + dr["RepairTypeCode"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["RepairTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("RepairTypeCode", dr["RepairTypeCode"].ToString());
                itemCollection.Attributes.Add("RepairTypeDesc", dr["RepairTypeDesc"].ToString());

                RCBRepairType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
