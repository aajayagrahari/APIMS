
//Created On :: 28, September, 2012
//Private const string ClassName = "AccountDocType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_FI
{
    [Serializable]
    public class AccountDocType
    {
        private string _AccountDocTypeCode;
        private string _AccountTypeDesc;
        private int _ItemNoIncr;
        private string _NumRange;
        private int _SaveMode;
        private string _RevAccountDocTypeCode;
        private int _AccTypeAllowedAsset;
        private int _AccTypeAllowedCustomer;
        private int _AccTypeAllowedVendor;
        private int _AccTypeAllowedMaterial;
        private int _AccTypeAllowedGL;
        private int _IsNegativePosting;
        private int _IsDocHeaderAllowed;
        private int _IsReferenceAllowed;
        private string _ExchangeRateType;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string AccountDocTypeCode
        {
            get
            {
                return _AccountDocTypeCode;
            }
            set
            {
                _AccountDocTypeCode = value;
            }
        }
        public string AccountTypeDesc
        {
            get
            {
                return _AccountTypeDesc;
            }
            set
            {
                _AccountTypeDesc = value;
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
        public int SaveMode
        {
            get
            {
                return _SaveMode;
            }
            set
            {
                _SaveMode = value;
            }
        }

        public string RevAccountDocTypeCode
        {
            get
            {
                return _RevAccountDocTypeCode;
            }
            set
            {
                _RevAccountDocTypeCode = value;
            }
        }
        public int AccTypeAllowedAsset
        {
            get
            {
                return _AccTypeAllowedAsset;
            }
            set
            {
                _AccTypeAllowedAsset = value;
            }
        }
        public int AccTypeAllowedCustomer
        {
            get
            {
                return _AccTypeAllowedCustomer;
            }
            set
            {
                _AccTypeAllowedCustomer = value;
            }
        }
        public int AccTypeAllowedVendor
        {
            get
            {
                return _AccTypeAllowedVendor;
            }
            set
            {
                _AccTypeAllowedVendor = value;
            }
        }
        public int AccTypeAllowedMaterial
        {
            get
            {
                return _AccTypeAllowedMaterial;
            }
            set
            {
                _AccTypeAllowedMaterial = value;
            }
        }
        public int AccTypeAllowedGL
        {
            get
            {
                return _AccTypeAllowedGL;
            }
            set
            {
                _AccTypeAllowedGL = value;
            }
        }
        public int IsNegativePosting
        {
            get
            {
                return _IsNegativePosting;
            }
            set
            {
                _IsNegativePosting = value;
            }
        }
        public int IsDocHeaderAllowed
        {
            get
            {
                return _IsDocHeaderAllowed;
            }
            set
            {
                _IsDocHeaderAllowed = value;
            }
        }
        public int IsReferenceAllowed
        {
            get
            {
                return _IsReferenceAllowed;
            }
            set
            {
                _IsReferenceAllowed = value;
            }
        }
        public string ExchangeRateType
        {
            get
            {
                return _ExchangeRateType;
            }
            set
            {
                _ExchangeRateType = value;
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
            this.AccountDocTypeCode = Convert.ToString(dr["AccountDocTypeCode"]);
            this.AccountTypeDesc = Convert.ToString(dr["AccountTypeDesc"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.SaveMode = Convert.ToInt32(dr["SaveMode"]);
            this.RevAccountDocTypeCode = Convert.ToString(dr["RevAccountDocTypeCode"]);
            this.AccTypeAllowedAsset = Convert.ToInt32(dr["AccTypeAllowedAsset"]);
            this.AccTypeAllowedCustomer = Convert.ToInt32(dr["AccTypeAllowedCustomer"]);
            this.AccTypeAllowedVendor = Convert.ToInt32(dr["AccTypeAllowedVendor"]);
            this.AccTypeAllowedMaterial = Convert.ToInt32(dr["AccTypeAllowedMaterial"]);
            this.AccTypeAllowedGL = Convert.ToInt32(dr["AccTypeAllowedGL"]);
            this.IsNegativePosting = Convert.ToInt32(dr["IsNegativePosting"]);
            this.IsDocHeaderAllowed = Convert.ToInt32(dr["IsDocHeaderAllowed"]);
            this.IsReferenceAllowed = Convert.ToInt32(dr["IsReferenceAllowed"]);
            this.ExchangeRateType = Convert.ToString(dr["ExchangeRateType"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}