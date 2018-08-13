
//Created On :: 05, May, 2012
//Private const string ClassName = "FiscalYearVariant"
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
    public class FiscalYearVariant
    {
        private string _FiscalYearType;
        private string _FYDesc;
        private string _FYearStart;
        private string _FYearEnd;
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
        public string FYDesc
        {
            get
            {
                return _FYDesc;
            }
            set
            {
                _FYDesc = value;
            }
        }
        public string FYearStart
        {
            get
            {
                return _FYearStart;
            }
            set
            {
                _FYearStart = value;
            }
        }
        public string FYearEnd
        {
            get
            {
                return _FYearEnd;
            }
            set
            {
                _FYearEnd = value;
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
            this.FYDesc = Convert.ToString(dr["FYDesc"]);
            this.FYearStart = Convert.ToString(dr["FYearStart"]);
            this.FYearEnd = Convert.ToString(dr["FYearEnd"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}