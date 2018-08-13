
//Created On :: 26, September, 2012
//Private const string ClassName = "PricingConditionType"
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
    public class PricingConditionType
    {
        private string _ConditionTypeCode;
        private string _ConditionTypeDesc;
        private string _ConditionClass;
        private string _CalculationTypeCode;
        private int _ManualEntryAllowed;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string ConditionTypeCode
        {
            get
            {
                return _ConditionTypeCode;
            }
            set
            {
                _ConditionTypeCode = value;
            }
        }
        public string ConditionTypeDesc
        {
            get
            {
                return _ConditionTypeDesc;
            }
            set
            {
                _ConditionTypeDesc = value;
            }
        }
        public string ConditionClass
        {
            get
            {
                return _ConditionClass;
            }
            set
            {
                _ConditionClass = value;
            }
        }
        public string CalculationTypeCode
        {
            get
            {
                return _CalculationTypeCode;
            }
            set
            {
                _CalculationTypeCode = value;
            }
        }
        public int ManualEntryAllowed
        {
            get
            {
                return _ManualEntryAllowed;
            }
            set
            {
                _ManualEntryAllowed = value;
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
            this.ConditionTypeCode = Convert.ToString(dr["ConditionTypeCode"]);
            this.ConditionTypeDesc = Convert.ToString(dr["ConditionTypeDesc"]);
            this.ConditionClass = Convert.ToString(dr["ConditionClass"]);
            this.CalculationTypeCode = Convert.ToString(dr["CalculationTypeCode"]);
            this.ManualEntryAllowed = Convert.ToInt32(dr["ManualEntryAllowed"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}