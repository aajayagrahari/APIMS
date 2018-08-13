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
    public class RepairDocTypeManagerExt : RepairDocTypeManager
    {

        public void FillCombobox(RadComboBox RCBRepairDocType, string argClientCode, int iIsDeleted)
        {
            RCBRepairDocType.Items.Clear();
            foreach (DataRow dr in GetRepairDocType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["RepairDocTypeCode"].ToString() + " " + dr["RepairTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["RepairDocTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("RepairDocTypeCode", dr["RepairDocTypeCode"].ToString());
                itemCollection.Attributes.Add("RepairTypeDesc", dr["RepairTypeDesc"].ToString());

                RCBRepairDocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
