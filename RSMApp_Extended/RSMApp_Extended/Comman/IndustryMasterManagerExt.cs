using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class IndustryMasterManagerExt : IndustryMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBIndustryMaster, string argClientCode, int iIsDeleted)
        {
            RCBIndustryMaster.Items.Clear();
            foreach (DataRow dr in GetIndustryMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["IndustryName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["IndustryName"].ToString().Trim()
                };

                itemCollection.Attributes.Add("IndustryName", dr["IndustryName"].ToString());


                RCBIndustryMaster.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
