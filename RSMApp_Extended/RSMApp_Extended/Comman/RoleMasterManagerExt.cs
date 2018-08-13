using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class RoleMasterManagerExt : RoleMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBRole, int iIsDeleted)
        {
            RCBRole.Items.Clear();
            foreach (DataRow dr in GetRoleMaster(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["RoleCode"].ToString() + " " + dr["Role"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["RoleCode"].ToString()
                };

                itemCollection.Attributes.Add("RoleCode", dr["RoleCode"].ToString());
                itemCollection.Attributes.Add("Role", dr["Role"].ToString());

                RCBRole.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
