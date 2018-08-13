
//Created On :: 01, November, 2012
//Private const string ClassName = "AsgTechnicianCallMaster"
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
    public class AsgTechnicianCallMaster
    {
        private string _AsgTechCallCode;
        private string _AsgTechDocTypeCode;
        private string _PartnerEmployeeCode;
        private string _AssignDate;
        private string _PartnerCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _AssignType;

        public string AsgTechCallCode
        {
            get
            {
                return _AsgTechCallCode;
            }
            set
            {
                _AsgTechCallCode = value;
            }
        }
        public string AsgTechDocTypeCode
        {
            get
            {
                return _AsgTechDocTypeCode;
            }
            set
            {
                _AsgTechDocTypeCode = value;
            }
        }
        public string PartnerEmployeeCode
        {
            get
            {
                return _PartnerEmployeeCode;
            }
            set
            {
                _PartnerEmployeeCode = value;
            }
        }
        public string AssignDate
        {
            get
            {
                return _AssignDate;
            }
            set
            {
                _AssignDate = value;
            }
        }
        public string PartnerCode
        {
            get
            {
                return _PartnerCode;
            }
            set
            {
                _PartnerCode = value;
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

        public string AssignType
        {
            get
            {
                return _AssignType;
            }
            set
            {
                _AssignType = value;
            }
        }
        
        public void SetObjectInfo(DataRow dr)
        {
            this.AsgTechCallCode = Convert.ToString(dr["AsgTechCallCode"]);
            this.AsgTechDocTypeCode = Convert.ToString(dr["AsgTechDocTypeCode"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.AssignDate = Convert.ToString(dr["AssignDate"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
        }
    }
}