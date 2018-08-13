
//Created On :: 09, January, 2013
//Private const string ClassName = "CanabalizeDocType"
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
    public class CanabalizeDocType
    {
        private string _CanablizeDocTypeCode;
        private string _CanablizeDocTypeDesc;
        private int _ItemNoIncr;
        private string _NumRange;
        private string _MatMaterialDocTypeCode;
        private string _MatDefaultStoreCode;
        private string _MatDefaultFromStockIndicator;
        private string _MatDefaultToStockIndicator;
        private string _SpareMaterialDocTypeCode;
        private string _SpareDefaultStoreCode;
        private string _SpareDefaultFromStockIndicator;
        private string _SpareDefaultToStockIndicator;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string CanablizeDocTypeCode
        {
            get
            {
                return _CanablizeDocTypeCode;
            }
            set
            {
                _CanablizeDocTypeCode = value;
            }
        }
        public string CanablizeDocTypeDesc
        {
            get
            {
                return _CanablizeDocTypeDesc;
            }
            set
            {
                _CanablizeDocTypeDesc = value;
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
        public string MatMaterialDocTypeCode
        {
            get
            {
                return _MatMaterialDocTypeCode;
            }
            set
            {
                _MatMaterialDocTypeCode = value;
            }
        }
        public string MatDefaultStoreCode
        {
            get
            {
                return _MatDefaultStoreCode;
            }
            set
            {
                _MatDefaultStoreCode = value;
            }
        }
        public string MatDefaultFromStockIndicator
        {
            get
            {
                return _MatDefaultFromStockIndicator;
            }
            set
            {
                _MatDefaultFromStockIndicator = value;
            }
        }
        public string MatDefaultToStockIndicator
        {
            get
            {
                return _MatDefaultToStockIndicator;
            }
            set
            {
                _MatDefaultToStockIndicator = value;
            }
        }
        public string SpareMaterialDocTypeCode
        {
            get
            {
                return _SpareMaterialDocTypeCode;
            }
            set
            {
                _SpareMaterialDocTypeCode = value;
            }
        }
        public string SpareDefaultStoreCode
        {
            get
            {
                return _SpareDefaultStoreCode;
            }
            set
            {
                _SpareDefaultStoreCode = value;
            }
        }
        public string SpareDefaultFromStockIndicator
        {
            get
            {
                return _SpareDefaultFromStockIndicator;
            }
            set
            {
                _SpareDefaultFromStockIndicator = value;
            }
        }
        public string SpareDefaultToStockIndicator
        {
            get
            {
                return _SpareDefaultToStockIndicator;
            }
            set
            {
                _SpareDefaultToStockIndicator = value;
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
            this.CanablizeDocTypeCode = Convert.ToString(dr["CanablizeDocTypeCode"]);
            this.CanablizeDocTypeDesc = Convert.ToString(dr["CanablizeDocTypeDesc"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.MatMaterialDocTypeCode = Convert.ToString(dr["MatMaterialDocTypeCode"]);
            this.MatDefaultStoreCode = Convert.ToString(dr["MatDefaultStoreCode"]);
            this.MatDefaultFromStockIndicator = Convert.ToString(dr["MatDefaultFromStockIndicator"]);
            this.MatDefaultToStockIndicator = Convert.ToString(dr["MatDefaultToStockIndicator"]);
            this.SpareMaterialDocTypeCode = Convert.ToString(dr["SpareMaterialDocTypeCode"]);
            this.SpareDefaultStoreCode = Convert.ToString(dr["SpareDefaultStoreCode"]);
            this.SpareDefaultFromStockIndicator = Convert.ToString(dr["SpareDefaultFromStockIndicator"]);
            this.SpareDefaultToStockIndicator = Convert.ToString(dr["SpareDefaultToStockIndicator"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}