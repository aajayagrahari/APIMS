using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;

namespace RSMApp_Extended
{
    public class Customer_SalesAreaManagerExt : Customer_SalesAreaManager
    {
        public RadComboBoxData FillCustRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();
            DataTable dtCustData = GetCustomer_SalesArea4Combo(context.Text.ToString(), context["SalesOrganizationCode"].ToString().Trim(), context["DivisionCode"].ToString().Trim(), context["DistChannelCode"].ToString().Trim(), strClientCode).Tables[0];

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
                        String itemText = dtCustData.Rows[i]["CustomerCode"].ToString().Trim() + "        " + dtCustData.Rows[i]["Name1"].ToString() + "            " + dtCustData.Rows[i]["City"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtCustData.Rows[i]["CustomerCode"].ToString().Trim();
                        itemData.Attributes["CustomerCode"] = dtCustData.Rows[i]["CustomerCode"].ToString().Trim();
                        itemData.Attributes["Name1"] = dtCustData.Rows[i]["Name1"].ToString().Trim();
                        itemData.Attributes["City"] = dtCustData.Rows[i]["City"].ToString().Trim();
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
