using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using Telerik.Web.UI;
using System.Data;


namespace RSMApp_Extended
{
    public class AccountGroupManagerExt : AccountGroupManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBGLAccountGroup, string argClientCode, int iIsDeleted)
        {
            RCBGLAccountGroup.Items.Clear();
            foreach (DataRow dr in GetAccountGroup(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AccGroupCode"].ToString() + " " + dr["GroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AccGroupCode"].ToString()
                };

                itemCollection.Attributes.Add("AccGroupCode", dr["AccGroupCode"].ToString());
                itemCollection.Attributes.Add("GroupDesc", dr["GroupDesc"].ToString());

                RCBGLAccountGroup.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
