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
    public class SalesOrganizationManagerExt : SalesOrganizationManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtSalesOrgData = GetSalesOrganisation4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtSalesOrgData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtSalesOrgData.Rows.Count)
                    {
                        endOffset = dtSalesOrgData.Rows.Count;
                    }
                    if (endOffset == dtSalesOrgData.Rows.Count)
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

                        String itemText = dtSalesOrgData.Rows[i]["SalesOrganizationCode"].ToString().Trim() + " " + dtSalesOrgData.Rows[i]["SalesOrgName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtSalesOrgData.Rows[i]["SalesOrganizationCode"].ToString().Trim();
                        itemData.Attributes["SalesOrganizationCode"] = dtSalesOrgData.Rows[i]["SalesOrganizationCode"].ToString().Trim();
                        itemData.Attributes["SalesOrgName"] = dtSalesOrgData.Rows[i]["SalesOrgName"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtSalesOrgData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtSalesOrgData.Rows.Count.ToString());
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSalesOrg, string argClientCode, int iIsDeleted)
        {
            RCBSalesOrg.Items.Clear();
            foreach (DataRow dr in GetSalesOrganization(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SalesOrganizationCode"].ToString().Trim() + " " + dr["SalesOrgName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SalesOrganizationCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("SalesOrganizationCode", dr["SalesOrganizationCode"].ToString().Trim());
                itemCollection.Attributes.Add("SalesOrgName", dr["SalesOrgName"].ToString());

                RCBSalesOrg.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
