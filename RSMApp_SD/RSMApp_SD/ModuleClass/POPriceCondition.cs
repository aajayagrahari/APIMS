
//Created On :: 16, October, 2012
//Private const string ClassName = "POPriceCondition"
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
    public class POPriceCondition
    {
        private string _PODocCode;
        private string _ItemNo;
        private string _ConditionType;
        private string _CalculationType;
        private int _Amount;
        private string _Currency;
        private string _PerUnit;
        private string _UOMCode;
        private int _ConditionValue;
        private int _POLevel;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PODocCode
        {
            get
            {
                return _PODocCode;
            }
            set
            {
                _PODocCode = value;
            }
        }
        public string ItemNo
        {
            get
            {
                return _ItemNo;
            }
            set
            {
                _ItemNo = value;
            }
        }
        public string ConditionType
        {
            get
            {
                return _ConditionType;
            }
            set
            {
                _ConditionType = value;
            }
        }
        public string CalculationType
        {
            get
            {
                return _CalculationType;
            }
            set
            {
                _CalculationType = value;
            }
        }
        public int Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
            }
        }
        public string Currency
        {
            get
            {
                return _Currency;
            }
            set
            {
                _Currency = value;
            }
        }
        public string PerUnit
        {
            get
            {
                return _PerUnit;
            }
            set
            {
                _PerUnit = value;
            }
        }
        public string UOMCode
        {
            get
            {
                return _UOMCode;
            }
            set
            {
                _UOMCode = value;
            }
        }
        public int ConditionValue
        {
            get
            {
                return _ConditionValue;
            }
            set
            {
                _ConditionValue = value;
            }
        }
        public int POLevel
        {
            get
            {
                return _POLevel;
            }
            set
            {
                _POLevel = value;
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
            this.PODocCode = Convert.ToString(dr["PODocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.ConditionType = Convert.ToString(dr["ConditionType"]);
            this.CalculationType = Convert.ToString(dr["CalculationType"]);
            this.Amount = Convert.ToInt32(dr["Amount"]);
            this.Currency = Convert.ToString(dr["Currency"]);
            this.PerUnit = Convert.ToString(dr["PerUnit"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.ConditionValue = Convert.ToInt32(dr["ConditionValue"]);
            this.POLevel = Convert.ToInt32(dr["POLevel"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}