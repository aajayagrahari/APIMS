using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class SpecialGLIndicatorManagerExt : SpecialGLIndicatorManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBGLIndicator, string argClientCode, int iIsDeleted)
        {
            RCBGLIndicator.Items.Clear();
            foreach (DataRow dr in GetSpecialGLIndicator(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SpecialGLIndCode"].ToString() + " " + dr["SpecialGLIndDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SpecialGLIndCode"].ToString()
                };

                itemCollection.Attributes.Add("SpecialGLIndCode", dr["SpecialGLIndCode"].ToString());
                itemCollection.Attributes.Add("SpecialGLIndDesc", dr["SpecialGLIndDesc"].ToString());


                RCBGLIndicator.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
