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
    public class CallTypeManagerExt : CallTypeManager
    {

        public void FillCombobox(RadComboBox RCBCallType, string argClientCode, int iIsDeleted)
        {
            RCBCallType.Items.Clear();
            foreach (DataRow dr in GetCallType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CallTypeCode"].ToString() + " " + dr["CallTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CallTypeCode"].ToString().Trim()
                };

                itemCollection.Attributes.Add("CallTypeCode", dr["CallTypeCode"].ToString());
                itemCollection.Attributes.Add("CallTypeDesc", dr["CallTypeDesc"].ToString());

                RCBCallType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        //public void FillCombobox(RadComboBox RCBCallType, string argRepairDocType, string argClientCode, int iIsDeleted)
        //{
        //    RCBCallType.Items.Clear();
        //    foreach (DataRow dr in GetCallType4RepairDocType(argRepairDocType, argClientCode, iIsDeleted).Tables[0].Rows)
        //    {
        //        String itemText = dr["CallTypeCode"].ToString() + " " + dr["CallTypeDesc"].ToString();
        //        var itemCollection = new RadComboBoxItem
        //        {
        //            Text = itemText,
        //            Value = dr["CallTypeCode"].ToString().Trim()
        //        };

        //        itemCollection.Attributes.Add("CallTypeCode", dr["CallTypeCode"].ToString());
        //        itemCollection.Attributes.Add("CallTypeDesc", dr["CallTypeDesc"].ToString());

        //        RCBCallType.Items.Add(itemCollection);
        //        itemCollection.DataBind();
        //    }
        //}

    }
}
