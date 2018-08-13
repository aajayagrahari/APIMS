using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class TransportZoneManagerExt : TransportZoneManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBTransportZone, string argClientCode, int iIsDeleted)
        {
            RCBTransportZone.Items.Clear();
            foreach (DataRow dr in GetTransportZone(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["TransportZoneCode"].ToString() + " " + dr["TransportZoneDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["TransportZoneCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("TransportZoneCode", dr["TransportZoneCode"].ToString());
                itemCollection.Attributes.Add("TransportZoneDesc", dr["TransportZoneDesc"].ToString());

                RCBTransportZone.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
