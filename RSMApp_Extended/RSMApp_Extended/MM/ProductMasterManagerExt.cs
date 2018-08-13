using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ProductMasterManagerExt : ProductMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSerialNo, string argMaterialCode,string argMatGroup1Code, string argClientCode)
        {
            RCBSerialNo.Items.Clear();
            foreach (DataRow dr in GetProductSerialNo(argMaterialCode, argMatGroup1Code,argClientCode).Tables[0].Rows)
                {
                    String itemText = dr["SerialNo"].ToString();
                    var itemCollection = new RadComboBoxItem
                    {
                        Text = itemText,
                        Value = dr["SerialNo"].ToString().Trim()
                    };

                    itemCollection.Attributes.Add("SerialNo", dr["SerialNo"].ToString().Trim());
                    itemCollection.Attributes.Add("tProductNode", dr["tProductNode"].ToString());

                    RCBSerialNo.Items.Add(itemCollection);
                    itemCollection.DataBind();
                }


            
        }

     


    }
}
