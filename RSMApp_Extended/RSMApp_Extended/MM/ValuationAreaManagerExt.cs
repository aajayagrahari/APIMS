using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ValuationAreaManagerExt : ValuationAreaManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBValuationArea, string argClientCode, int iIsDeleted)
        {
            RCBValuationArea.Items.Clear();
            foreach (DataRow dr in GetValuationArea(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ValuationAreaCode"].ToString().Trim() + " " + dr["ValAreaDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ValuationAreaCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ValuationAreaCode", dr["ValuationAreaCode"].ToString().Trim());
                itemCollection.Attributes.Add("ValAreaDesc", dr["ValAreaDesc"].ToString());


                RCBValuationArea.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox4ValuationMode(Telerik.Web.UI.RadComboBox RCBValuationArea, int argValuationMode, string argClientCode)
        {
            RCBValuationArea.Items.Clear();
            foreach (DataRow dr in GetValuationArea4Combo(argValuationMode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ValAreaCode"].ToString().Trim() + " " + dr["ValAreaDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ValAreaCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ValAreaCode", dr["ValAreaCode"].ToString().Trim());
                itemCollection.Attributes.Add("ValAreaDesc", dr["ValAreaDesc"].ToString());


                RCBValuationArea.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
