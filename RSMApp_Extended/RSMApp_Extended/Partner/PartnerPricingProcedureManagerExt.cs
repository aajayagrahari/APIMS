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
    public class PartnerPricingProcedureManagerExt : PartnerPricingProcedureManager
    {

        public void FillCombobox(RadComboBox RCBPartnerPProcedure, string argClientCode, int iIsDeleted)
        {
            RCBPartnerPProcedure.Items.Clear();
            foreach (DataRow dr in GetPartnerPricingProcedure(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ProcedureType"].ToString().Trim() + " " + dr["PricingDescription"].ToString().Trim() + " " + dr["CalculationType"].ToString() + " " + dr["CalculationValue"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ProcedureType"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ProcedureType", dr["ProcedureType"].ToString());
                itemCollection.Attributes.Add("PricingDescription", dr["PricingDescription"].ToString());
                itemCollection.Attributes.Add("CalculationType", dr["CalculationType"].ToString());
                itemCollection.Attributes.Add("CalculationValue", dr["CalculationValue"].ToString());

                RCBPartnerPProcedure.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
