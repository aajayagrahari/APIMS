
//Created On :: 16, October, 2012
//Private const string ClassName = "WarrantyMaster"
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
    public class WarrantyMaster
    {
        private string _WarrantyCode;
        private string _WarrantyName;
        private string _MatGroup1Code;
        private string _VendorWarTermsCode;
        private string _CustWarTermsCode;
        private string _ValidFrom;
        private string _ValidTo;
        private int _IsActive;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string WarrantyCode
        {
            get
            {
                return _WarrantyCode;
            }
            set
            {
                _WarrantyCode = value;
            }
        }
        public string WarrantyName
        {
            get
            {
                return _WarrantyName;
            }
            set
            {
                _WarrantyName = value;
            }
        }
        public string MatGroup1Code
        {
            get
            {
                return _MatGroup1Code;
            }
            set
            {
                _MatGroup1Code = value;
            }
        }
   
        public string VendorWarTermsCode
        {
            get
            {
                return _VendorWarTermsCode;
            }
            set
            {
                _VendorWarTermsCode = value;
            }
        }
        public string CustWarTermsCode
        {
            get
            {
                return _CustWarTermsCode;
            }
            set
            {
                _CustWarTermsCode = value;
            }
        }
        public string ValidFrom
        {
            get
            {
                return _ValidFrom;
            }
            set
            {
                _ValidFrom = value;
            }
        }
        public string ValidTo
        {
            get
            {
                return _ValidTo;
            }
            set
            {
                _ValidTo = value;
            }
        }
        public int IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
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
            this.WarrantyCode = Convert.ToString(dr["WarrantyCode"]);
            this.WarrantyName = Convert.ToString(dr["WarrantyName"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.VendorWarTermsCode = Convert.ToString(dr["VendorWarTermsCode"]);
            this.CustWarTermsCode = Convert.ToString(dr["CustWarTermsCode"]);
            this.ValidFrom = Convert.ToString(dr["ValidFrom"]);
            this.ValidTo = Convert.ToString(dr["ValidTo"]);
            this.IsActive = Convert.ToInt32(dr["IsActive"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}