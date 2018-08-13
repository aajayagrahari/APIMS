
//Created On :: 03, October, 2012
//Private const string ClassName = "IncoTerms"
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
    public class IncoTerms
    {
        private string _IncoTermsCode;
        private string _IncoTermsDesc;
        private string _ClientCode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string IncoTermsCode
        {
            get
            {
                return _IncoTermsCode;
            }
            set
            {
                _IncoTermsCode = value;
            }
        }
        public string IncoTermsDesc
        {
            get
            {
                return _IncoTermsDesc;
            }
            set
            {
                _IncoTermsDesc = value;
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
            this.IncoTermsCode = Convert.ToString(dr["IncoTermsCode"]);
            this.IncoTermsDesc = Convert.ToString(dr["IncoTermsDesc"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}