
//Created On :: 18, May, 2012
//Private const string ClassName = "CreditControl_Customer"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Organization
{
    [Serializable]
    public class CreditControl_Customer
    {
        private string _CrContAreaCode;
        private string _CustomerCode;
        private string _SalesOrganizationCode;
        private string _CurrencyCode;
        private string _CRUpdateCode;
        private string _FiscalYearType;
        private string _RiskCategoryCode;
        private int _CreditLimit;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string CrContAreaCode
        {
            get
            {
                return _CrContAreaCode;
            }
            set
            {
                _CrContAreaCode = value;
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
        public string SalesOrganizationCode
        {
            get
            {
                return _SalesOrganizationCode;
            }
            set
            {
                _SalesOrganizationCode = value;
            }
        }
        public string CurrencyCode
        {
            get
            {
                return _CurrencyCode;
            }
            set
            {
                _CurrencyCode = value;
            }
        }
        public string CRUpdateCode
        {
            get
            {
                return _CRUpdateCode;
            }
            set
            {
                _CRUpdateCode = value;
            }
        }
        public string FiscalYearType
        {
            get
            {
                return _FiscalYearType;
            }
            set
            {
                _FiscalYearType = value;
            }
        }
        public string RiskCategoryCode
        {
            get
            {
                return _RiskCategoryCode;
            }
            set
            {
                _RiskCategoryCode = value;
            }
        }
        public int CreditLimit
        {
            get
            {
                return _CreditLimit;
            }
            set
            {
                _CreditLimit = value;
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
            this.CrContAreaCode = Convert.ToString(dr["CrContAreaCode"]);
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.CRUpdateCode = Convert.ToString(dr["CRUpdateCode"]);
            this.FiscalYearType = Convert.ToString(dr["FiscalYearType"]);
            this.RiskCategoryCode = Convert.ToString(dr["RiskCategoryCode"]);
            this.CreditLimit = Convert.ToInt32(dr["CreditLimit"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}