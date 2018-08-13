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
    public class PartnerMaterialDocTypeManagerExt : PartnerMaterialDocTypeManager
    {

        public void FillCombobox(RadComboBox RCBPartnerDocTypeMaster, string argClientCode, int iIsDeleted)
        {
            RCBPartnerDocTypeMaster.Items.Clear();
            foreach (DataRow dr in GetPartnerMaterialDocType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["MaterialDocTypeCode"].ToString() + " " + dr["MaterialDocTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["MaterialDocTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("MaterialDocTypeCode", dr["MaterialDocTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("MaterialDocTypeDesc", dr["MaterialDocTypeDesc"].ToString());

                RCBPartnerDocTypeMaster.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
