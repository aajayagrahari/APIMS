using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class StateManagerExt : StateManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBState, string argCountryCode, int iIsDeleted)
        {
            RCBState.Items.Clear();
            foreach (DataRow dr in GetState(argCountryCode, iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["StateCode"].ToString() + " " + dr["StateName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["StateCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("StateCode", dr["StateCode"].ToString());
                itemCollection.Attributes.Add("StateName", dr["StateName"].ToString());

                RCBState.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBState, int iIsDeleted)
        {
            RCBState.Items.Clear();
            foreach (DataRow dr in GetState(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["StateCode"].ToString() + " " + dr["StateName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["StateCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("StateCode", dr["StateCode"].ToString());
                itemCollection.Attributes.Add("StateName", dr["StateName"].ToString());

                RCBState.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
