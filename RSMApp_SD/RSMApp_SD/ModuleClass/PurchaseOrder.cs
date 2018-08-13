
//Created On :: 15, October, 2012
//Private const string ClassName = "PurchaseOrder"
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
    public class PurchaseOrder
    {
        private string _PODocCode;
        private string _POTypeCode;
        private string _PurchaseOrgCode;
        private string _CompanyCode;
        private string _SourcePlantCode;
        private string _VendorCode;
        private string _ReqDeliveryDate;
        private string _PriceDate;
        private string _PODocumentDate;
        private string _PostingDate;
        private string _POStatus;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PODocCode
        {
            get
            {
                return _PODocCode;
            }
            set
            {
                _PODocCode = value;
            }
        }
        public string POTypeCode
        {
            get
            {
                return _POTypeCode;
            }
            set
            {
                _POTypeCode = value;
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
        public string SourcePlantCode
        {
            get
            {
                return _SourcePlantCode;
            }
            set
            {
                _SourcePlantCode = value;
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
        public string PODocumentDate
        {
            get
            {
                return _PODocumentDate;
            }
            set
            {
                _PODocumentDate = value;
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
        public string POStatus
        {
            get
            {
                return _POStatus;
            }
            set
            {
                _POStatus = value;
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
            this.PODocCode = Convert.ToString(dr["PODocCode"]);
            this.POTypeCode = Convert.ToString(dr["POTypeCode"]);
            this.PurchaseOrgCode = Convert.ToString(dr["PurchaseOrgCode"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.SourcePlantCode = Convert.ToString(dr["SourcePlantCode"]);
            this.VendorCode = Convert.ToString(dr["VendorCode"]);
            this.ReqDeliveryDate = Convert.ToString(dr["ReqDeliveryDate"]);
            this.PriceDate = Convert.ToString(dr["PriceDate"]);
            this.PODocumentDate = Convert.ToString(dr["PODocumentDate"]);
            this.PostingDate = Convert.ToString(dr["PostingDate"]);
            this.POStatus = Convert.ToString(dr["POStatus"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}