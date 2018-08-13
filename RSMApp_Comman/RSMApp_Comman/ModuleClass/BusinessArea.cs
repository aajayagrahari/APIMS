
//Created On :: 28, September, 2012
//Private const string ClassName = "BusinessArea"
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
    public class BusinessArea
    {
        private string _BusinessAreaCode;
        private string _BusinessAreaName;
        private string _ClientCode;



        public string BusinessAreaCode
        {
            get
            {
                return _BusinessAreaCode;
            }
            set
            {
                _BusinessAreaCode = value;
            }
        }
        public string BusinessAreaName
        {
            get
            {
                return _BusinessAreaName;
            }
            set
            {
                _BusinessAreaName = value;
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


        public void SetObjectInfo(DataRow dr)
        {
            this.BusinessAreaCode = Convert.ToString(dr["BusinessAreaCode"]);
            this.BusinessAreaName = Convert.ToString(dr["BusinessAreaName"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);

        }
    }
}