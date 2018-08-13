
//Created On :: 07, May, 2012
//Private const string ClassName = "ControllingArea"
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
    public class ControllingArea
    {
        private string _COACode;
        private string _COAName;
        private string _ChartACCode;
        private string _FiscalYearType;
        private string _CurrencyCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string COACode
        {
            get
            {
                return _COACode;
            }
            set
            {
                _COACode = value;
            }
        }
        public string COAName
        {
            get
            {
                return _COAName;
            }
            set
            {
                _COAName = value;
            }
        }
        public string ChartACCode
        {
            get
            {
                return _ChartACCode;
            }
            set
            {
                _ChartACCode = value;
            }
        }
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
            this.COACode = Convert.ToString(dr["COACode"]);
            this.COAName = Convert.ToString(dr["COAName"]);
            this.ChartACCode = Convert.ToString(dr["ChartACCode"]);
            this.FiscalYearType = Convert.ToString(dr["FiscalYearType"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}