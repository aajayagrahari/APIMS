using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_FI;

namespace RSMApp_Extended
{
    public class GLAccountManagerExt : GLAccountManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBGLAccount, string argClientCode, int iIsDeleted)
        {
            RCBGLAccount.Items.Clear();
            foreach (DataRow dr in GetGLAccount(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["GLCode"].ToString() + " " + dr["GLDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["GLCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("GLCode", dr["GLCode"].ToString());
                itemCollection.Attributes.Add("GLDesc", dr["GLDesc"].ToString());

                RCBGLAccount.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }


        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtCustData = GetGLAccount4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtCustData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtCustData.Rows.Count)
                    {
                        endOffset = dtCustData.Rows.Count;
                    }
                    if (endOffset == dtCustData.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>();

                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();

                        String itemText = dtCustData.Rows[i]["GLCode"].ToString().Trim() + " " + dtCustData.Rows[i]["GLDesc"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtCustData.Rows[i]["GLCode"].ToString().Trim();
                        itemData.Attributes["GLCode"] = dtCustData.Rows[i]["GLCode"].ToString().Trim();
                        itemData.Attributes["GLDesc"] = dtCustData.Rows[i]["GLDesc"].ToString().Trim();
                        //itemData.Attributes["CurrencyCode"] = dtCustData.Rows[i]["CurrencyCode"].ToString().Trim();
                        result.Add(itemData);
                    }

                    if (dtCustData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtCustData.Rows.Count.ToString());
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }

                }
                catch (Exception e)
                {
                    comboData.Message = e.Message;
                }

                comboData.Items = result.ToArray();
            }
            return comboData;
        }

    }
}
