
//Created On :: 23, May, 2012
//Private const string ClassName = "SalesOrder"
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
    public class SalesOrder
    {
        private string _SODocCode;
        private string _SODocDate;
        private string _SOTypeCode;
        private string _SalesofficeCode;
        private string _CustomerCode;
        private string _SalesOrganizationCode;
        private string _DistChannelCode;
        private string _DivisionCode;
        private string _SalesGroupCode;
        private string _SalesRegionCode;
        private string _SalesDistrictCode;
        private string _RefPODocCode;
        private string _RefPODate;
        private string _PostingDate;
        private string _ReqDeliveryDate;
        private string _PriceDate;
        private int _DeliveryBlocked;
        private int _BillingBlocked;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string SODocCode
        {
            get
            {
                return _SODocCode;
            }
            set
            {
                _SODocCode = value;
            }
        }
        public string SODocDate
        {
            get
            {
                return _SODocDate;
            }
            set
            {
                _SODocDate = value;
            }
        }
        public string SOTypeCode
        {
            get
            {
                return _SOTypeCode;
            }
            set
            {
                _SOTypeCode = value;
            }
        }
        public string SalesofficeCode
        {
            get
            {
                return _SalesofficeCode;
            }
            set
            {
                _SalesofficeCode = value;
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
        public string RefPODocCode
        {
            get
            {
                return _RefPODocCode;
            }
            set
            {
                _RefPODocCode = value;
            }
        }
        public string RefPODate
        {
            get
            {
                return _RefPODate;
            }
            set
            {
                _RefPODate = value;
            }
        }
        public string PostingDate
        {
            get
            {
                return _PostingDate;
            }
            set
            {
                _PostingDate = value;
            }
        }
        public string ReqDeliveryDate
        {
            get
            {
                return _ReqDeliveryDate;
            }
            set
            {
                _ReqDeliveryDate = value;
            }
        }
        public string PriceDate
        {
            get
            {
                return _PriceDate;
            }
            set
            {
                _PriceDate = value;
            }
        }
        public int DeliveryBlocked
        {
            get
            {
                return _DeliveryBlocked;
            }
            set
            {
                _DeliveryBlocked = value;
            }
        }
        public int BillingBlocked
        {
            get
            {
                return _BillingBlocked;
            }
            set
            {
                _BillingBlocked = value;
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
            this.SODocCode = Convert.ToString(dr["SODocCode"]);
            this.SODocDate = Convert.ToString(dr["SODocDate"]);
            this.SOTypeCode = Convert.ToString(dr["SOTypeCode"]);
            this.SalesofficeCode = Convert.ToString(dr["SalesofficeCode"]);
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);
            this.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]);
            this.DistChannelCode = Convert.ToString(dr["DistChannelCode"]);
            this.DivisionCode = Convert.ToString(dr["DivisionCode"]);
            this.SalesGroupCode = Convert.ToString(dr["SalesGroupCode"]);
            this.SalesRegionCode = Convert.ToString(dr["SalesRegionCode"]);
            this.SalesDistrictCode = Convert.ToString(dr["SalesDistrictCode"]);
            this.RefPODocCode = Convert.ToString(dr["RefPODocCode"]);
            this.RefPODate = Convert.ToString(dr["RefPODate"]);
            this.PostingDate = Convert.ToString(dr["PostingDate"]);
            this.ReqDeliveryDate = Convert.ToString(dr["ReqDeliveryDate"]);
            this.PriceDate = Convert.ToString(dr["PriceDate"]);
            this.DeliveryBlocked = Convert.ToInt32(dr["DeliveryBlocked"]);
            this.BillingBlocked = Convert.ToInt32(dr["BillingBlocked"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}