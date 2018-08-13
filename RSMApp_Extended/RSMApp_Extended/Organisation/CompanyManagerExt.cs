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
    public class CompanyManagerExt : CompanyManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCompany, string argClientCode, int iIsDeleted)
        {
            RCBCompany.Items.Clear();
            foreach (DataRow dr in GetCompany(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CompanyCode"].ToString().Trim() + " " + dr["CompanyName"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CompanyCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CompanyCode", dr["CompanyCode"].ToString().Trim());
                itemCollection.Attributes.Add("CompanyName", dr["CompanyName"].ToString().Trim());

                RCBCompany.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
