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
    public class PricingConditionTypeManagerExt : PricingConditionTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPricingCondition, string argClientCode, string argSOTypeCode)
        {
            RCBPricingCondition.Items.Clear();
            foreach (DataRow dr in GetPricingConditionType4SO(argClientCode, argSOTypeCode).Tables[0].Rows)
            {
                String itemText = dr["ConditionTypeCode"].ToString().Trim() + " " + dr["ConditionTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ConditionTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ConditionTypeCode", dr["ConditionTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("ConditionTypeDesc", dr["ConditionTypeDesc"].ToString());
                itemCollection.Attributes.Add("ConditionClass", dr["ConditionClass"].ToString().Trim());
                itemCollection.Attributes.Add("ManualEntryAllowed", dr["ManualEntryAllowed"].ToString());
                itemCollection.Attributes.Add("CalculationTypeCode", dr["CalculationTypeCode"].ToString());

                RCBPricingCondition.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPricingCondition, string argClientCode, int iIsDeleted)
        {
            RCBPricingCondition.Items.Clear();
            foreach (DataRow dr in GetPricingConditionType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ConditionTypeCode"].ToString().Trim() + " " + dr["ConditionTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ConditionTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ConditionTypeCode", dr["ConditionTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("ConditionTypeDesc", dr["ConditionTypeDesc"].ToString());

                RCBPricingCondition.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}

