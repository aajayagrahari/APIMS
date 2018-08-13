using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class ExchangeRateTypeManagerExt : ExchangeRateTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBExchangeRate, int iIsDeleted)
        {
            RCBExchangeRate.Items.Clear();
            foreach (DataRow dr in GetExchangeRateType(iIsDeleted).Tables[0].Rows)
            {
                String itemText = dr["ExChngRateTypeCode"].ToString() + " " + dr["ExChngRateTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ExChngRateTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ExChngRateTypeCode", dr["ExChngRateTypeCode"].ToString().Trim());
                itemCollection.Attributes.Add("ExChngRateTypeDesc", dr["ExChngRateTypeDesc"].ToString());

                RCBExchangeRate.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
