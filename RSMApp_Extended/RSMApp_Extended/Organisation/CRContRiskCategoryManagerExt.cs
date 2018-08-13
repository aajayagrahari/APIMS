using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Organization;

namespace RSMApp_Extended
{
    public class CRContRiskCategoryManagerExt : CRContRiskCategoryManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBRiskCategory, string argClientCode, int iIsDeleted)
        {
            RCBRiskCategory.Items.Clear();
            foreach (DataRow dr in GetCRContRiskCategory(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["RiskCategoryCode"].ToString() + " " + dr["RiskCategory"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["RiskCategoryCode"].ToString()
                };

                itemCollection.Attributes.Add("RiskCategoryCode", dr["RiskCategoryCode"].ToString());
                itemCollection.Attributes.Add("RiskCategory", dr["RiskCategory"].ToString());

                RCBRiskCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
