
//Created On :: 29, August, 2012
//Private const string ClassName = "BillingDocType"
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
    public class BillingDocType
    {
        private string _BillingDocTypeCode;
        private string _BillingTypeDesc;
        private string _CRLimitCheckType;
        private int _ItemNoIncr;
        private string _NumRange;
        private string _RangeFrom;
        private string _RangeTo;
        private int _BasedOn;
        private int _SaveMode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string BillingDocTypeCode
        {
            get
            {
                return _BillingDocTypeCode;
            }
            set
            {
                _BillingDocTypeCode = value;
            }
        }
        public string BillingTypeDesc
        {
            get
            {
                return _BillingTypeDesc;
            }
            set
            {
                _BillingTypeDesc = value;
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
        public int BasedOn
        {
            get
            {
                return _BasedOn;
            }
            set
            {
                _BasedOn = value;
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
            this.BillingDocTypeCode = Convert.ToString(dr["BillingDocTypeCode"]);
            this.BillingTypeDesc = Convert.ToString(dr["BillingTypeDesc"]);
            this.CRLimitCheckType = Convert.ToString(dr["CRLimitCheckType"]);
            this.ItemNoIncr = Convert.ToInt32(dr["ItemNoIncr"]);
            this.NumRange = Convert.ToString(dr["NumRange"]);
            this.RangeFrom = Convert.ToString(dr["RangeFrom"]);
            this.RangeTo = Convert.ToString(dr["RangeTo"]);
            this.BasedOn = Convert.ToInt32(dr["BasedOn"]);
            this.SaveMode = Convert.ToInt32(dr["SaveMode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}