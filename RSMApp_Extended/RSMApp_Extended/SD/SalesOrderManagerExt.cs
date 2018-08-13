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
    public class SalesOrderManagerExt : SalesOrderManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesOrder, string argDeliveryDocTypeCode, string argToDocType, string argClientCode, string argShipToParty)
        {
            RCBSalesOrder.Items.Clear();
            foreach (DataRow dr in GetSalesOrder4Combo(argShipToParty, argDeliveryDocTypeCode, argToDocType, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SODocCode"].ToString().Trim() + "  " + dr["ShipToParty"].ToString() + "  " + dr["ShipToPartyName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SODocCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SODocCode", dr["SODocCode"].ToString().Trim());
                itemCollection.Attributes.Add("SODocDate", dr["SODocDate"].ToString());
                itemCollection.Attributes.Add("ShipToParty", dr["ShipToParty"].ToString());
                itemCollection.Attributes.Add("ShipToPartyName", dr["ShipToPartyName"].ToString());
                itemCollection.Attributes.Add("SalesOrganizationCode", dr["SalesOrganizationCode"].ToString());
                itemCollection.Attributes.Add("DistChannelCode", dr["DistChannelCode"].ToString());
                itemCollection.Attributes.Add("DivisionCode", dr["DivisionCode"].ToString());
                itemCollection.Attributes.Add("City", dr["City"].ToString());

                RCBSalesOrder.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesOrder, string argClientCode, string argDeliveryDocTypeCode, string argToDocType)
        {
            RCBSalesOrder.Items.Clear();
            foreach (DataRow dr in GetSalesOrder4DC(argClientCode, argDeliveryDocTypeCode, argToDocType).Tables[0].Rows)
            {
                String itemText = dr["SODocCode"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SODocCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SODocCode", dr["SODocCode"].ToString().Trim());
                itemCollection.Attributes.Add("SODocDate", dr["SODocDate"].ToString());
                itemCollection.Attributes.Add("SOTypeCode", dr["SOTypeCode"].ToString());
                itemCollection.Attributes.Add("ShipToParty", dr["ShipToParty"].ToString());
                itemCollection.Attributes.Add("ShipToPartyName", dr["ShipToPartyName"].ToString());

                itemCollection.Attributes.Add("SalesOrganizationCode", dr["SalesOrganizationCode"].ToString());
                itemCollection.Attributes.Add("DistChannelCode", dr["DistChannelCode"].ToString());
                itemCollection.Attributes.Add("DivisionCode", dr["DivisionCode"].ToString());
                itemCollection.Attributes.Add("SOStatus", dr["SOStatus"].ToString());
                itemCollection.Attributes.Add("City", dr["City"].ToString());

                RCBSalesOrder.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox4BD(Telerik.Web.UI.RadComboBox RCBSalesOrder, string argClientCode, string argBillingDocTypeCode, string argToDocType)
        {
            RCBSalesOrder.Items.Clear();
            foreach (DataRow dr in GetSalesOrder4BD(argClientCode, argBillingDocTypeCode, argToDocType).Tables[0].Rows)
            {
                String itemText = dr["SODocCode"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SODocCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SODocCode", dr["SODocCode"].ToString().Trim());
                itemCollection.Attributes.Add("SODocDate", dr["SODocDate"].ToString());
                itemCollection.Attributes.Add("SOTypeCode", dr["SOTypeCode"].ToString());
                itemCollection.Attributes.Add("BillToParty", dr["BillToParty"].ToString());
                itemCollection.Attributes.Add("BillToPartyName", dr["BillToPartyName"].ToString());
                itemCollection.Attributes.Add("DocumentCode", dr["DocumentCode"].ToString().Trim());

                itemCollection.Attributes.Add("SalesOrganizationCode", dr["SalesOrganizationCode"].ToString());
                itemCollection.Attributes.Add("DistChannelCode", dr["DistChannelCode"].ToString());
                itemCollection.Attributes.Add("DivisionCode", dr["DivisionCode"].ToString());
                itemCollection.Attributes.Add("SOStatus", dr["SOStatus"].ToString());

                RCBSalesOrder.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
