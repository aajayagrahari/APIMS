
//Created On :: 11, October, 2012
//Private const string ClassName = "PartnerEmployeeMaster"
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
    public class PartnerEmployeeMaster
    {
        private string _PartnerEmployeeCode;
        private string _EmployeeName;
        private string _Address1;
        private string _Address2;
        private string _StateCode;
        private string _City;
        private string _PinCode;
        private string _TelNo;
        private string _EmailID;
        private string _MobileNo;
        private string _DepartmentCode;
        private string _DesignationCode;
        private string _PartnerCode;
        private string _CountryCode;

        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string EmployeeName
        {
            get
            {
                return _EmployeeName;
            }
            set
            {
                _EmployeeName = value;
            }
        }
        public string Address1
        {
            get
            {
                return _Address1;
            }
            set
            {
                _Address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return _Address2;
            }
            set
            {
                _Address2 = value;
            }
        }
        public string StateCode
        {
            get
            {
                return _StateCode;
            }
            set
            {
                _StateCode = value;
            }
        }
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
            }
        }
        public string PinCode
        {
            get
            {
                return _PinCode;
            }
            set
            {
                _PinCode = value;
            }
        }
        public string TelNo
        {
            get
            {
                return _TelNo;
            }
            set
            {
                _TelNo = value;
            }
        }
        public string EmailID
        {
            get
            {
                return _EmailID;
            }
            set
            {
                _EmailID = value;
            }
        }
        public string MobileNo
        {
            get
            {
                return _MobileNo;
            }
            set
            {
                _MobileNo = value;
            }
        }
        public string DepartmentCode
        {
            get
            {
                return _DepartmentCode;
            }
            set
            {
                _DepartmentCode = value;
            }
        }
        public string DesignationCode
        {
            get
            {
                return _DesignationCode;
            }
            set
            {
                _DesignationCode = value;
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

        public string CountryCode
        {
            get
            {
                return _CountryCode;
            }
            set
            {
                _CountryCode = value;
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
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.EmployeeName = Convert.ToString(dr["EmployeeName"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.City = Convert.ToString(dr["City"]);
            this.PinCode = Convert.ToString(dr["PinCode"]);
            this.TelNo = Convert.ToString(dr["TelNo"]);
            this.EmailID = Convert.ToString(dr["EmailID"]);
            this.MobileNo = Convert.ToString(dr["MobileNo"]);
            this.DepartmentCode = Convert.ToString(dr["DepartmentCode"]);
            this.DesignationCode = Convert.ToString(dr["DesignationCode"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}