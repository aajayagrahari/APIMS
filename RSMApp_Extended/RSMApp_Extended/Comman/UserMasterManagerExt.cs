using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Web.UI;
using RSMApp_Comman;


namespace RSMApp_Extended
{
    public class UserMasterManagerExt : UserMasterManager
    {
        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBUser, string argClientCode, int iIsDeleted)
        {
            RCBUser.Items.Clear();
            foreach (DataRow dr in GetUser(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["UserName"].ToString() + " " + dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["UserName"].ToString()
                };

                itemCollection.Attributes.Add("UserName", dr["UserName"].ToString());
                itemCollection.Attributes.Add("FirstName", dr["FirstName"].ToString());
                itemCollection.Attributes.Add("LastName", dr["LastName"].ToString());

                RCBUser.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtUserMasterData = GetUserMaster4Combo(context.Text.ToString(), strClientCode).Tables[0];

            if (dtUserMasterData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtUserMasterData.Rows.Count)
                    {
                        endOffset = dtUserMasterData.Rows.Count;
                    }
                    if (endOffset == dtUserMasterData.Rows.Count)
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

                        String itemText = dtUserMasterData.Rows[i]["UserName"].ToString().Trim();
                        itemData.Text = itemText;
                        itemData.Value = dtUserMasterData.Rows[i]["UserName"].ToString().Trim();
                        itemData.Attributes["UserName"] = dtUserMasterData.Rows[i]["UserName"].ToString().Trim();
                        
                        result.Add(itemData);
                    }

                    if (dtUserMasterData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtUserMasterData.Rows.Count.ToString());
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
