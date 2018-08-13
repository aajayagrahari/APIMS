using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ValuationCategoryManagerExt : ValuationCategoryManager
    {

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBValCategory, string argClientCode, int iIsDeleted)
        {
            RCBValCategory.Items.Clear();
            foreach (DataRow dr in GetValuationCategory(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ValCatCode"].ToString().Trim() + " " + dr["ValCatDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ValCatCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ValCatCode", dr["ValCatCode"].ToString().Trim());
                itemCollection.Attributes.Add("ValCatDesc", dr["ValCatDesc"].ToString());


                RCBValCategory.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }
    }
}
