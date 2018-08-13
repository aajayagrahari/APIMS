using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class MaterialGroup3ManagerExt : MaterialGroup3Manager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBMaterialGroup3, string argClientCode, int iIsDeleted)
        {
            RCBMaterialGroup3.Items.Clear();
            foreach (DataRow dr in GetMaterialGroup2(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["MatGroup3Code"].ToString() + " " + dr["MatGroup3Desc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["MatGroup3Code"].ToString().Trim()
                };

                itemCollection.Attributes.Add("MatGroup3Code", dr["MatGroup3Code"].ToString().Trim());
                itemCollection.Attributes.Add("MatGroup3Desc", dr["MatGroup3Desc"].ToString());


                RCBMaterialGroup3.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
