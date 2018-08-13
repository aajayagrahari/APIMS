
//Created On :: 17, September, 2012
//Private const string ClassName = "BillingMaster"
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
    public class BillingMaster
    {
        private string _BillingDocCode;
        private string _BillingDocTypeCode;
        private string _BillingDate;
        private string _CustomerCode;
        private string _BillToParty;
        private int _TotalQty;
        private double _GrossAmt;
        private double _NetAmt;
        private string _SAPTranID;
        private int _IsSAPPosted;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _CustName1;
        private string _BPName;




        public string BillingDocCode
        {
            get
            {
                return _BillingDocCode;
            }
            set
            {
                _BillingDocCode = value;
            }
        }
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
        public string CustomerCode
        {
            get
            {
                return _CustomerCode;
            }
            set
            {
                _CustomerCode = value;
            }
        }
        public string BillToParty
        {
            get
            {
                return _BillToParty;
            }
            set
            {
                _BillToParty = value;
            }
        }
        public int TotalQty
        {
            get
            {
                return _TotalQty;
            }
            set
            {
                _TotalQty = value;
            }
        }
        public double GrossAmt
        {
            get
            {
                return _GrossAmt;
            }
            set
            {
                _GrossAmt = value;
            }
        }
        public double NetAmt
        {
            get
            {
                return _NetAmt;
            }
            set
            {
                _NetAmt = value;
            }
        }
        public string SAPTranID
        {
            get
            {
                return _SAPTranID;
            }
            set
            {
                _SAPTranID = value;
            }
        }
        public int IsSAPPosted
        {
            get
            {
                return _IsSAPPosted;
            }
            set
            {
                _IsSAPPosted = value;
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

        public string CustName1
        {
            get
            {
                return _CustName1;
            }
            set
            {
                _CustName1 = value;
            }
        }

        public string BPName
        {
            get
            {
                return _BPName;
            }
            set
            {
                _BPName = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.BillingDocCode = Convert.ToString(dr["BillingDocCode"]);
            this.BillingDocTypeCode = Convert.ToString(dr["BillingDocTypeCode"]);
            this.BillingDate = Convert.ToString(dr["BillingDate"]);
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);
            this.BillToParty = Convert.ToString(dr["BillToParty"]);
            this.TotalQty = Convert.ToInt32(dr["TotalQty"]);
            this.GrossAmt = Convert.ToDouble(dr["GrossAmt"]);
            this.NetAmt = Convert.ToDouble(dr["NetAmt"]);
            this.SAPTranID = Convert.ToString(dr["SAPTranID"]);
            this.IsSAPPosted = Convert.ToInt32(dr["IsSAPPosted"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
            this.CustName1 = Convert.ToString(dr["Name1"]);
            this.BPName = Convert.ToString(dr["BPName"]);

        }
    }
}