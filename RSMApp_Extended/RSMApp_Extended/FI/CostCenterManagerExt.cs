using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class CostCenterManagerExt : CostCenterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCostCenter, string argClientCode, int iIsDeleted)
        {
            RCBCostCenter.Items.Clear();
            foreach (DataRow dr in GetCostCenter(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CostCenterCode"].ToString() + " " + dr["CCName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CostCenterCode"].ToString()
                };

                itemCollection.Attributes.Add("CostCenterCode", dr["CostCenterCode"].ToString());
                itemCollection.Attributes.Add("CCName", dr["CCName"].ToString());

                RCBCostCenter.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
