using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_Partner;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace RSMApp_Extended
{
    public class PartnerTypeMasterManagerExt : PartnerTypeMasterManager
    {

        public void FillCombobox(RadComboBox RCBPartnerDocType, string argClientCode, int iIsDeleted)
        {
            RCBPartnerDocType.Items.Clear();
            foreach (DataRow dr in GetPartnerTypeMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PartnerTypeCode"].ToString() + " " + dr["PartnerTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PartnerTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PartnerTypeCode", dr["PartnerTypeCode"].ToString());
                itemCollection.Attributes.Add("PartnerTypeDesc", dr["PartnerTypeDesc"].ToString());
                itemCollection.Attributes.Add("NumRange", dr["NumRange"].ToString());

                RCBPartnerDocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
