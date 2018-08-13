using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Authorization;
namespace RSMApp_Extended
{
    public class AuthOrgRoleMasterManagerExt:AuthOrgRoleMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBAuthOrgRole, string argClientCode)
        {
            RCBAuthOrgRole.Items.Clear();
            foreach (DataRow dr in GetAuthOrgRoleMaster(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AuthOrgcode"].ToString() + " " + dr["AuthOrgDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AuthOrgcode"].ToString()
                };

                itemCollection.Attributes.Add("AuthOrgcode", dr["AuthOrgcode"].ToString());
                itemCollection.Attributes.Add("AuthOrgDesc", dr["AuthOrgDesc"].ToString());

                RCBAuthOrgRole.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }

    }
}
