
//Created On :: 16, January, 2013
//Private const string ClassName = "CallBillingDocMaster"
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
    public class CallBillingDocMaster
    {
        private string _CallBillingDocCode;
        private string _CallBillingDocTypeCode;
        private string _BillingDate;
        private string _CallClosingCode;
        private double _GrossValue;
        private double _DiscPer;
        private double _DiscValue;
        private double _NetValue;
        private double _PaidValue;
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
        public string CallBillingDocTypeCode
        {
            get
            {
                return _CallBillingDocTypeCode;
            }
            set
            {
                _CallBillingDocTypeCode = value;
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
        public double GrossValue
        {
            get
            {
                return _GrossValue;
            }
            set
            {
                _GrossValue = value;
            }
        }
        public double DiscPer
        {
            get
            {
                return _DiscPer;
            }
            set
            {
                _DiscPer = value;
            }
        }
        public double DiscValue
        {
            get
            {
                return _DiscValue;
            }
            set
            {
                _DiscValue = value;
            }
        }
        public double NetValue
        {
            get
            {
                return _NetValue;
            }
            set
            {
                _NetValue = value;
            }
        }
        public double PaidValue
        {
            get
            {
                return _PaidValue;
            }
            set
            {
                _PaidValue = value;
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
            this.CallBillingDocTypeCode = Convert.ToString(dr["CallBillingDocTypeCode"]);
            this.BillingDate = Convert.ToString(dr["BillingDate"]);
            this.CallClosingCode = Convert.ToString(dr["CallClosingCode"]);
            this.GrossValue = Convert.ToDouble(dr["GrossValue"]);
            this.DiscPer = Convert.ToDouble(dr["DiscPer"]);
            this.DiscValue = Convert.ToDouble(dr["DiscValue"]);
            this.NetValue = Convert.ToDouble(dr["NetValue"]);
            this.PaidValue = Convert.ToDouble(dr["PaidValue"]);
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