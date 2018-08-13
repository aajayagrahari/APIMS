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
    public class PartnerStorageLocationManagerExt : PartnerStorageLocationManager
    {

        public void FillCombobox(RadComboBox RCBPartnerStore, string argClientCode, int iIsDeleted)
        {
            RCBPartnerStore.Items.Clear();
            foreach (DataRow dr in GetPartnerStorageLocation(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["StoreCode"].ToString() + " " + dr["StoreName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["StoreCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("StoreCode", dr["StoreCode"].ToString());
                itemCollection.Attributes.Add("StoreName", dr["StoreName"].ToString());

                RCBPartnerStore.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
