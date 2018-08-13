
//Created On :: 15, December, 2012
//Private const string ClassName = "CallStandByDocType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    [Serializable]
    public class CallStandByDocType
    {
        private string _CallStandByDocTypeCode;
        private string _CallStandByDocTypeDesc;
        private int _ItemNoIncr;
        private string _NumRange;
        private string _StandByType;
        private string _MaterialDocTypeCode;
        private string _RepairProcessCode;
        private string _DefaultStoreCode;
        private string _DefaultStockIndicator;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string CallStandByDocTypeCode
        {
            get
            {
                return _CallStandByDocTypeCode;
            }
            set
            {
                _CallStandByDocTypeCode = value;
            }
        }
        public string CallStandByDocTypeDesc
        {
            get
            {
                return _CallStandByDocTypeDesc;
            }
            set
            {
                _CallStandByDocTypeDesc = value;
            }
        }
        public int ItemNoIncr
        {
            get
            {
                return _ItemNoIncr;
            }
            set
            {
                _ItemNoIncr = value;
            }
        }
        public string NumRange
        {
            get
            {
                return _NumRange;
            }
            set
            {
                _NumRange = value;
            }
        }
        public string StandByType
        {
            get
            {
                return _StandByType;
            }
            set
            {
                _StandByType = value;
            }
        }
        public string MaterialDocTypeCode
        {
            get
            {
                return _MaterialDocTypeCode;
            }
            set
            {
                _MaterialDocTypeCode = value;
            }
        }
        public string RepairProcessCode
        {
            get
            {
                return _RepairProcessCode;
            }
            set
            {
                _RepairProcessCode = value;
            }
        }
        public string DefaultStoreCode
        {
            get
            {
                return _DefaultStoreCode;
            }
            set
            {
                _DefaultStoreCode = value;
            }
        }
        public string DefaultStockIndicator
        {
            get
            {
                return _DefaultStockIndicator;
            }
            set
            {
                _DefaultStockIndicator = value;
            }
        }
        public string ClientCode
        {
            get
            {
                return _ClientCode;
            }
            set
            {
                _ClientCode = value;
            }
        }
        public string CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
            }
        }
        public string ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                _ModifiedBy = value;
            }
        }
        public string CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }
        public string ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                _ModifiedDate = value;
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
            this.CallStandByDocTypeCode = Convert.ToString(dr["CallStandByDocTypeCode"]);
            this.CallStandByDocTypeDesc = Convert.ToString(dr["CallStandByDocTypeDesc"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.StandByType = Convert.ToString(dr["StandByType"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.RepairProcessCode = Convert.ToString(dr["RepairProcessCode"]);
            this.DefaultStoreCode = Convert.ToString(dr["DefaultStoreCode"]);
            this.DefaultStockIndicator = Convert.ToString(dr["DefaultStockIndicator"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}