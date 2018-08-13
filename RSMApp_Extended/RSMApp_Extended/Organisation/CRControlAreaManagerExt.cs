using System;
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
    public class CRControlAreaManagerExt : CRControlAreaManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCRContArea, string argClientCode, int iIsDeleted)
        {
            RCBCRContArea.Items.Clear();
            foreach (DataRow dr in GetCRControlArea(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CRContAreaCode"].ToString().Trim() + " " + dr["CRContArea"].ToString().Trim() + " " + dr["CurrencyCode"].ToString().Trim() + " " + dr["CRUpdateCode"].ToString().Trim() + " " + dr["FiscalYearType"].ToString().Trim() + " " + dr["RiskCategoryCode"].ToString() + " " + dr["CreditLimit"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CRContAreaCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CRContAreaCode", dr["CRContAreaCode"].ToString().Trim());
                itemCollection.Attributes.Add("CRContArea", dr["CRContArea"].ToString().Trim());
                itemCollection.Attributes.Add("CurrencyCode", dr["CurrencyCode"].ToString().Trim());
                itemCollection.Attributes.Add("CRUpdateCode", dr["CRUpdateCode"].ToString().Trim());
                itemCollection.Attributes.Add("FiscalYearType", dr["FiscalYearType"].ToString().Trim());
                itemCollection.Attributes.Add("RiskCategoryCode", dr["RiskCategoryCode"].ToString().Trim());
                itemCollection.Attributes.Add("CreditLimit", dr["CreditLimit"].ToString().Trim());

                RCBCRContArea.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
