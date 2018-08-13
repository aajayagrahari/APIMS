
//Created On :: 06, June, 2012
//Private const string ClassName = "SAPProcess"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SAP
{
    [Serializable]
    public class SAPProcess
    {
        private string _ProcessCode;
        private string _ProcessName;
        private string _Activity;
        private string _BAPIName;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string ProcessCode
        {
            get
            {
                return _ProcessCode;
            }
            set
            {
                _ProcessCode = value;
            }
        }
        public string ProcessName
        {
            get
            {
                return _ProcessName;
            }
            set
            {
                _ProcessName = value;
            }
        }
        public string Activity
        {
            get
            {
                return _Activity;
            }
            set
            {
                _Activity = value;
            }
        }
        public string BAPIName
        {
            get
            {
                return _BAPIName;
            }
            set
            {
                _BAPIName = value;
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
            this.ProcessCode = Convert.ToString(dr["ProcessCode"]);
            this.ProcessName = Convert.ToString(dr["ProcessName"]);
            this.Activity = Convert.ToString(dr["Activity"]);
            this.BAPIName = Convert.ToString(dr["BAPIName"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}