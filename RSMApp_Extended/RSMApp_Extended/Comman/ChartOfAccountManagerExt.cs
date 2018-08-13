using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class ChartOfAccountManagerExt : ChartOfAccountManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBChartACCode, string argClientCode, int iIsDeleted)
        {
            RCBChartACCode.Items.Clear();
            foreach (DataRow dr in GetChartOfAccount(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ChartACCode"].ToString() + " " + dr["ChartAcName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ChartACCode"].ToString()
                };

                itemCollection.Attributes.Add("ChartACCode", dr["ChartACCode"].ToString());
                itemCollection.Attributes.Add("ChartAcName", dr["ChartAcName"].ToString());

                RCBChartACCode.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
