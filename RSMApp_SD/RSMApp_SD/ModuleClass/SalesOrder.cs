
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
        private string _SoldToParty;
        private string _BillToParty;
        private string _ShipToParty;
        private string _PayToParty;
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
        private string _ReasonCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        private int _IsSAPPosted;
        private string _SAPTranID;

        private string _CustName1;
        private string _SPName1;
        private string _BPName1;
        private string _SHName1;
        private string _PPName1;
        private string _CurrencyCode;


        private string _SalesOrgName;
        private string _DistChannel;
        private string _DivisionName;

        private string _SalesRegionName;
        private string _SalesDistrictName;
        private string _SalesGroupName;
        private string _SalesOfficeName;



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

        public string SoldToParty
        {
            get
            {
                return _SoldToParty;
            }
            set
            {
                _SoldToParty = value;
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

        public string ShipToParty
        {
            get
            {
                return _ShipToParty;
            }
            set
            {
                _ShipToParty = value;
            }
        }

        public string PayToParty
        {
            get
            {
                return _PayToParty;
            }
            set
            {
                _PayToParty = value;
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

        public string ReasonCode
        {
            get
            {
                return _ReasonCode;
            }
            set
            {
                _ReasonCode = value;
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

        public string SPName1
        {
            get
            {
                return _SPName1;
            }
            set
            {
                _SPName1 = value;
            }
        }

        public string BPName1
        {
            get
            {
                return _BPName1;
            }
            set
            {
                _BPName1 = value;
            }
        }

        public string SHName1
        {
            get
            {
                return _SHName1;
            }
            set
            {
                _SHName1 = value;
            }
        }

        public string PPName1
        {
            get
            {
                return _PPName1;
            }
            set
            {
                _PPName1 = value;
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

        public string SalesOrgName
        {
            get
            {
                return _SalesOrgName;
            }
            set
            {
                _SalesOrgName = value;
            }
        }

        public string DistChannel
        {
            get
            {
                return _DistChannel;
            }
            set
            {
                _DistChannel = value;
            }
        }

        public string DivisionName
        {
            get
            {
                return _DivisionName;
            }
            set
            {
                _DivisionName = value;
            }
        }

        public string SalesRegionName
        {
            get
            {
                return _SalesRegionName;
            }
            set
            {
                _SalesRegionName = value;
            }
        }

        public string SalesDistrictName
        {
            get
            {
                return _SalesDistrictName;
            }
            set
            {
                _SalesDistrictName = value;
            }
        }

        public string SalesGroupName
        {
            get
            {
                return _SalesGroupName;
            }
            set
            {
                _SalesGroupName = value;
            }

        }

        public string SalesOfficeName
        {
            get
            {
                return _SalesOfficeName;
            }
            set
            {
                _SalesOfficeName = value;
            }
        
        }

       

        public void SetObjectInfo(DataRow dr)
        {
            this.SODocCode = Convert.ToString(dr["SODocCode"]);
            this.SODocDate = Convert.ToString(dr["SODocDate"]);
            this.SOTypeCode = Convert.ToString(dr["SOTypeCode"]);
            this.SalesofficeCode = Convert.ToString(dr["SalesofficeCode"]);
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);

            this.SoldToParty = Convert.ToString(dr["SoldToParty"]);
            this.BillToParty = Convert.ToString(dr["BillToParty"]);
            this.ShipToParty = Convert.ToString(dr["ShipToParty"]);
            this.PayToParty = Convert.ToString(dr["PayToParty"]);
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
            this.ReasonCode = Convert.ToString(dr["ReasonCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
            this.IsSAPPosted = Convert.ToInt32(dr["IsSAPPosted"]);
            this.SAPTranID = Convert.ToString(dr["SAPTranID"]);

            this.CustName1 = Convert.ToString(dr["Name1"]);
           // this.SPName1 = Convert.ToString(dr["SPName1"]);
           // this.BPName1 = Convert.ToString(dr["BPName1"]);
           // this.SHName1 = Convert.ToString(dr["SHName1"]);
           // this.PPName1 = Convert.ToString(dr["PPName1"]);

            this.SalesOrgName = Convert.ToString(dr["SalesOrgName"]);
            this.DistChannel = Convert.ToString(dr["DistChannel"]);
            this.DivisionName = Convert.ToString(dr["DivisionName"]);
            this.SalesOfficeName = Convert.ToString(dr["SalesOfficeName"]);
            this.SalesGroupName = Convert.ToString(dr["SalesGroupName"]);

            this.SalesRegionName = Convert.ToString(dr["SalesRegionName"]);
            this.SalesDistrictName = Convert.ToString(dr["SalesDistrictName"]);

            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);

        }
    }
}