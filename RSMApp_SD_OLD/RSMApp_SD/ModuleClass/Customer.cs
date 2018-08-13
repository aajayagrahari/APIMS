
//Created On :: 18, May, 2012
//Private const string ClassName = "Customer"
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
    public class Customer
    {
        private string _CustomerCode;
        private string _Name1;
        private string _Name2;
        private string _CustomerAccTypeCode;
        private string _Address1;
        private string _Address2;
        private string _CountryCode;
        private string _StateCode;
        private string _City;
        private string _PinCode;
        private string _TelNo;
        private string _FaxNo;
        private string _EmailID;
        private string _Title;
        private string _ContactPerson;
        private string _VATNo;
        private string _SalesDistrictCode;
        private string _SalesRegionCode;
        private string _SalesOfficeCode;
        private int _IsTaxExempted;
        private string _SalesGroupCode;
        private string _CurrencyCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string Name1
        {
            get
            {
                return _Name1;
            }
            set
            {
                _Name1 = value;
            }
        }
        public string Name2
        {
            get
            {
                return _Name2;
            }
            set
            {
                _Name2 = value;
            }
        }
        public string CustomerAccTypeCode
        {
            get
            {
                return _CustomerAccTypeCode;
            }
            set
            {
                _CustomerAccTypeCode = value;
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
        public string StateCode
        {
            get
            {
                return _StateCode;
            }
            set
            {
                _StateCode = value;
            }
        }
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
            }
        }
        public string PinCode
        {
            get
            {
                return _PinCode;
            }
            set
            {
                _PinCode = value;
            }
        }
        public string TelNo
        {
            get
            {
                return _TelNo;
            }
            set
            {
                _TelNo = value;
            }
        }
        public string FaxNo
        {
            get
            {
                return _FaxNo;
            }
            set
            {
                _FaxNo = value;
            }
        }
        public string EmailID
        {
            get
            {
                return _EmailID;
            }
            set
            {
                _EmailID = value;
            }
        }
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public string ContactPerson
        {
            get
            {
                return _ContactPerson;
            }
            set
            {
                _ContactPerson = value;
            }
        }
        public string VATNo
        {
            get
            {
                return _VATNo;
            }
            set
            {
                _VATNo = value;
            }
        }
        public string SalesDistrictCode
        {
            get
            {
                return _SalesDistrictCode;
            }
            set
            {
                _SalesDistrictCode = value;
            }
        }
        public string SalesRegionCode
        {
            get
            {
                return _SalesRegionCode;
            }
            set
            {
                _SalesRegionCode = value;
            }
        }
        public string SalesOfficeCode
        {
            get
            {
                return _SalesOfficeCode;
            }
            set
            {
                _SalesOfficeCode = value;
            }
        }
        public int IsTaxExempted
        {
            get
            {
                return _IsTaxExempted;
            }
            set
            {
                _IsTaxExempted = value;
            }
        }
        public string SalesGroupCode
        {
            get
            {
                return _SalesGroupCode;
            }
            set
            {
                _SalesGroupCode = value;
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
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);
            this.Name1 = Convert.ToString(dr["Name1"]);
            this.Name2 = Convert.ToString(dr["Name2"]);
            this.CustomerAccTypeCode = Convert.ToString(dr["CustomerAccTypeCode"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.City = Convert.ToString(dr["City"]);
            this.PinCode = Convert.ToString(dr["PinCode"]);
            this.TelNo = Convert.ToString(dr["TelNo"]);
            this.FaxNo = Convert.ToString(dr["FaxNo"]);
            this.EmailID = Convert.ToString(dr["EmailID"]);
            this.Title = Convert.ToString(dr["Title"]);
            this.ContactPerson = Convert.ToString(dr["ContactPerson"]);
            this.VATNo = Convert.ToString(dr["VATNo"]);
            this.SalesDistrictCode = Convert.ToString(dr["SalesDistrictCode"]);
            this.SalesRegionCode = Convert.ToString(dr["SalesRegionCode"]);
            this.SalesOfficeCode = Convert.ToString(dr["SalesOfficeCode"]);
            this.IsTaxExempted = Convert.ToInt32(dr["IsTaxExempted"]);
            this.SalesGroupCode = Convert.ToString(dr["SalesGroupCode"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}