
//Created On :: 26, September, 2012
//Private const string ClassName = "ProfitCenter"
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
    public class ProfitCenter
    {
        private string _ProfitCenterCode;
        private string _PCName;
        private string _PCDecription;
        private string _COACode;
        private string _PCStatus;
        private string _ValidFrom;
        private string _ValidTo;
        private string _ContactPerson;
        private string _Department;
        private string _PCGroup;
        private string _TelePhone1;
        private string _TelePhone2;
        private string _MobileNo;
        private string _EmailID;
        private string _Title;
        private string _Name1;
        private string _Name2;
        private string _Name3;
        private string _Name4;
        private string _Address1;
        private string _Address2;
        private string _CountryCode;
        private string _StateCode;
        private string _City;
        private string _ZipCode;
        private string _ClientCode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private string _CreatedBy;
        private string _ModifiedBy;
        private int _IsDeleted;



        public string ProfitCenterCode
        {
            get
            {
                return _ProfitCenterCode;
            }
            set
            {
                _ProfitCenterCode = value;
            }
        }
        public string PCName
        {
            get
            {
                return _PCName;
            }
            set
            {
                _PCName = value;
            }
        }
        public string PCDecription
        {
            get
            {
                return _PCDecription;
            }
            set
            {
                _PCDecription = value;
            }
        }
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
        public string PCStatus
        {
            get
            {
                return _PCStatus;
            }
            set
            {
                _PCStatus = value;
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
        public string ContactPerson
        {
            get
            {
                return _ContactPerson;
            }
            set
            {
                _ContactPerson = value;
            }
        }
        public string Department
        {
            get
            {
                return _Department;
            }
            set
            {
                _Department = value;
            }
        }
        public string PCGroup
        {
            get
            {
                return _PCGroup;
            }
            set
            {
                _PCGroup = value;
            }
        }
        public string TelePhone1
        {
            get
            {
                return _TelePhone1;
            }
            set
            {
                _TelePhone1 = value;
            }
        }
        public string TelePhone2
        {
            get
            {
                return _TelePhone2;
            }
            set
            {
                _TelePhone2 = value;
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
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public string Name1
        {
            get
            {
                return _Name1;
            }
            set
            {
                _Name1 = value;
            }
        }
        public string Name2
        {
            get
            {
                return _Name2;
            }
            set
            {
                _Name2 = value;
            }
        }
        public string Name3
        {
            get
            {
                return _Name3;
            }
            set
            {
                _Name3 = value;
            }
        }
        public string Name4
        {
            get
            {
                return _Name4;
            }
            set
            {
                _Name4 = value;
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
        public string ZipCode
        {
            get
            {
                return _ZipCode;
            }
            set
            {
                _ZipCode = value;
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
            this.ProfitCenterCode = Convert.ToString(dr["ProfitCenterCode"]);
            this.PCName = Convert.ToString(dr["PCName"]);
            this.PCDecription = Convert.ToString(dr["PCDecription"]);
            this.COACode = Convert.ToString(dr["COACode"]);
            this.PCStatus = Convert.ToString(dr["PCStatus"]);
            this.ValidFrom = Convert.ToString(dr["ValidFrom"]);
            this.ValidTo = Convert.ToString(dr["ValidTo"]);
            this.ContactPerson = Convert.ToString(dr["ContactPerson"]);
            this.Department = Convert.ToString(dr["Department"]);
            this.PCGroup = Convert.ToString(dr["PCGroup"]);
            this.TelePhone1 = Convert.ToString(dr["TelePhone1"]);
            this.TelePhone2 = Convert.ToString(dr["TelePhone2"]);
            this.MobileNo = Convert.ToString(dr["MobileNo"]);
            this.EmailID = Convert.ToString(dr["EmailID"]);
            this.Title = Convert.ToString(dr["Title"]);
            this.Name1 = Convert.ToString(dr["Name1"]);
            this.Name2 = Convert.ToString(dr["Name2"]);
            this.Name3 = Convert.ToString(dr["Name3"]);
            this.Name4 = Convert.ToString(dr["Name4"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.City = Convert.ToString(dr["City"]);
            this.ZipCode = Convert.ToString(dr["ZipCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}