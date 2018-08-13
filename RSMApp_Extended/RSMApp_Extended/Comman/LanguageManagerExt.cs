using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;

namespace RSMApp_Extended
{
    public class LanguageManagerExt : LanguageManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBLanguage, int iIsDeleted)
        {
            RCBLanguage.Items.Clear();
            foreach (DataRow dr in GetLanguage().Tables[0].Rows)
            {
                String itemText = dr["LanguageCode"].ToString() + " " + dr["LanguageName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["LanguageCode"].ToString()
                };

                itemCollection.Attributes.Add("LanguageCode", dr["LanguageCode"].ToString());
                itemCollection.Attributes.Add("LanguageName", dr["LanguageName"].ToString());

                RCBLanguage.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
