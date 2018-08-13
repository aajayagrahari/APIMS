using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;


namespace RSMApp_Extended
{
    public class AccAssignGrpCustomerManagerExt : AccAssignGrpCustomerManager
    {

        public void FillCombobox(RadComboBox RCBAccAssGrpCustomer, string argClientCode, int iIsDeleted)
        {
            RCBAccAssGrpCustomer.Items.Clear();
            foreach (DataRow dr in GetAccAssignGrpCustomer(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AccAssignGroupCode"].ToString() + " " + dr["AccAssignGroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AccAssignGroupCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("AccAssignGroupCode", dr["AccAssignGroupCode"].ToString());
                itemCollection.Attributes.Add("AccAssignGroupDesc", dr["AccAssignGroupDesc"].ToString());

                RCBAccAssGrpCustomer.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

    }
}
