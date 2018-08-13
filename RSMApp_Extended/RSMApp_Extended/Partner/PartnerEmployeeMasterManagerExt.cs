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
    public class PartnerEmployeeMasterManagerExt : PartnerEmployeeMasterManager
    {

        public void FillCombobox(RadComboBox RCBPartnerMaster, string argClientCode, int iIsDeleted)
        {
            RCBPartnerMaster.Items.Clear();
            foreach (DataRow dr in GetPartnerEmployeeMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PartnerEmployeeCode"].ToString() + " " + dr["EmployeeName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PartnerEmployeeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PartnerEmployeeCode", dr["PartnerEmployeeCode"].ToString());
                itemCollection.Attributes.Add("EmployeeName", dr["EmployeeName"].ToString());
              
                RCBPartnerMaster.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox(RadComboBox RCBPartnerMaster, string argPartnerCode,  string argClientCode, int iIsDeleted)
        {
            RCBPartnerMaster.Items.Clear();
            foreach (DataRow dr in GetPartnerEmployee4Combo(argPartnerCode,argClientCode,iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["PartnerEmployeeCode"].ToString() + " " + dr["EmployeeName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PartnerEmployeeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PartnerEmployeeCode", dr["PartnerEmployeeCode"].ToString());
                itemCollection.Attributes.Add("EmployeeName", dr["EmployeeName"].ToString());

                RCBPartnerMaster.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

    }
}
