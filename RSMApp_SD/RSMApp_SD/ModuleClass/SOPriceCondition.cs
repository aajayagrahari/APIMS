
//Created On :: 04, June, 2012
//Private const string ClassName = "SOPriceCondition"
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
    public class SOPriceCondition
    {
        private string _SODocCode;
        private string _ItemNo;
        private string _ConditionType;
        private string _CalculationType;
        private double _Amount;
        private string _Currency;
        private string _PerUnit;
        private string _UOMCode;
        private double _ConditionValue;
        private int _SOLevel;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string SODocCode
        {
            get
            {
                return _SODocCode;
            }
            set
            {
                _SODocCode = value;
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
        public double Amount
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
        public double ConditionValue
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
        public int SOLevel
        {
            get
            {
                return _SOLevel;
            }
            set
            {
                _SOLevel = value;
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
            this.SODocCode = Convert.ToString(dr["SODocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.ConditionType = Convert.ToString(dr["ConditionType"]);
            this.CalculationType = Convert.ToString(dr["CalculationType"]);
            this.Amount = Convert.ToDouble(dr["Amount"]);
            this.Currency = Convert.ToString(dr["Currency"]);
            this.PerUnit = Convert.ToString(dr["PerUnit"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.ConditionValue = Convert.ToDouble(dr["ConditionValue"]);
            this.SOLevel = Convert.ToInt32(dr["SOLevel"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}