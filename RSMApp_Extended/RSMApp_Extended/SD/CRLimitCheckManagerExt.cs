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
    public class CRLimitCheckManagerExt : CRLimitCheckManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCbCRLimitCheck, string argClientCode, int iIsDeleted)
        {
            RCbCRLimitCheck.Items.Clear();
            foreach (DataRow dr in GetCRLimitCheck(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CRLimitCheckType"].ToString().Trim() + " " + dr["CRLimitCheckDesc"].ToString().Trim();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CRLimitCheckType"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CRLimitCheckType", dr["CRLimitCheckType"].ToString().Trim());
                itemCollection.Attributes.Add("CRLimitCheckDesc", dr["CRLimitCheckDesc"].ToString().Trim());
                RCbCRLimitCheck.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
