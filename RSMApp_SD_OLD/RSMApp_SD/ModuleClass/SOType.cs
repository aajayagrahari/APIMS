
//Created On :: 22, May, 2012
//Private const string ClassName = "SOType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    [Serializable]
    public class SOType
    {
        private string _SOTypeCode;
        private string _SOCategoryCode;
        private string _SOTypeDesc;
        private string _NumRange;
        private string _RangeFrom;
        private string _RangeTo;
        private string _ItemCategoryCode;
        private string _CRLimitCheckType;
        private int _ItemNoIncr;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string SOTypeCode
        {
            get
            {
                return _SOTypeCode;
            }
            set
            {
                _SOTypeCode = value;
            }
        }
        public string SOCategoryCode
        {
            get
            {
                return _SOCategoryCode;
            }
            set
            {
                _SOCategoryCode = value;
            }
        }
        public string SOTypeDesc
        {
            get
            {
                return _SOTypeDesc;
            }
            set
            {
                _SOTypeDesc = value;
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
        public string RangeFrom
        {
            get
            {
                return _RangeFrom;
            }
            set
            {
                _RangeFrom = value;
            }
        }
        public string RangeTo
        {
            get
            {
                return _RangeTo;
            }
            set
            {
                _RangeTo = value;
            }
        }
        public string ItemCategoryCode
        {
            get
            {
                return _ItemCategoryCode;
            }
            set
            {
                _ItemCategoryCode = value;
            }
        }
        public string CRLimitCheckType
        {
            get
            {
                return _CRLimitCheckType;
            }
            set
            {
                _CRLimitCheckType = value;
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
            this.SOTypeCode = Convert.ToString(dr["SOTypeCode"]);
            this.SOCategoryCode = Convert.ToString(dr["SOCategoryCode"]);
            this.SOTypeDesc = Convert.ToString(dr["SOTypeDesc"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.RangeFrom = Convert.ToString(dr["RangeFrom"]);
            this.RangeTo = Convert.ToString(dr["RangeTo"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.CRLimitCheckType = Convert.ToString(dr["CRLimitCheckType"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}