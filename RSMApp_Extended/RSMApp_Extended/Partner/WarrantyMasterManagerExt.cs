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
    public class WarrantyMasterManagerExt : WarrantyMasterManager
    {

        public void FillCombobox(RadComboBox RCBWarrantyCode, string argClientCode, int iIsDeleted)
        {
            RCBWarrantyCode.Items.Clear();
            foreach (DataRow dr in GetWarrantyMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["WarrantyCode"].ToString() + " " + dr["WarrantyName"].ToString() + " " + dr["MatGroup1Code"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["WarrantyCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("WarrantyCode", dr["WarrantyCode"].ToString());
                itemCollection.Attributes.Add("WarrantyName", dr["WarrantyName"].ToString());
                itemCollection.Attributes.Add("MatGroup1Code", dr["MatGroup1Code"].ToString());
                itemCollection.Attributes.Add("ValidFrom", dr["ValidFrom"].ToString());
                itemCollection.Attributes.Add("ValidTo", dr["ValidTo"].ToString());

                RCBWarrantyCode.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
