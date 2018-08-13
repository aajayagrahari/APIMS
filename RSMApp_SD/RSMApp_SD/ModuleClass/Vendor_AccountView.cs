
//Created On :: 08, October, 2012
//Private const string ClassName = "Vendor_AccountView"
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
    public class Vendor_AccountView
    {
        private string _VendorCode;
        private string _PurchaseOrgCode;
        private string _CompanyCode;
        private string _ReconsilationAccount;
        private string _PaymentTermsCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string PurchaseOrgCode
        {
            get
            {
                return _PurchaseOrgCode;
            }
            set
            {
                _PurchaseOrgCode = value;
            }
        }
        public string CompanyCode
        {
            get
            {
                return _CompanyCode;
            }
            set
            {
                _CompanyCode = value;
            }
        }
        public string ReconsilationAccount
        {
            get
            {
                return _ReconsilationAccount;
            }
            set
            {
                _ReconsilationAccount = value;
            }
        }
        public string PaymentTermsCode
        {
            get
            {
                return _PaymentTermsCode;
            }
            set
            {
                _PaymentTermsCode = value;
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
            this.VendorCode = Convert.ToString(dr["VendorCode"]);
            this.PurchaseOrgCode = Convert.ToString(dr["PurchaseOrgCode"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.ReconsilationAccount = Convert.ToString(dr["ReconsilationAccount"]);
            this.PaymentTermsCode = Convert.ToString(dr["PaymentTermsCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}