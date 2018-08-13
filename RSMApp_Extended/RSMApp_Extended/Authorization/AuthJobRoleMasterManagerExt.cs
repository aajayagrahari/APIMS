using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Authorization;


namespace RSMApp_Extended
{
    public class AuthJobRoleMasterManagerExt : AuthJobRoleMasterManager
    {

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBAuthJobRole, string argClientCode)
        {
            RCBAuthJobRole.Items.Clear();
            foreach (DataRow dr in GetAuthJobRoleMaster(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AuthJobRoleCode"].ToString() + " " + dr["AuthJobRoleDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AuthJobRoleCode"].ToString()
                };

                itemCollection.Attributes.Add("AuthJobRoleCode", dr["AuthJobRoleCode"].ToString());
                itemCollection.Attributes.Add("AuthJobRoleDesc", dr["AuthJobRoleDesc"].ToString());

                RCBAuthJobRole.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
