
//Created On :: 08, October, 2012
//Private const string ClassName = "Vendor_AccViewWHTax"
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
    public class Vendor_AccViewWHTax
    {
        private string _VendorCode;
        private string _PurchaseOrgCode;
        private string _CompanyCode;
        private string _WHTaxCountryCode;
        private string _WHTaxType;
        private string _WHTaxCode;
        private string _WHTaxID;
        private string _TypeOfRecCode;
        private int _ExemptNo;
        private double _ExemptPercentage;
        private string _ExemptReason;
        private string _ExemptFrom;
        private string _ExemptTo;
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
        public string WHTaxCountryCode
        {
            get
            {
                return _WHTaxCountryCode;
            }
            set
            {
                _WHTaxCountryCode = value;
            }
        }
        public string WHTaxType
        {
            get
            {
                return _WHTaxType;
            }
            set
            {
                _WHTaxType = value;
            }
        }
        public string WHTaxCode
        {
            get
            {
                return _WHTaxCode;
            }
            set
            {
                _WHTaxCode = value;
            }
        }
        public string WHTaxID
        {
            get
            {
                return _WHTaxID;
            }
            set
            {
                _WHTaxID = value;
            }
        }
        public string TypeOfRecCode
        {
            get
            {
                return _TypeOfRecCode;
            }
            set
            {
                _TypeOfRecCode = value;
            }
        }
        public int ExemptNo
        {
            get
            {
                return _ExemptNo;
            }
            set
            {
                _ExemptNo = value;
            }
        }
        public double ExemptPercentage
        {
            get
            {
                return _ExemptPercentage;
            }
            set
            {
                _ExemptPercentage = value;
            }
        }
        public string ExemptReason
        {
            get
            {
                return _ExemptReason;
            }
            set
            {
                _ExemptReason = value;
            }
        }
        public string ExemptFrom
        {
            get
            {
                return _ExemptFrom;
            }
            set
            {
                _ExemptFrom = value;
            }
        }
        public string ExemptTo
        {
            get
            {
                return _ExemptTo;
            }
            set
            {
                _ExemptTo = value;
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
            this.WHTaxCountryCode = Convert.ToString(dr["WHTaxCountryCode"]);
            this.WHTaxType = Convert.ToString(dr["WHTaxType"]);
            this.WHTaxCode = Convert.ToString(dr["WHTaxCode"]);
            this.WHTaxID = Convert.ToString(dr["WHTaxID"]);
            this.TypeOfRecCode = Convert.ToString(dr["TypeOfRecCode"]);
            this.ExemptNo = Convert.ToInt32(dr["ExemptNo"]);
            this.ExemptPercentage = Convert.ToDouble(dr["ExemptPercentage"]);
            this.ExemptReason = Convert.ToString(dr["ExemptReason"]);
            this.ExemptFrom = Convert.ToString(dr["ExemptFrom"]);
            this.ExemptTo = Convert.ToString(dr["ExemptTo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}