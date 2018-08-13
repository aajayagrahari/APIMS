using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class TaxCatCustomerManagerExt : TaxCatCustomerManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBTaxCatCustomer, string argClientCode, int iIsDeleted)
        {
            RCBTaxCatCustomer.Items.Clear();
            foreach (DataRow dr in GetTaxCatCustomer(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["TaxClassCode"].ToString() + " " + dr["TaxClassDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["TaxClassCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("TaxClassCode", dr["TaxClassCode"].ToString());
                itemCollection.Attributes.Add("TaxClassDesc", dr["TaxClassDesc"].ToString());

                RCBTaxCatCustomer.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
