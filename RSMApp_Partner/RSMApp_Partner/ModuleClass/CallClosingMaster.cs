
//Created On :: 26, December, 2012
//Private const string ClassName = "CallClosingMaster"
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
    public class CallClosingMaster
    {
        private string _CallClosingCode;
        private string _CallClosingDocTypeCode;
        private string _CallCode;
        private string _CallClosingDate;
        private double _NetPayable;
        private double _PaidAmt;
        private string _PartnerCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string CallClosingDocTypeCode
        {
            get
            {
                return _CallClosingDocTypeCode;
            }
            set
            {
                _CallClosingDocTypeCode = value;
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
        public string CallClosingDate
        {
            get
            {
                return _CallClosingDate;
            }
            set
            {
                _CallClosingDate = value;
            }
        }
        public double NetPayable
        {
            get
            {
                return _NetPayable;
            }
            set
            {
                _NetPayable = value;
            }
        }
        public double PaidAmt
        {
            get
            {
                return _PaidAmt;
            }
            set
            {
                _PaidAmt = value;
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
            this.CallClosingCode = Convert.ToString(dr["CallClosingCode"]);
            this.CallClosingDocTypeCode = Convert.ToString(dr["CallClosingDocTypeCode"]);
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.CallClosingDate = Convert.ToString(dr["CallClosingDate"]);
            this.NetPayable = Convert.ToDouble(dr["NetPayable"]);
            this.PaidAmt = Convert.ToDouble(dr["PaidAmt"]);
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