
//Created On :: 18, May, 2012
//Private const string ClassName = "Vendor"
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
    public class Vendor
    {
        private string _VendorCode;
        private string _Name1;
        private string _Name2;
        private string _VendorAccTypeCode;
        private string _Address1;
        private string _Address2;
        private string _CountryCode;
        private string _StateCode;
        private string _City;
        private string _PinCode;
        private string _EMailID;
        private string _TelNo;
        private string _MobileNo;
        private string _FaxNo;
        private string _Title;
        private string _ContactPerson;
        private string _PANNo;
        private string _TINNo;
        private string _VATNo;
        private string _MappedAs;
        private string _MappedCode;
        private string _TypeOfBusiness;
        private string _TypeOfIndustry;
        private string _IndustryName;
        private string _CurrencyCode;
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
        public string VendorAccTypeCode
        {
            get
            {
                return _VendorAccTypeCode;
            }
            set
            {
                _VendorAccTypeCode = value;
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

        public string MobileNo
        {
            get
            {
                return _MobileNo;
            }
            set
            {
                _MobileNo = value;
            }
        }
        public string EMailID
        {
            get
            {
                return _EMailID;
            }
            set
            {
                _EMailID = value;
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
        public string PANNo
        {
            get
            {
                return _PANNo;
            }
            set
            {
                _PANNo = value;
            }
        }
        public string TINNo
        {
            get
            {
                return _TINNo;
            }
            set
            {
                _TINNo = value;
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

        public string MappedAs
        {
            get
            {
                return _MappedAs;
            }
            set
            {
                _MappedAs = value;
            }
        }
        public string MappedCode
        {
            get
            {
                return _MappedCode;
            }
            set
            {
                _MappedCode = value;
            }
        }

        public string IndustryName
        {
            get
            {
                return _IndustryName;
            }
            set
            {
                _IndustryName = value;
            }
        }

        public string TypeOfBusiness
        {
            get
            {
                return _TypeOfBusiness;
            }
            set
            {
                _TypeOfBusiness = value;
            }
        }
        public string TypeOfIndustry
        {
            get
            {
                return _TypeOfIndustry;
            }
            set
            {
                _TypeOfIndustry = value;
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
            this.VendorCode = Convert.ToString(dr["VendorCode"]);
            this.Name1 = Convert.ToString(dr["Name1"]);
            this.Name2 = Convert.ToString(dr["Name2"]);
            this.VendorAccTypeCode = Convert.ToString(dr["VendorAccTypeCode"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.City = Convert.ToString(dr["City"]);
            this.PinCode = Convert.ToString(dr["PinCode"]);
            this.TelNo = Convert.ToString(dr["TelNo"]);
            this.MobileNo = Convert.ToString(dr["MobileNo"]);
            this.EMailID = Convert.ToString(dr["EmailID"]);
            this.FaxNo = Convert.ToString(dr["FaxNo"]);
            this.Title = Convert.ToString(dr["Title"]);
            this.ContactPerson = Convert.ToString(dr["ContactPerson"]);
            this.PANNo = Convert.ToString(dr["PANNo"]);
            this.TINNo = Convert.ToString(dr["TINNo"]);
            this.VATNo = Convert.ToString(dr["VATNo"]);
            this.MappedAs = Convert.ToString(dr["MappedAs"]);
            this.MappedCode = Convert.ToString(dr["MappedCode"]);
            this.TypeOfBusiness = Convert.ToString(dr["TypeOfBusiness"]);
            this.TypeOfIndustry = Convert.ToString(dr["TypeOfIndustry"]);
            this.IndustryName = Convert.ToString(dr["IndustryName"]);
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