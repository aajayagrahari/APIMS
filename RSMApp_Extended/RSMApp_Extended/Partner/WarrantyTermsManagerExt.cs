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
    public class WarrantyTermsManagerExt : WarrantyTermsManager
    {

        public void FillCombobox(RadComboBox RCBWarrantyTerms, string argClientCode, int iIsDeleted)
        {
            RCBWarrantyTerms.Items.Clear();
            foreach (DataRow dr in GetWarrantyTerms(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["WarrantyTermsCode"].ToString() + " " + dr["WarrantyTermsDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["WarrantyTermsCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("WarrantyTermsCode", dr["WarrantyTermsCode"].ToString());
                itemCollection.Attributes.Add("WarrantyTermsDesc", dr["WarrantyTermsDesc"].ToString());

                RCBWarrantyTerms.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
