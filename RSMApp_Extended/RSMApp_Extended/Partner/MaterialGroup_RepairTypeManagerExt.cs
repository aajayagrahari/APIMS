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
    public class MatGroup_RepairTypeManagerExt : MatGroup_RepairTypeManager
    {
        public void FillCombobox(RadComboBox RCBMaterialGroup_RepairType, string argMatGroup1Code, string argClientCode)
        {
            RCBMaterialGroup_RepairType.Items.Clear();

            foreach (DataRow dr in GetMatGroup_RepairType(argMatGroup1Code, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["RepairTypeCode"].ToString().Trim() + " " + dr["RepairTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["RepairTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("RepairTypeCode", dr["RepairTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("RepairTypeDesc", dr["RepairTypeDesc"].ToString());

                RCBMaterialGroup_RepairType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}