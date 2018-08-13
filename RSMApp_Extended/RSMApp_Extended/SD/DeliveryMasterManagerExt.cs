using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;

namespace RSMApp_Extended
{
    public class DeliveryMasterManagerExt : DeliveryMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDelivery, string argClientCode, string argBillingDocTypeCode, string argToDocType)
        {
            RCBDelivery.Items.Clear();
            foreach (DataRow dr in GetDeliveryMaster4BD(argClientCode, argBillingDocTypeCode, argToDocType).Tables[0].Rows)
            {
                String itemText = dr["DeliveryDocCode"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DeliveryDocCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DeliveryDocCode", dr["DeliveryDocCode"].ToString().Trim());
                itemCollection.Attributes.Add("DeliveryDate", dr["DeliveryDate"].ToString());
                itemCollection.Attributes.Add("DeliveryDocTypeCode", dr["DeliveryDocTypeCode"].ToString());
                itemCollection.Attributes.Add("BillToParty", dr["BillToParty"].ToString());
                itemCollection.Attributes.Add("BillToPartyName", dr["BillToPartyName"].ToString());
                itemCollection.Attributes.Add("DocumentCode", dr["DocumentCode"].ToString().Trim());

                //itemCollection.Attributes.Add("SalesOrganizationCode", dr["SalesOrganizationCode"].ToString());
                //itemCollection.Attributes.Add("DistChannelCode", dr["DistChannelCode"].ToString());
                //itemCollection.Attributes.Add("DivisionCode", dr["DivisionCode"].ToString());
                //itemCollection.Attributes.Add("SOStatus", dr["SOStatus"].ToString());

                RCBDelivery.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
