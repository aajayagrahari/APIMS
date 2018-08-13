
//Created On :: 03, October, 2012
//Private const string ClassName = "TaxCatCustomer"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_FI
{
    [Serializable]
    public class TaxCatCustomer
    {
        private string _TaxCategoryCode;
        private string _TaxCategoryDesc;
        private string _TaxClassCode;
        private string _TaxClassDesc;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string TaxCategoryCode
        {
            get
            {
                return _TaxCategoryCode;
            }
            set
            {
                _TaxCategoryCode = value;
            }
        }
        public string TaxCategoryDesc
        {
            get
            {
                return _TaxCategoryDesc;
            }
            set
            {
                _TaxCategoryDesc = value;
            }
        }
        public string TaxClassCode
        {
            get
            {
                return _TaxClassCode;
            }
            set
            {
                _TaxClassCode = value;
            }
        }
        public string TaxClassDesc
        {
            get
            {
                return _TaxClassDesc;
            }
            set
            {
                _TaxClassDesc = value;
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
            this.TaxCategoryCode = Convert.ToString(dr["TaxCategoryCode"]);
            this.TaxCategoryDesc = Convert.ToString(dr["TaxCategoryDesc"]);
            this.TaxClassCode = Convert.ToString(dr["TaxClassCode"]);
            this.TaxClassDesc = Convert.ToString(dr["TaxClassDesc"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}