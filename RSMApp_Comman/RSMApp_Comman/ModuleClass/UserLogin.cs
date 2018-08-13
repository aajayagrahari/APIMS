
//Created On :: 09, October, 2012
//Private const string ClassName = "UserLogin"
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
    public class UserLogin
    {
        private int _idUserLogin;
        private string _UserName;
        private string _IP;
        private string _UserAgent;
        private string _LoginDate;
        private string _LastRefreshedDate;
        private int _IsSuccessfull;
        private string _Host;
        private string _ClientCode;
        private string _PartnerCode;



        public int idUserLogin
        {
            get
            {
                return _idUserLogin;
            }
            set
            {
                _idUserLogin = value;
            }
        }
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
        public string IP
        {
            get
            {
                return _IP;
            }
            set
            {
                _IP = value;
            }
        }
        public string UserAgent
        {
            get
            {
                return _UserAgent;
            }
            set
            {
                _UserAgent = value;
            }
        }
        public string LoginDate
        {
            get
            {
                return _LoginDate;
            }
            set
            {
                _LoginDate = value;
            }
        }
        public string LastRefreshedDate
        {
            get
            {
                return _LastRefreshedDate;
            }
            set
            {
                _LastRefreshedDate = value;
            }
        }
        public int IsSuccessfull
        {
            get
            {
                return _IsSuccessfull;
            }
            set
            {
                _IsSuccessfull = value;
            }
        }
        public string Host
        {
            get
            {
                return _Host;
            }
            set
            {
                _Host = value;
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


        public void SetObjectInfo(DataRow dr)
        {
            this.idUserLogin = Convert.ToInt32(dr["idUserLogin"]);
            this.UserName = Convert.ToString(dr["UserName"]);
            this.IP = Convert.ToString(dr["IP"]);
            this.UserAgent = Convert.ToString(dr["UserAgent"]);
            this.LoginDate = Convert.ToString(dr["LoginDate"]);
            this.LastRefreshedDate = Convert.ToString(dr["LastRefreshedDate"]);
            this.IsSuccessfull = Convert.ToInt32(dr["IsSuccessfull"]);
            this.Host = Convert.ToString(dr["Host"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);

        }
    }
}