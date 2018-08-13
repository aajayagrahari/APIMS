using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;

namespace RSMApp_Extended
{
    public class AccountDocTypeManagerExt : AccountDocTypeManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBAccountDocType, string argClientCode, int iIsDeleted)
        {
            RCBAccountDocType.Items.Clear();
            foreach (DataRow dr in GetAccountDocType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["AccountDocTypeCode"].ToString() + " " + dr["AccountTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["AccountDocTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("AccountDocTypeCode", dr["AccountDocTypeCode"].ToString());
                itemCollection.Attributes.Add("AccountTypeDesc", dr["AccountTypeDesc"].ToString());
                itemCollection.Attributes.Add("IsDocHeaderAllowed", dr["IsDocHeaderAllowed"].ToString());
                itemCollection.Attributes.Add("IsReferenceAllowed", dr["IsReferenceAllowed"].ToString());

                RCBAccountDocType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
