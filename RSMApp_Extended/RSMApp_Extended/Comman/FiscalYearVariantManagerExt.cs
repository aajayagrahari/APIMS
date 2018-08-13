using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class FiscalYearVariantManagerExt : FiscalYearVariantManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBFiscalYr, int iIsDeleted)
        {
            RCBFiscalYr.Items.Clear();
            foreach (DataRow dr in GetFiscalYearVariant(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["FiscalYearType"].ToString().Trim() + " " + dr["FYDesc"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["FiscalYearType"].ToString().Trim()
                };

                itemCollection.Attributes.Add("FiscalYearType", dr["FiscalYearType"].ToString().Trim());
                itemCollection.Attributes.Add("FYDesc", dr["FYDesc"].ToString().Trim());

                RCBFiscalYr.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
