
//Created On :: 07, May, 2012
//Private const string ClassName = "CompanyAddInfo"
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
    public class CompanyAddInfo
    {
        private string _CompanyCode;
        private string _ParamName;
        private string _ParamValue;
        private int _IsDeleted;



        public string CompanyCode
        {
            get
            {
                return _CompanyCode;
            }
            set
            {
                _CompanyCode = value;
            }
        }
        public string ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }
        public string ParamValue
        {
            get
            {
                return _ParamValue;
            }
            set
            {
                _ParamValue = value;
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
            this.CompanyCode = Convert.ToString(dr["CompanyCode"]);
            this.ParamName = Convert.ToString(dr["ParamName"]);
            this.ParamValue = Convert.ToString(dr["ParamValue"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}