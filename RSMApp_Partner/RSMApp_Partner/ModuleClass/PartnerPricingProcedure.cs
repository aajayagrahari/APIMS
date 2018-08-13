
//Created On :: 23, February, 2013
//Private const string ClassName = "PartnerPricingProcedure"
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
    public class PartnerPricingProcedure
    {
        private string _ProcedureType;
        private int _LineItemNo;
        private string _PricingDescription;
        private string _CalculationType;
        private double _CalculationValue;
        private string _CalculationOn;
        private int _CalcLineItemNo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string ProcedureType
        {
            get
            {
                return _ProcedureType;
            }
            set
            {
                _ProcedureType = value;
            }
        }
        public int LineItemNo
        {
            get
            {
                return _LineItemNo;
            }
            set
            {
                _LineItemNo = value;
            }
        }
        public string PricingDescription
        {
            get
            {
                return _PricingDescription;
            }
            set
            {
                _PricingDescription = value;
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
        public double CalculationValue
        {
            get
            {
                return _CalculationValue;
            }
            set
            {
                _CalculationValue = value;
            }
        }
        public string CalculationOn
        {
            get
            {
                return _CalculationOn;
            }
            set
            {
                _CalculationOn = value;
            }
        }
        public int CalcLineItemNo
        {
            get
            {
                return _CalcLineItemNo;
            }
            set
            {
                _CalcLineItemNo = value;
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
            this.ProcedureType = Convert.ToString(dr["ProcedureType"]);
            this.LineItemNo = Convert.ToInt32(dr["LineItemNo"]);
            this.PricingDescription = Convert.ToString(dr["PricingDescription"]);
            this.CalculationType = Convert.ToString(dr["CalculationType"]);
            this.CalculationValue = Convert.ToDouble(dr["CalculationValue"]);
            this.CalculationOn = Convert.ToString(dr["CalculationOn"]);
            this.CalcLineItemNo = Convert.ToInt32(dr["CalcLineItemNo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}