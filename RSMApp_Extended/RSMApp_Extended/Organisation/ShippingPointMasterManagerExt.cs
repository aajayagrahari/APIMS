﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;
using RSMApp_Organization;

namespace RSMApp_Extended
{
    public class ShippingPointMasterManagerExt : ShippingPointMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBShippingPoint, string argClientCode, int iIsDeleted)
        {
            RCBShippingPoint.Items.Clear();
            foreach (DataRow dr in GetShippingPointMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ShippingPointCode"].ToString() + " " + dr["ShippingPointName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ShippingPointCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ShippingPointCode", dr["ShippingPointCode"].ToString().Trim());
                itemCollection.Attributes.Add("ShippingPointName", dr["ShippingPointName"].ToString());

                RCBShippingPoint.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
