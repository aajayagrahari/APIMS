using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_MM;

namespace RSMApp_Extended
{
    public class ExtMaterialGroupManagerExt : ExtMaterialGroupManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBExtMaterialGroup, string argClientCode, int iIsDeleted)
        {
            RCBExtMaterialGroup.Items.Clear();
            foreach (DataRow dr in GetExtMaterialGroup(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ExtMatGroupCode"].ToString() + " " + dr["ExtMatGroupDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ExtMatGroupCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("ExtMatGroupCode", dr["ExtMatGroupCode"].ToString().Trim());
                itemCollection.Attributes.Add("ExtMatGroupDesc", dr["ExtMatGroupDesc"].ToString());


                RCBExtMaterialGroup.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtExtMatGroupData = GetExtMatGroup4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtExtMatGroupData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtExtMatGroupData.Rows.Count)
                    {
                        endOffset = dtExtMatGroupData.Rows.Count;
                    }
                    if (endOffset == dtExtMatGroupData.Rows.Count)
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

                        String itemText = dtExtMatGroupData.Rows[i]["ExtMatGroupCode"].ToString().Trim() + " " + dtExtMatGroupData.Rows[i]["ExtMatGroupDesc"].ToString();

                        itemData.Text = itemText;
                        itemData.Value = dtExtMatGroupData.Rows[i]["ExtMatGroupCode"].ToString().Trim();
                        itemData.Attributes["ExtMatGroupCode"] = dtExtMatGroupData.Rows[i]["ExtMatGroupCode"].ToString();
                        itemData.Attributes["ExtMatGroupDesc"] = dtExtMatGroupData.Rows[i]["ExtMatGroupDesc"].ToString();
                        result.Add(itemData);
                    }

                    if (dtExtMatGroupData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtExtMatGroupData.Rows.Count.ToString());
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
