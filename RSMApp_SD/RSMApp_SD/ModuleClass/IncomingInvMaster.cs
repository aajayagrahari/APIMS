
//Created On :: 26, October, 2012
//Private const string ClassName = "IncomingInvMaster"
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
    public class IncomingInvMaster
    {
        private string _IncomingInvDocCode;
        private string _IncomingInvDocTypeCode;
        private string _InvoiceDate;
        private string _VendorCode;
        private int _TotalQty;
        private int _GrossAmt;
        private int _NetAmt;
        private string _SAPTranID;
        private int _IsSAPPosted;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string IncomingInvDocCode
        {
            get
            {
                return _IncomingInvDocCode;
            }
            set
            {
                _IncomingInvDocCode = value;
            }
        }
        public string IncomingInvDocTypeCode
        {
            get
            {
                return _IncomingInvDocTypeCode;
            }
            set
            {
                _IncomingInvDocTypeCode = value;
            }
        }
        public string InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
            }
        }
        public string VendorCode
        {
            get
            {
                return _VendorCode;
            }
            set
            {
                _VendorCode = value;
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
        public int NetAmt
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

        public int GrossAmt
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


        public void SetObjectInfo(DataRow dr)
        {
            this.IncomingInvDocCode = Convert.ToString(dr["IncomingInvDocCode"]);
            this.IncomingInvDocTypeCode = Convert.ToString(dr["IncomingInvDocTypeCode"]);
            this.InvoiceDate = Convert.ToString(dr["InvoiceDate"]);
            this.VendorCode = Convert.ToString(dr["VendorCode"]);
            this.TotalQty = Convert.ToInt32(dr["TotalQty"]);
            this.NetAmt = Convert.ToInt32(dr["NetAmt"]);
            this.GrossAmt = Convert.ToInt32(dr["GrossAmt"]);
            this.SAPTranID = Convert.ToString(dr["SAPTranID"]);
            this.IsSAPPosted = Convert.ToInt32(dr["IsSAPPosted"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}