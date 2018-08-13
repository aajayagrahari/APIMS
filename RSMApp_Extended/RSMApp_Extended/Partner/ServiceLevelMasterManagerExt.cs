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
    public class ServiceLevelMasterManagerExt : ServiceLevelMasterManager
    {

        public void FillCombobox(RadComboBox RCBServiceLevel, string argClientCode, int iIsDeleted)
        {
            RCBServiceLevel.Items.Clear();
            foreach (DataRow dr in GetServiceLevelMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ServiceLevel"].ToString() + " " + dr["ServiceDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ServiceLevel"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ServiceLevel", dr["ServiceLevel"].ToString());
                itemCollection.Attributes.Add("ServiceDesc", dr["ServiceDesc"].ToString());

                RCBServiceLevel.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

       
    }
}
