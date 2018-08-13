
//Created On :: 07, May, 2012
//Private const string ClassName = "UserMaster"
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
    public class UserMaster
    {
        private string _UserName;
        private string _UserPassword;
        private string _FirstName;
        private string _LastName;
        private string _Gender;
        private string _EMPCode;
        private string _Address1;
        private string _Address2;
        private string _Phone1;
        private string _Phone2;
        private string _EmailID;
        private string _City;
        private string _StateCode;
        private string _CountryCode;
        private string _ClientCode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        public string UserPassword
        {
            get
            {
                return _UserPassword;
            }
            set
            {
                _UserPassword = value;
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
        public string EMPCode
        {
            get
            {
                return _EMPCode;
            }
            set
            {
                _EMPCode = value;
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
            this.UserName = Convert.ToString(dr["UserName"]);
            this.UserPassword = Convert.ToString(dr["UserPassword"]);
            this.FirstName = Convert.ToString(dr["FirstName"]);
            this.LastName = Convert.ToString(dr["LastName"]);
            this.Gender = Convert.ToString(dr["Gender"]);
            this.EMPCode = Convert.ToString(dr["EMPCode"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.Phone1 = Convert.ToString(dr["Phone1"]);
            this.Phone2 = Convert.ToString(dr["Phone2"]);
            this.EmailID = Convert.ToString(dr["EmailID"]);
            this.City = Convert.ToString(dr["City"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}