using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Authorization;

namespace RSMApp_Extended
{
    public class ProfileManagerExt : ProfileManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBProfile, int iIsDeleted)
        {
            RCBProfile.Items.Clear();
            foreach (DataRow dr in GetProfile(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["ProfileCode"].ToString() + " " + dr["ProfileDesc"].ToString() + " " + dr["TranType"].ToString() + " " + dr["Modules"].ToString() + " " + dr["Activity"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ProfileCode"].ToString()
                };

                itemCollection.Attributes.Add("ProfileCode", dr["ProfileCode"].ToString());
                itemCollection.Attributes.Add("ProfileDesc", dr["ProfileDesc"].ToString());
                itemCollection.Attributes.Add("TranType", dr["TranType"].ToString());
                itemCollection.Attributes.Add("Modules", dr["Modules"].ToString());
                itemCollection.Attributes.Add("Activity", dr["Activity"].ToString());
                RCBProfile.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }

    }
}
