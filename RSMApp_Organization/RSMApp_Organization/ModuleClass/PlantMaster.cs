
//Created On :: 15, May, 2012
//Private const string ClassName = "PlantMaster"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Organization
{
    [Serializable]
    public class PlantMaster
    {
        private string _PlantCode;
        private string _PlantName;
        private string _PlantDesc;
        private string _Address1;
        private string _Address2;
        private string _CountryCode;
        private string _StateCode;
        private string _City;
        private string _PinCode;
        private string _TelNo;
        private string _MobileNo;
        private string _LanguageCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PlantCode
        {
            get
            {
                return _PlantCode;
            }
            set
            {
                _PlantCode = value;
            }
        }
        public string PlantName
        {
            get
            {
                return _PlantName;
            }
            set
            {
                _PlantName = value;
            }
        }
        public string PlantDesc
        {
            get
            {
                return _PlantDesc;
            }
            set
            {
                _PlantDesc = value;
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
        public string LanguageCode
        {
            get
            {
                return _LanguageCode;
            }
            set
            {
                _LanguageCode = value;
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
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.PlantName = Convert.ToString(dr["PlantName"]);
            this.PlantDesc = Convert.ToString(dr["PlantDesc"]);
            this.Address1 = Convert.ToString(dr["Address1"]);
            this.Address2 = Convert.ToString(dr["Address2"]);
            this.CountryCode = Convert.ToString(dr["CountryCode"]);
            this.StateCode = Convert.ToString(dr["StateCode"]);
            this.City = Convert.ToString(dr["City"]);
            this.PinCode = Convert.ToString(dr["PinCode"]);
            this.TelNo = Convert.ToString(dr["TelNo"]);
            this.MobileNo = Convert.ToString(dr["MobileNo"]);
            this.LanguageCode = Convert.ToString(dr["LanguageCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}