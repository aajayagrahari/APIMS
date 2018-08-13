using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class FiscalYearMasterManagerExt : FiscalYearMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDesignation,string argClientCode, int iIsDeleted)
        {
            RCBDesignation.Items.Clear();
            foreach (DataRow dr in GetFiscalYearMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["FiscalYearType"].ToString() + " " + dr["FiscalYear"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["FiscalYearType"].ToString()
                };

                itemCollection.Attributes.Add("FiscalYearType", dr["FiscalYearType"].ToString());
                itemCollection.Attributes.Add("FiscalYear", dr["FiscalYear"].ToString());
                itemCollection.Attributes.Add("FYStartDate", dr["FYStartDate"].ToString());
                itemCollection.Attributes.Add("FYEndDate", dr["FYEndDate"].ToString());

                RCBDesignation.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
