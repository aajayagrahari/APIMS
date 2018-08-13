using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class StockIndicatoresManagerExt : StockIndicatoresManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBStockIndicator, string argClientCode, int iIsDeleted)
        {
            RCBStockIndicator.Items.Clear();
            foreach (DataRow dr in GetStockIndicatores(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["StockIndicatorCode"].ToString() + " " + dr["StockIndicator"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["StockIndicatorCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("StockIndicatorCode", dr["StockIndicatorCode"].ToString());
                itemCollection.Attributes.Add("StockIndicator", dr["StockIndicator"].ToString());


                RCBStockIndicator.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
