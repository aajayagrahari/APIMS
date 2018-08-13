
//Created On :: 09, October, 2012
//Private const string ClassName = "PartnerMaster"
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
    public class PartnerMaster
    {
        private string _PartnerCode;
        private string _PartnerTypeCode;
        private string _ShortCode;
        private string _Title;
        private string _PartnerName;
        private string _Address1;
        private string _Address2;
        private string _CountryCode;
        private string _StateCode;
        private string _City;
        private string _PinCode;
        private string _TelNo;
        private string _FaxNo;
        private string _MobileNo;
        private string _EmailID;
        private string _ContactPerson;
        private string _PrintName;
        private string _CSTTIN;
        private string _VATTIN;
        private string _PANNo;
        private string _ServiceTaxNo;
        private string _DefaultPlantCode;
        private double _AnnualSales;
        private string _AnnualSalesCurrency;
        private int _TotalEmployees;
        private string _TransportZoneCode;
        private string _CustomerClassCode;
        private string _IndustryName;
        private int _CompanyOwned;
        private int _IsBlocked;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string PartnerTypeCode
        {
            get
            {
                return _PartnerTypeCode;
            }
            set
            {
                _PartnerTypeCode = value;
            }
        }
        public string ShortCode
        {
            get
            {
                return _ShortCode;
            }
            set
            {
                _ShortCode = value;
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
        public string PartnerName
        {
            get
            {
                return _PartnerName;
            }
            set
            {
                _PartnerName = value;
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
        public string PrintName
        {
            get
            {
                return _PrintName;
            }
            set
            {
                _PrintName = value;
            }
        }
        public string CSTTIN
        {
            get
            {
                return _CSTTIN;
            }
            set
            {
                _CSTTIN = value;
            }
        }
        public string VATTIN
        {
            get
            {
                return _VATTIN;
            }
            set
            {
                _VATTIN = value;
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
        public string ServiceTaxNo
        {
            get
            {
                return _ServiceTaxNo;
            }
            set
            {
                _ServiceTaxNo = value;
            }
        }

        public string DefaultPlantCode
        {
            get
            {
                return _DefaultPlantCode;
            }
            set
            {
                _DefaultPlantCode = value;
            }
        }


        public double AnnualSales
        {
            get
            {
                return _AnnualSales;
            }
            set
            {
                _AnnualSales = value;
            }
        }


        public string AnnualSalesCurrency
        {
            get
            {
                return _AnnualSalesCurrency;
            }
            set
            {
                _AnnualSalesCurrency = value;
            }
        }

        public int TotalEmployees
        {
            get
            {
                return _TotalEmployees;
            }
            set
            {
                _TotalEmployees = value;
            }
        }

        public string TransportZoneCode
        {
            get
            {
                return _TransportZoneCode;
            }
            set
            {
                _TransportZoneCode = value;
            }
        }

        public string CustomerClassCode
        {
            get
            {
                return _CustomerClassCode;
            }
            set
            {
                _CustomerClassCode = value;
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


        public int CompanyOwned
        {
            get
            {
                return _CompanyOwned;
            }
            set
            {
                _CompanyOwned = value;
            }
        }
        public int IsBlocked
        {
            get
            {
                return _IsBlocked;
            }
            set
            {
                _IsBlocked = value;
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
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.PartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"]);
            this.ShortCode = Convert.ToString(dr["ShortCode"]);
            this.Title = Convert.ToString(dr["Title"]);
            this.PartnerName = Convert.ToString(dr["PartnerName"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.City = Convert.ToString(dr["City"]);
            this.PinCode = Convert.ToString(dr["PinCode"]);
            this.TelNo = Convert.ToString(dr["TelNo"]);
            this.FaxNo = Convert.ToString(dr["FaxNo"]);
            this.MobileNo = Convert.ToString(dr["MobileNo"]);
            this.EmailID = Convert.ToString(dr["EmailID"]);
            this.ContactPerson = Convert.ToString(dr["ContactPerson"]);
            this.PrintName = Convert.ToString(dr["PrintName"]);
            this.CSTTIN = Convert.ToString(dr["CSTTIN"]);
            this.VATTIN = Convert.ToString(dr["VATTIN"]);
            this.PANNo = Convert.ToString(dr["PANNo"]);
            this.ServiceTaxNo = Convert.ToString(dr["ServiceTaxNo"]);
            this.DefaultPlantCode = Convert.ToString(dr["DefaultPlantCode"]);
            this.AnnualSales = Convert.ToDouble(dr["AnnualSales"]);
            this.AnnualSalesCurrency = Convert.ToString(dr["AnnualSalesCurrency"]);
            this.TotalEmployees = Convert.ToInt32(dr["TotalEmployees"]);
            this.CustomerClassCode = Convert.ToString(dr["CustomerClassCode"]);
            this.TransportZoneCode = Convert.ToString(dr["TransportZoneCode"]);
            this.IndustryName = Convert.ToString(dr["IndustryName"]);
            this.CompanyOwned = Convert.ToInt32(dr["CompanyOwned"]);
            this.IsBlocked = Convert.ToInt32(dr["IsBlocked"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}