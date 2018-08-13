using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_FI;
using System.Data;
using Telerik.Web.UI;
using RSMApp_SD;
using RSMApp_Organization;

namespace RSMApp_Extended
{
    public class SalesofficeManagerExt : SalesofficeManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtSalesOfficeData = GetSalesOffice4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtSalesOfficeData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtSalesOfficeData.Rows.Count)
                    {
                        endOffset = dtSalesOfficeData.Rows.Count;
                    }
                    if (endOffset == dtSalesOfficeData.Rows.Count)
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

                        String itemText = dtSalesOfficeData.Rows[i]["SalesofficeCode"].ToString().Trim() + " " + dtSalesOfficeData.Rows[i]["SalesofficeName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtSalesOfficeData.Rows[i]["SalesofficeCode"].ToString().Trim();
                        itemData.Attributes["SalesofficeCode"] = dtSalesOfficeData.Rows[i]["SalesofficeCode"].ToString().Trim();
                        itemData.Attributes["SalesofficeName"] = dtSalesOfficeData.Rows[i]["SalesofficeName"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtSalesOfficeData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtSalesOfficeData.Rows.Count.ToString());
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesOffice, string argClientCode, int iIsDeleted)
        {
            RCBSalesOffice.Items.Clear();
            foreach (DataRow dr in GetSalesoffice(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SalesofficeCode"].ToString().Trim() + " " + dr["SalesofficeName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SalesofficeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SalesofficeCode", dr["SalesofficeCode"].ToString().Trim());
                itemCollection.Attributes.Add("SalesofficeName", dr["SalesofficeName"].ToString());

                RCBSalesOffice.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
