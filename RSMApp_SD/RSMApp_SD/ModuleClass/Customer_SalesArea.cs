
//Created On :: 18, May, 2012
//Private const string ClassName = "Customer_SalesArea"
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
    public class Customer_SalesArea
    {
        private string _CustomerCode;
        private string _SalesOrganizationCode;
        private string _DivisionCode;
        private string _DistChannelCode;
        private string _SalesDistrictCode;
        private string _SalesRegionCode;
        private string _SalesOfficeCode;
        private string _SalesGroupCode;
        private string _CustGroupCode;
        private string _CurrencyCode;
        private string _DlvPriorityCode;
        private string _DefaultPlantCode;
        private string _IncoTermsCode;
        private string _PaymentTermsCode;
        private string _CrContAreaCode;
        private string _AccAssignGroupCode;
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
        public string DivisionCode
        {
            get
            {
                return _DivisionCode;
            }
            set
            {
                _DivisionCode = value;
            }
        }
        public string DistChannelCode
        {
            get
            {
                return _DistChannelCode;
            }
            set
            {
                _DistChannelCode = value;
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
        public string CustGroupCode
        {
            get
            {
                return _CustGroupCode;
            }
            set
            {
                _CustGroupCode = value;
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
        public string DlvPriorityCode
        {
            get
            {
                return _DlvPriorityCode;
            }
            set
            {
                _DlvPriorityCode = value;
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
        public string IncoTermsCode
        {
            get
            {
                return _IncoTermsCode;
            }
            set
            {
                _IncoTermsCode = value;
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
        public string AccAssignGroupCode
        {
            get
            {
                return _AccAssignGroupCode;
            }
            set
            {
                _AccAssignGroupCode = value;
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
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.DivisionCode = Convert.ToString(dr["DivisionCode"]);
            this.DistChannelCode = Convert.ToString(dr["DistChannelCode"]);
            this.SalesDistrictCode = Convert.ToString(dr["SalesDistrictCode"]);
            this.SalesRegionCode = Convert.ToString(dr["SalesRegionCode"]);
            this.SalesOfficeCode = Convert.ToString(dr["SalesOfficeCode"]);
            this.SalesGroupCode = Convert.ToString(dr["SalesGroupCode"]);
            this.CustGroupCode = Convert.ToString(dr["CustGroupCode"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.DlvPriorityCode = Convert.ToString(dr["DlvPriorityCode"]);
            this.DefaultPlantCode = Convert.ToString(dr["DefaultPlantCode"]);
            this.IncoTermsCode = Convert.ToString(dr["IncoTermsCode"]);
            this.PaymentTermsCode = Convert.ToString(dr["PaymentTermsCode"]);
            this.CrContAreaCode = Convert.ToString(dr["CrContAreaCode"]);
            this.AccAssignGroupCode = Convert.ToString(dr["AccAssignGroupCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}