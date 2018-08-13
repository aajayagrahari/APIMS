
//Created On :: 05, May, 2012
//Private const string ClassName = "Client"
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
    public class Client
    {
        private string _ClientCode;
        private string _ClientName;
        private string _ClientDescription;
        private string _LicenceCode;
        private int _LicencePeriod;
        private int _ValuationMode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private string _CreatedBy;
        private string _ModifiedBy;
        private int _IsDeleted;



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
        public string ClientName
        {
            get
            {
                return _ClientName;
            }
            set
            {
                _ClientName = value;
            }
        }
        public string ClientDescription
        {
            get
            {
                return _ClientDescription;
            }
            set
            {
                _ClientDescription = value;
            }
        }
        public string LicenceCode
        {
            get
            {
                return _LicenceCode;
            }
            set
            {
                _LicenceCode = value;
            }
        }
        public int LicencePeriod
        {
            get
            {
                return _LicencePeriod;
            }
            set
            {
                _LicencePeriod = value;
            }
        }

        public int ValuationMode
        {
            get
            {
                return _ValuationMode;
            }
            set
            {
                _ValuationMode = value;
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
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.ClientName = Convert.ToString(dr["ClientName"]);
            this.ClientDescription = Convert.ToString(dr["ClientDescription"]);
            this.LicenceCode = Convert.ToString(dr["LicenceCode"]);
            this.LicencePeriod = Convert.ToInt32(dr["LicencePeriod"]);
            this.ValuationMode = Convert.ToInt32(dr["ValuationMode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}