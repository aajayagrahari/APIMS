using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class IncoTermsManagerExt : IncoTermsManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBIncoTerms, string argClientCode, int iIsDeleted)
        {
            RCBIncoTerms.Items.Clear();
            foreach (DataRow dr in GetIncoTerms(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["IncoTermsCode"].ToString() + " " + dr["IncoTermsDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["IncoTermsCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("IncoTermsCode", dr["IncoTermsCode"].ToString());
                itemCollection.Attributes.Add("IncoTermsDesc", dr["IncoTermsDesc"].ToString());

                RCBIncoTerms.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
