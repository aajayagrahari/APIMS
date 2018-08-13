
//Created On :: 07, May, 2012
//Private const string ClassName = "Company"
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
    public class Company
    {
        private string _ClientCode;
        private string _CompanyCode;
        private string _CompanyName;
        private string _Address1;
        private string _Address2;
        private string _CountryCode;
        private string _CurrencyCode;
        private string _LanguageCode;
        private string _ChartACCode;
        private string _CrContAreaCode;
        private string _FiscalYearType;
        private string _PostingPeriodType;
        private string _VatRegistrationNo;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }
        public string Address1
        {
            get
            {
                return _Address1;
            }
            set
            {
                _Address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return _Address2;
            }
            set
            {
                _Address2 = value;
            }
        }
        public string CountryCode
        {
            get
            {
                return _CountryCode;
            }
            set
            {
                _CountryCode = value;
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
        public string LanguageCode
        {
            get
            {
                return _LanguageCode;
            }
            set
            {
                _LanguageCode = value;
            }
        }
        public string ChartACCode
        {
            get
            {
                return _ChartACCode;
            }
            set
            {
                _ChartACCode = value;
            }
        }
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
        public string PostingPeriodType
        {
            get
            {
                return _PostingPeriodType;
            }
            set
            {
                _PostingPeriodType = value;
            }
        }
        public string VatRegistrationNo
        {
            get
            {
                return _VatRegistrationNo;
            }
            set
            {
                _VatRegistrationNo = value;
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
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.CompanyName = Convert.ToString(dr["CompanyName"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.LanguageCode = Convert.ToString(dr["LanguageCode"]);
            this.ChartACCode = Convert.ToString(dr["ChartACCode"]);
            this.CrContAreaCode = Convert.ToString(dr["CrContAreaCode"]);
            this.FiscalYearType = Convert.ToString(dr["FiscalYearType"]);
            this.PostingPeriodType = Convert.ToString(dr["PostingPeriodType"]);
            this.VatRegistrationNo = Convert.ToString(dr["VatRegistrationNo"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}