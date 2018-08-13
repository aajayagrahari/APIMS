
//Created On :: 15, January, 2013
//Private const string ClassName = "CallBillingDoc"
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
    public class CallBillingDoc
    {
        private string _CallBillingDocCode;
        private string _CallCode;
        private int _CallItemNo;
        private string _CallClosingCode;
        private int _BillingItemNo;
        private string _BillingDate;
        private string _ProcedureType;
        private int _LineItemNo;
        private string _PricingDescription;
        private double _BaseAmount;
        private string _CalCulationOn;
        private string _CalculationType;
        private double _CalculationValue;
        private double _DiscountPer;
        private double _DiscountAmt;
        private string _PartnerCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string CallBillingDocCode
        {
            get
            {
                return _CallBillingDocCode;
            }
            set
            {
                _CallBillingDocCode = value;
            }
        }

        public string CallCode
        {
            get
            {
                return _CallCode;
            }
            set
            {
                _CallCode = value;
            }
        }
        public int CallItemNo
        {
            get
            {
                return _CallItemNo;
            }
            set
            {
                _CallItemNo = value;
            }
        }
        public string CallClosingCode
        {
            get
            {
                return _CallClosingCode;
            }
            set
            {
                _CallClosingCode = value;
            }
        }
        public int BillingItemNo
        {
            get
            {
                return _BillingItemNo;
            }
            set
            {
                _BillingItemNo = value;
            }
        }
        public string BillingDate
        {
            get
            {
                return _BillingDate;
            }
            set
            {
                _BillingDate = value;
            }
        }
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
        public double BaseAmount
        {
            get
            {
                return _BaseAmount;
            }
            set
            {
                _BaseAmount = value;
            }
        }

        public string CalCulationOn
        {
            get
            {
                return _CalCulationOn;
            }
            set
            {
                _CalCulationOn = value;
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
        public double DiscountPer
        {
            get
            {
                return _DiscountPer;
            }
            set
            {
                _DiscountPer = value;
            }
        }
        public double DiscountAmt
        {
            get
            {
                return _DiscountAmt;
            }
            set
            {
                _DiscountAmt = value;
            }
        }
        public string PartnerCode
        {
            get
            {
                return _PartnerCode;
            }
            set
            {
                _PartnerCode = value;
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
            this.CallBillingDocCode = Convert.ToString(dr["CallBillingDocCode"]);
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.CallItemNo = Convert.ToInt32(dr["CallItemNo"]);
            this.CallClosingCode = Convert.ToString(dr["CallClosingCode"]);
            this.BillingItemNo = Convert.ToInt32(dr["BillingItemNo"]);
            this.BillingDate = Convert.ToString(dr["BillingDate"]);
            this.ProcedureType = Convert.ToString(dr["ProcedureType"]);
            this.LineItemNo = Convert.ToInt32(dr["LineItemNo"]);
            this.PricingDescription = Convert.ToString(dr["PricingDescription"]);
            this.BaseAmount = Convert.ToDouble(dr["BaseAmount"]);
            this.CalCulationOn = Convert.ToString(dr["CalCulationOn"]);
            this.CalculationType = Convert.ToString(dr["CalculationType"]);
            this.CalculationValue = Convert.ToDouble(dr["CalculationValue"]);
            this.DiscountPer = Convert.ToDouble(dr["DiscountPer"]);
            this.DiscountAmt = Convert.ToDouble(dr["DiscountAmt"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}