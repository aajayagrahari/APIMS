using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class PaymentTermsManagerExt : PaymentTermsManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPaymentTerms, string argClientCode, int iIsDeleted)
        {
            RCBPaymentTerms.Items.Clear();
            foreach (DataRow dr in GetPaymentTerms(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PaymentTermsCode"].ToString() + " " + dr["PaymentTermsDesc1"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PaymentTermsCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PaymentTermsCode", dr["PaymentTermsCode"].ToString());
                itemCollection.Attributes.Add("PaymentTermsDesc1", dr["PaymentTermsDesc1"].ToString());

                RCBPaymentTerms.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
