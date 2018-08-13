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
    public class PartnerFunctionManagerExt : PartnerFunctionManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPFunction, string argClientCode, int iIsDeleted)
        {
            RCBPFunction.Items.Clear();
            foreach (DataRow dr in GetPartnerFunction(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PartnerType"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PFunctionCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PFunctionCode", dr["PFunctionCode"].ToString());
                itemCollection.Attributes.Add("PartnerType", dr["PartnerType"].ToString());
                itemCollection.Attributes.Add("PFunctionDesc", dr["PFunctionDesc"].ToString());
                itemCollection.Attributes.Add("PartnerTable", dr["PartnerTable"].ToString());

                RCBPFunction.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
