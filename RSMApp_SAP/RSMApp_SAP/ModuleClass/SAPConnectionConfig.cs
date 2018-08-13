
//Created On :: 08, June, 2012
//Private const string ClassName = "SAPConnectionConfig"
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
    public class SAPConnectionConfig
    {
        private string _SAPConCode;
        private string _SAPServer;
        private string _SAPRouter;
        private string _AppServerHost;
        private string _SystemNumber;
        private string _SystemID;
        private string _SAPUser;
        private string _SAPPassword;
        private string _SAPClient;
        private string _SAPLanguage;
        private string _PoolSize;
        private string _MaxPoolSize;
        private string _IdleTimeout;
        private int _IsActive;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string SAPConCode
        {
            get
            {
                return _SAPConCode;
            }
            set
            {
                _SAPConCode = value;
            }
        }
        public string SAPServer
        {
            get
            {
                return _SAPServer;
            }
            set
            {
                _SAPServer = value;
            }
        }
        public string SAPRouter
        {
            get
            {
                return _SAPRouter;
            }
            set
            {
                _SAPRouter = value;
            }
        }
        public string AppServerHost
        {
            get
            {
                return _AppServerHost;
            }
            set
            {
                _AppServerHost = value;
            }
        }
        public string SystemNumber
        {
            get
            {
                return _SystemNumber;
            }
            set
            {
                _SystemNumber = value;
            }
        }
        public string SystemID
        {
            get
            {
                return _SystemID;
            }
            set
            {
                _SystemID = value;
            }
        }
        public string SAPUser
        {
            get
            {
                return _SAPUser;
            }
            set
            {
                _SAPUser = value;
            }
        }
        public string SAPPassword
        {
            get
            {
                return _SAPPassword;
            }
            set
            {
                _SAPPassword = value;
            }
        }
        public string SAPClient
        {
            get
            {
                return _SAPClient;
            }
            set
            {
                _SAPClient = value;
            }
        }
        public string SAPLanguage
        {
            get
            {
                return _SAPLanguage;
            }
            set
            {
                _SAPLanguage = value;
            }
        }
        public string PoolSize
        {
            get
            {
                return _PoolSize;
            }
            set
            {
                _PoolSize = value;
            }
        }
        public string MaxPoolSize
        {
            get
            {
                return _MaxPoolSize;
            }
            set
            {
                _MaxPoolSize = value;
            }
        }
        public string IdleTimeout
        {
            get
            {
                return _IdleTimeout;
            }
            set
            {
                _IdleTimeout = value;
            }
        }
        public int IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
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
            this.SAPConCode = Convert.ToString(dr["SAPConCode"]);
            this.SAPServer = Convert.ToString(dr["SAPServer"]);
            this.SAPRouter = Convert.ToString(dr["SAPRouter"]);
            this.AppServerHost = Convert.ToString(dr["AppServerHost"]);
            this.SystemNumber = Convert.ToString(dr["SystemNumber"]);
            this.SystemID = Convert.ToString(dr["SystemID"]);
            this.SAPUser = Convert.ToString(dr["SAPUser"]);
            this.SAPPassword = Convert.ToString(dr["SAPPassword"]);
            this.SAPClient = Convert.ToString(dr["SAPClient"]);
            this.SAPLanguage = Convert.ToString(dr["SAPLanguage"]);
            this.PoolSize = Convert.ToString(dr["PoolSize"]);
            this.MaxPoolSize = Convert.ToString(dr["MaxPoolSize"]);
            this.IdleTimeout = Convert.ToString(dr["IdleTimeout"]);
            this.IsActive = Convert.ToInt32(dr["IsActive"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}