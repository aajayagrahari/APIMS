using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class ProfitCenterManagerExt : ProfitCenterManager 
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBProfitCenter, string argClientCode, int iIsDeleted)
        {
            RCBProfitCenter.Items.Clear();
            foreach (DataRow dr in GetProfitCenter(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ProfitCenterCode"].ToString() + " " + dr["PCName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ProfitCenterCode"].ToString()
                };

                itemCollection.Attributes.Add("ProfitCenterCode", dr["ProfitCenterCode"].ToString());
                itemCollection.Attributes.Add("PCName", dr["PCName"].ToString());

                RCBProfitCenter.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
