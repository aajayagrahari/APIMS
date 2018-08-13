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
    public class VendorManagerExt : VendorManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBVendor, string argClientCode, int iIsDeleted)
        {
            RCBVendor.Items.Clear();
            foreach (DataRow dr in GetVendor(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["VendorCode"].ToString() + " " + dr["VendorName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["VendorCode"].ToString()
                };

                itemCollection.Attributes.Add("VendorCode", dr["VendorCode"].ToString());
                itemCollection.Attributes.Add("VendorName", dr["VendorName"].ToString());
                itemCollection.Attributes.Add("CurrencyCode", dr["CurrencyCode"].ToString());

                RCBVendor.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtCustData = GetVendor4Combo(context.Text.ToString(), strClientCode).Tables[0];

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

                        String itemText = dtCustData.Rows[i]["VendorCode"].ToString().Trim() + " " + dtCustData.Rows[i]["Name1"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtCustData.Rows[i]["VendorCode"].ToString().Trim();
                        itemData.Attributes["VendorCode"] = dtCustData.Rows[i]["VendorCode"].ToString().Trim();
                        itemData.Attributes["Name1"] = dtCustData.Rows[i]["Name1"].ToString().Trim();
                        itemData.Attributes["CurrencyCode"] = dtCustData.Rows[i]["CurrencyCode"].ToString().Trim();
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

        public RadComboBoxData FillRadComboData4PO(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtCustData = GetVendor4PO(context.Text.ToString(), context["PurchaseOrgCode"].ToString().Trim(), context["CompanyCode"].ToString().Trim(), strClientCode).Tables[0];

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

                        String itemText = dtCustData.Rows[i]["VendorCode"].ToString().Trim() + " " + dtCustData.Rows[i]["Name1"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtCustData.Rows[i]["VendorCode"].ToString().Trim();
                        itemData.Attributes["VendorCode"] = dtCustData.Rows[i]["VendorCode"].ToString().Trim();
                        itemData.Attributes["Name1"] = dtCustData.Rows[i]["Name1"].ToString().Trim();
                        itemData.Attributes["CurrencyCode"] = dtCustData.Rows[i]["CurrencyCode"].ToString().Trim();
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
