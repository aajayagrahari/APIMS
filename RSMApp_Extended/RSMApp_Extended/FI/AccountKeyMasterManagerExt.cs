using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class AccountKeyMasterManagerExt : AccountKeyMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBAccKeyMaster, string argClientCode, int iIsDeleted)
        {
            RCBAccKeyMaster.Items.Clear();
            foreach (DataRow dr in GetAccountKeyMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AccKeyCode"].ToString() + " " + dr["AccKeyDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AccKeyCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("AccKeyCode", dr["AccKeyCode"].ToString());
                itemCollection.Attributes.Add("AccKeyDesc", dr["AccKeyDesc"].ToString());

                RCBAccKeyMaster.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
