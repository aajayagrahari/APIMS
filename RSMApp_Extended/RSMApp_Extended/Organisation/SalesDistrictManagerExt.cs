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
    public class SalesDistrictManagerExt : SalesDistrictManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtSalesDistrictData = GetSalesDistrict4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtSalesDistrictData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtSalesDistrictData.Rows.Count)
                    {
                        endOffset = dtSalesDistrictData.Rows.Count;
                    }
                    if (endOffset == dtSalesDistrictData.Rows.Count)
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

                        String itemText = dtSalesDistrictData.Rows[i]["SalesDistrictCode"].ToString().Trim() + " " + dtSalesDistrictData.Rows[i]["SalesDistrictName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtSalesDistrictData.Rows[i]["SalesDistrictCode"].ToString().Trim();
                        itemData.Attributes["SalesDistrictCode"] = dtSalesDistrictData.Rows[i]["SalesDistrictCode"].ToString().Trim();
                        itemData.Attributes["SalesDistrictName"] = dtSalesDistrictData.Rows[i]["SalesDistrictName"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtSalesDistrictData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtSalesDistrictData.Rows.Count.ToString());
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesDistrict, string argClientCode, int iIsDeleted)
        {
            RCBSalesDistrict.Items.Clear();
            foreach (DataRow dr in GetSalesDistrict(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SalesDistrictCode"].ToString().Trim() + " " + dr["SalesDistrictName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SalesDistrictCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SalesDistrictCode", dr["SalesDistrictCode"].ToString().Trim());
                itemCollection.Attributes.Add("SalesDistrictName", dr["SalesDistrictName"].ToString());

                RCBSalesDistrict.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
