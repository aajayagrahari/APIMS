
//Created On :: 15, October, 2012
//Private const string ClassName = "FiscalYearMaster"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    [Serializable]
    public class FiscalYearMaster
    {
        private string _FiscalYearType;
        private string _FiscalYear;
        private string _FYStartDate;
        private string _FYEndDate;
        private string _CompanyCode;
        private int _IsOpen;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string FiscalYearType
        {
            get
            {
                return _FiscalYearType;
            }
            set
            {
                _FiscalYearType = value;
            }
        }
        public string FiscalYear
        {
            get
            {
                return _FiscalYear;
            }
            set
            {
                _FiscalYear = value;
            }
        }
        public string FYStartDate
        {
            get
            {
                return _FYStartDate;
            }
            set
            {
                _FYStartDate = value;
            }
        }
        public string FYEndDate
        {
            get
            {
                return _FYEndDate;
            }
            set
            {
                _FYEndDate = value;
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
        public int IsOpen
        {
            get
            {
                return _IsOpen;
            }
            set
            {
                _IsOpen = value;
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
            this.FiscalYearType = Convert.ToString(dr["FiscalYearType"]);
            this.FiscalYear = Convert.ToString(dr["FiscalYear"]);
            this.FYStartDate = Convert.ToString(dr["FYStartDate"]);
            this.FYEndDate = Convert.ToString(dr["FYEndDate"]);
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.IsOpen = Convert.ToInt32(dr["IsOpen"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}