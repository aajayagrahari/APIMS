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
    public class CustomerManagerExt : CustomerManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCustomer, string argClientCode, int iIsDeleted)
        {
            RCBCustomer.Items.Clear();
            foreach (DataRow dr in GetCustomer(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CustomerCode"].ToString().Trim() + " " + dr["CustomerName"].ToString() + " " + dr["City"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CustomerCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CustomerCode", dr["CustomerCode"].ToString().Trim());
                itemCollection.Attributes.Add("CustomerName", dr["CustomerName"].ToString());
                itemCollection.Attributes.Add("CurrencyCode", dr["CurrencyCode"].ToString());
                itemCollection.Attributes.Add("City", dr["City"].ToString());


                RCBCustomer.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtCustData = GetCustomer4Combo(context.Text.ToString(), context["SalesOfficeCode"].ToString().Trim(), strClientCode).Tables[0];

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

                        String itemText = dtCustData.Rows[i]["CustomerCode"].ToString().Trim() + " " + dtCustData.Rows[i]["Name1"].ToString() + " " + dtCustData.Rows[i]["City"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtCustData.Rows[i]["CustomerCode"].ToString().Trim();
                        itemData.Attributes["CustomerCode"] = dtCustData.Rows[i]["CustomerCode"].ToString().Trim();
                        itemData.Attributes["Name1"] = dtCustData.Rows[i]["Name1"].ToString().Trim();
                        itemData.Attributes["StateCode"] = dtCustData.Rows[i]["StateCode"].ToString().Trim();
                        itemData.Attributes["SalesDistrictCode"] = dtCustData.Rows[i]["SalesDistrictCode"].ToString().Trim();
                        itemData.Attributes["SalesRegionCode"] = dtCustData.Rows[i]["SalesRegionCode"].ToString().Trim();
                        itemData.Attributes["SalesOfficeCode"] = dtCustData.Rows[i]["SalesOfficeCode"].ToString().Trim();
                        itemData.Attributes["SalesGroupCode"] = dtCustData.Rows[i]["SalesGroupCode"].ToString().Trim();
                        itemData.Attributes["VATNo"] = dtCustData.Rows[i]["VATNo"].ToString().Trim();
                        //itemData.Attributes["IsTaxExempted"] = dtCustData.Rows[i]["IsTaxExempted"].ToString().Trim();
                        itemData.Attributes["SalesOrganizationCode"] = dtCustData.Rows[i]["SalesOrganizationCode"].ToString().Trim();
                        itemData.Attributes["DistChannelCode"] = dtCustData.Rows[i]["DistChannelCode"].ToString().Trim();
                        itemData.Attributes["DivisionCode"] = dtCustData.Rows[i]["DivisionCode"].ToString().Trim();
                        itemData.Attributes["CurrencyCode"] = dtCustData.Rows[i]["CurrencyCode"].ToString().Trim();
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

        public RadComboBoxData FillRadComboData4Payment(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtCustData = GetCustomer4Combo(context.Text.ToString(), strClientCode).Tables[0];

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

                        String itemText = dtCustData.Rows[i]["CustomerCode"].ToString().Trim() + " " + dtCustData.Rows[i]["Name1"].ToString() + " " + dtCustData.Rows[i]["City"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtCustData.Rows[i]["CustomerCode"].ToString().Trim();
                        itemData.Attributes["CustomerCode"] = dtCustData.Rows[i]["CustomerCode"].ToString().Trim();
                        itemData.Attributes["Name1"] = dtCustData.Rows[i]["Name1"].ToString().Trim();
                        itemData.Attributes["StateCode"] = dtCustData.Rows[i]["StateCode"].ToString().Trim();
                        itemData.Attributes["VATNo"] = dtCustData.Rows[i]["VATNo"].ToString().Trim();
                        itemData.Attributes["SalesOrganizationCode"] = dtCustData.Rows[i]["SalesOrganizationCode"].ToString().Trim();
                        itemData.Attributes["DistChannelCode"] = dtCustData.Rows[i]["DistChannelCode"].ToString().Trim();
                        itemData.Attributes["DivisionCode"] = dtCustData.Rows[i]["DivisionCode"].ToString().Trim();
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
