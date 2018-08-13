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
    public class IncomingInvDocTypeManagerExt : IncomingInvDocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBIncomingInvDocType, string argClientCode, int iIsDeleted)
        {
            RCBIncomingInvDocType.Items.Clear();
            foreach (DataRow dr in GetIncomingInvDocType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["IncomingInvDocTypeCode"].ToString() + " " + dr["IncomingInvTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["IncomingInvDocTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("IncomingInvDocTypeCode", dr["IncomingInvDocTypeCode"].ToString());
                itemCollection.Attributes.Add("IncomingInvTypeDesc", dr["IncomingInvTypeDesc"].ToString());
                itemCollection.Attributes.Add("NumRange", dr["NumRange"].ToString());


                RCBIncomingInvDocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
