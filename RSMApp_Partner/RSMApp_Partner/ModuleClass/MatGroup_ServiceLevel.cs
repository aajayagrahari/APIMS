
//Created On :: 29, November, 2012
//Private const string ClassName = "MatGroup_ServiceLevel"
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
    public class MatGroup_ServiceLevel
    {
        private int _LabourChrgID;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _ServiceLevel;
        private double _UnderWarLbChrg;
        private double _OutWarLbChrg;
        private string _ValidFrom;
        private string _ValidTo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public int LabourChrgID
        {
            get
            {
                return _LabourChrgID;
            }
            set
            {
                _LabourChrgID = value;
            }
        }

        public string MaterialCode
        {
            get
            {
                return _MaterialCode;
            }
            set
            {
                _MaterialCode = value;
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
        public string ServiceLevel
        {
            get
            {
                return _ServiceLevel;
            }
            set
            {
                _ServiceLevel = value;
            }
        }
        public double UnderWarLbChrg
        {
            get
            {
                return _UnderWarLbChrg;
            }
            set
            {
                _UnderWarLbChrg = value;
            }
        }
        public double OutWarLbChrg
        {
            get
            {
                return _OutWarLbChrg;
            }
            set
            {
                _OutWarLbChrg = value;
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
            this.LabourChrgID = Convert.ToInt32(dr["LabourChrgID"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.ServiceLevel = Convert.ToString(dr["ServiceLevel"]);
            this.UnderWarLbChrg = Convert.ToDouble(dr["UnderWarLbChrg"]);
            this.OutWarLbChrg = Convert.ToDouble(dr["OutWarLbChrg"]);
            this.ValidFrom = Convert.ToString(dr["ValidFrom"]);
            this.ValidTo = Convert.ToString(dr["ValidTo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}