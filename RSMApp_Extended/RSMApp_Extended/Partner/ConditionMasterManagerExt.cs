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
    public class ConditionMasterManagerExt : ConditionMasterManager
    {

        public void FillCombobox(RadComboBox RCBCondition, string argClientCode, int iIsDeleted)
        {
            RCBCondition.Items.Clear();
            foreach (DataRow dr in GetConditionMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ConditionCode"].ToString() + " " + dr["ConditionName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ConditionCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ConditionCode", dr["ConditionCode"].ToString());
                itemCollection.Attributes.Add("ConditionName", dr["ConditionName"].ToString());
     
                RCBCondition.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
