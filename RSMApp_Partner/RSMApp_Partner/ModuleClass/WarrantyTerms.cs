
//Created On :: 12, October, 2012
//Private const string ClassName = "WarrantyTerms"
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
    public class WarrantyTerms
    {
        private string _WarrantyTermsCode;
        private string _WarrantyTermsDesc;
        private string _PeriodType;
        private string _PeriodNo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        public string WarrantyTermsCode
        {
            get
            {
                return _WarrantyTermsCode;
            }
            set
            {
                _WarrantyTermsCode = value;
            }
        }

        public string WarrantyTermsDesc
        {
            get
            {
                return _WarrantyTermsDesc;
            }
            set
            {
                _WarrantyTermsDesc = value;
            }
        }

        public string PeriodType
        {
            get
            {
                return _PeriodType;
            }
            set
            {
                _PeriodType = value;
            }
        }

        public string PeriodNo
        {
            get
            {
                return _PeriodNo;
            }
            set
            {
                _PeriodNo = value;
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
            this.WarrantyTermsCode = Convert.ToString(dr["WarrantyTermsCode"]);
            this.WarrantyTermsDesc = Convert.ToString(dr["WarrantyTermsDesc"]);
            this.PeriodType = Convert.ToString(dr["PeriodType"]);
            this.PeriodNo = Convert.ToString(dr["PeriodNo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
        }
    }
}