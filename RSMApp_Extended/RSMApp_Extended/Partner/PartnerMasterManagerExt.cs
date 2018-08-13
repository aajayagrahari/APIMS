using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_Partner;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace RSMApp_Extended
{
    public class PartnerMasterManagerExt : PartnerMasterManager
    {
        public void FillCombobox(RadComboBox RCBPartnerMaster, string argClientCode, int iIsDeleted)
        {
            RCBPartnerMaster.Items.Clear();
            foreach (DataRow dr in GetPartnerMaster(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["PartnerCode"].ToString() + "  " + dr["PartnerName"].ToString() + "  " + dr["PartnerTypeCode"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["PartnerCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("PartnerCode", dr["PartnerCode"].ToString());
                itemCollection.Attributes.Add("PartnerName", dr["PartnerName"].ToString());
                itemCollection.Attributes.Add("PartnerTypeCode", dr["PartnerTypeCode"].ToString());
                RCBPartnerMaster.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtPartnerData = GetPartner4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtPartnerData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtPartnerData.Rows.Count)
                    {
                        endOffset = dtPartnerData.Rows.Count;
                    }
                    if (endOffset == dtPartnerData.Rows.Count)
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

                        String itemText = dtPartnerData.Rows[i]["PartnerCode"].ToString().Trim() + " " + dtPartnerData.Rows[i]["PartnerName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtPartnerData.Rows[i]["PartnerCode"].ToString().Trim();
                        itemData.Attributes["PartnerCode"] = dtPartnerData.Rows[i]["PartnerCode"].ToString().Trim();
                        itemData.Attributes["PartnerName"] = dtPartnerData.Rows[i]["PartnerName"].ToString().Trim();
                        itemData.Attributes["PartnerTypeCode"] = dtPartnerData.Rows[i]["PartnerTypeCode"].ToString().Trim();
                       
                        result.Add(itemData);
                    }

                    if (dtPartnerData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtPartnerData.Rows.Count.ToString());
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

        public RadComboBoxData FillRadComboData4AssignPartner(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtPartnerData = GetPartner4AssignPartnerCombo(context["PartnerCode"].ToString(), context.Text.ToString(), strClientCode).Tables[0];

            if (dtPartnerData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtPartnerData.Rows.Count)
                    {
                        endOffset = dtPartnerData.Rows.Count;
                    }
                    if (endOffset == dtPartnerData.Rows.Count)
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

                        String itemText = dtPartnerData.Rows[i]["PartnerCode"].ToString().Trim() + " " + dtPartnerData.Rows[i]["PartnerName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtPartnerData.Rows[i]["PartnerCode"].ToString().Trim();
                        itemData.Attributes["PartnerCode"] = dtPartnerData.Rows[i]["PartnerCode"].ToString().Trim();
                        itemData.Attributes["PartnerName"] = dtPartnerData.Rows[i]["PartnerName"].ToString().Trim();
                        itemData.Attributes["PartnerTypeCode"] = dtPartnerData.Rows[i]["PartnerTypeCode"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtPartnerData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtPartnerData.Rows.Count.ToString());
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
