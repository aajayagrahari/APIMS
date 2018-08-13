
//Created On :: 09, October, 2012
//Private const string ClassName = "PartnerUserMaster"
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
    public class PartnerUserMaster
    {
        private string _PartnerUserName;
        private string _PartnerUserPassword;
        private string _PartnerEmployeeCode;
        private string _PartnerCode;
        private string _FirstName;
        private string _LastName;
        private string _Gender;
        private string _Address1;
        private string _Address2;
        private string _Phone1;
        private string _Phone2;
        private string _EmailID;
        private string _CountryCode;
        private string _StateCode;
        private string _City;
        private int _IsBlocked;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PartnerUserName
        {
            get
            {
                return _PartnerUserName;
            }
            set
            {
                _PartnerUserName = value;
            }
        }
        public string PartnerUserPassword
        {
            get
            {
                return _PartnerUserPassword;
            }
            set
            {
                _PartnerUserPassword = value;
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
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }
        public string Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                _Gender = value;
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
        public string Phone1
        {
            get
            {
                return _Phone1;
            }
            set
            {
                _Phone1 = value;
            }
        }
        public string Phone2
        {
            get
            {
                return _Phone2;
            }
            set
            {
                _Phone2 = value;
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
        public int IsBlocked
        {
            get
            {
                return _IsBlocked;
            }
            set
            {
                _IsBlocked = value;
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
            this.PartnerUserName = Convert.ToString(dr["PartnerUserName"]);
            this.PartnerUserPassword = Convert.ToString(dr["PartnerUserPassword"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.FirstName = Convert.ToString(dr["FirstName"]);
            this.LastName = Convert.ToString(dr["LastName"]);
            this.Gender = Convert.ToString(dr["Gender"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.Phone1 = Convert.ToString(dr["Phone1"]);
            this.Phone2 = Convert.ToString(dr["Phone2"]);
            this.EmailID = Convert.ToString(dr["EmailID"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.City = Convert.ToString(dr["City"]);
            this.IsBlocked = Convert.ToInt32(dr["IsBlocked"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}