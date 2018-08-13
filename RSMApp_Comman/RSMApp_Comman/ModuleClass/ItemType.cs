
//Created On :: 06, September, 2012
//Private const string ClassName = "ItemType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    [Serializable]
    public class ItemType
    {
        private string _ItemTypeCode;
        private string _ItemTypeDesc;
        private int _IsDeleted;



        public string ItemTypeCode
        {
            get
            {
                return _ItemTypeCode;
            }
            set
            {
                _ItemTypeCode = value;
            }
        }
        public string ItemTypeDesc
        {
            get
            {
                return _ItemTypeDesc;
            }
            set
            {
                _ItemTypeDesc = value;
            }
        }
        public int IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            set
            {
                _IsDeleted = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.ItemTypeCode = Convert.ToString(dr["ItemTypeCode"]);
            this.ItemTypeDesc = Convert.ToString(dr["ItemTypeDesc"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}