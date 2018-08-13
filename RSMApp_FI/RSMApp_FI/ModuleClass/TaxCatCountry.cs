
//Created On :: 03, October, 2012
//Private const string ClassName = "TaxCatCountry"
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
    public class TaxCatCountry
    {
        private string _TaxCountryCode;
        private int _Sequence;
        private string _TaxCategoryCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string TaxCountryCode
        {
            get
            {
                return _TaxCountryCode;
            }
            set
            {
                _TaxCountryCode = value;
            }
        }
        public int Sequence
        {
            get
            {
                return _Sequence;
            }
            set
            {
                _Sequence = value;
            }
        }
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
            this.TaxCountryCode = Convert.ToString(dr["TaxCountryCode"]);
            this.Sequence = Convert.ToInt32(dr["Sequence"]);
            this.TaxCategoryCode = Convert.ToString(dr["TaxCategoryCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}