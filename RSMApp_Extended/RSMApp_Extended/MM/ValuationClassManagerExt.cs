using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ValuationClassManagerExt : ValuationClassManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBValuationClass, string argClientCode, int iIsDeleted)
        {
            RCBValuationClass.Items.Clear();
            foreach (DataRow dr in GetValuationClass(iIsDeleted,argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ValClassType"].ToString().Trim() + " " + dr["ValClassDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ValClassType"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ValClassType", dr["ValClassType"].ToString().Trim());
                itemCollection.Attributes.Add("ValClassDesc", dr["ValClassDesc"].ToString());


                RCBValuationClass.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox4MatType(Telerik.Web.UI.RadComboBox RCBValuationClass, string argMaterialTypeCode, string argClientCode)
        {
            RCBValuationClass.Items.Clear();
            foreach (DataRow dr in GetValuationClass4MatType(argMaterialTypeCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ValClassType"].ToString().Trim() + " " + dr["ValClassDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ValClassType"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ValClassType", dr["ValClassType"].ToString().Trim());
                itemCollection.Attributes.Add("ValClassDesc", dr["ValClassDesc"].ToString());


                RCBValuationClass.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

    }
}
