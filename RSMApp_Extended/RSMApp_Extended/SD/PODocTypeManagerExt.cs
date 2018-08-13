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
    public class PODocTypeManagerExt : PODocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBPODocType, string argClientCode, int iIsDeleted)
        {
            RCBPODocType.Items.Clear();
            foreach (DataRow dr in GetPODocType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PODocTypeCode"].ToString() + " " + dr["PODocTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PODocTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PODocTypeCode", dr["PODocTypeCode"].ToString());
                itemCollection.Attributes.Add("PODocTypeDesc", dr["PODocTypeDesc"].ToString());
        

                RCBPODocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
