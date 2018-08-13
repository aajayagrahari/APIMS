using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class CalculationTypeMasterManagerExt : CalculationTypeMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCalculationType, string argClientCode)
        {
            RCBCalculationType.Items.Clear();
            foreach (DataRow dr in GetCalculationTypeMaster(argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CalculationTypeCode"].ToString() + " " + dr["CalculationTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CalculationTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CalculationTypeCode", dr["CalculationTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("CalculationTypeDesc", dr["CalculationTypeDesc"].ToString());

                RCBCalculationType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
