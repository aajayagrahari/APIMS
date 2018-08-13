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
    public class DivisionManagerExt : DivisionManager
    {
        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtDivisionData = GetDivision4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtDivisionData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtDivisionData.Rows.Count)
                    {
                        endOffset = dtDivisionData.Rows.Count;
                    }
                    if (endOffset == dtDivisionData.Rows.Count)
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

                        String itemText = dtDivisionData.Rows[i]["DivisionCode"].ToString().Trim() + " " + dtDivisionData.Rows[i]["DivisionName"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtDivisionData.Rows[i]["DivisionCode"].ToString().Trim();
                        itemData.Attributes["DivisionCode"] = dtDivisionData.Rows[i]["DivisionCode"].ToString().Trim();
                        itemData.Attributes["DivisionName"] = dtDivisionData.Rows[i]["DivisionName"].ToString().Trim();

                        result.Add(itemData);
                    }

                    if (dtDivisionData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtDivisionData.Rows.Count.ToString());
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDivision, string argClientCode, int iIsDeleted)
        {
            RCBDivision.Items.Clear();
            foreach (DataRow dr in GetDivision(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DivisionCode"].ToString().Trim() + " " + dr["DivisionName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DivisionCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DivisionCode", dr["DivisionCode"].ToString().Trim());
                itemCollection.Attributes.Add("DivisionName", dr["DivisionName"].ToString());

                RCBDivision.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBDivision, string argClientCode, string argSalesOrganizationCode)
        {
            RCBDivision.Items.Clear();
            foreach (DataRow dr in GetDivision4SalesOrg(argSalesOrganizationCode, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["DivisionCode"].ToString().Trim() + " " + dr["DivisionName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["DivisionCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("DivisionCode", dr["DivisionCode"].ToString().Trim());
                itemCollection.Attributes.Add("DivisionName", dr["DivisionName"].ToString());

                RCBDivision.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }
    }
}
