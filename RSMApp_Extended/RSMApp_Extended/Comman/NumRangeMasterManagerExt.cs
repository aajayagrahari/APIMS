using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class NumRangeMasterManagerExt : NumRangeMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBNumRange, int iIsDeleted, string argClientCode)
        {
            RCBNumRange.Items.Clear();
            foreach (DataRow dr in GetNumRangeMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["NumRangeCode"].ToString() + " " + dr["Prefix"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["NumRangeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("NumRangeCode", dr["NumRangeCode"].ToString().Trim());
                itemCollection.Attributes.Add("Prefix", dr["Prefix"].ToString());
                itemCollection.Attributes.Add("FromRange", dr["FromRange"].ToString());
                itemCollection.Attributes.Add("ToRange", dr["ToRange"].ToString());

                RCBNumRange.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
