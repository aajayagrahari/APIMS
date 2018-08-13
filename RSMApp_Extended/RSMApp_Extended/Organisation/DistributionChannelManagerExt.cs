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
    public class DistributionChannelManagerExt : DistributionChannelManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtDistChannelData = GetDistChannel4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtDistChannelData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtDistChannelData.Rows.Count)
                    {
                        endOffset = dtDistChannelData.Rows.Count;
                    }
                    if (endOffset == dtDistChannelData.Rows.Count)
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

                        String itemText = dtDistChannelData.Rows[i]["DistChannelCode"].ToString().Trim() + " " + dtDistChannelData.Rows[i]["DistChannel"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtDistChannelData.Rows[i]["DistChannelCode"].ToString().Trim();
                        itemData.Attributes["DistChannelCode"] = dtDistChannelData.Rows[i]["DistChannelCode"].ToString().Trim();
                        itemData.Attributes["DistChannel"] = dtDistChannelData.Rows[i]["DistChannel"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtDistChannelData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtDistChannelData.Rows.Count.ToString());
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDistChannel, string argClientCode, int iIsDeleted)
        {
            RCBDistChannel.Items.Clear();
            foreach (DataRow dr in GetDistributionChannel(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DistChannelCode"].ToString().Trim() + " " + dr["DistChannel"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DistChannelCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DistChannelCode", dr["DistChannelCode"].ToString().Trim());
                itemCollection.Attributes.Add("DistChannel", dr["DistChannel"].ToString());

                RCBDistChannel.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDistChannel, string argClientCode, string argSalesOrganizationCode)
        {
            RCBDistChannel.Items.Clear();
            foreach (DataRow dr in GetDistributionChannel4SalesOrg(argSalesOrganizationCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DistChannelCode"].ToString().Trim() + " " + dr["DistChannel"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DistChannelCode"].ToString()
                };

                itemCollection.Attributes.Add("DistChannelCode", dr["DistChannelCode"].ToString().Trim());
                itemCollection.Attributes.Add("DistChannel", dr["DistChannel"].ToString());

                RCBDistChannel.Items.Add(itemCollection);
                itemCollection.DataBind();
            }


        }
    }
}
