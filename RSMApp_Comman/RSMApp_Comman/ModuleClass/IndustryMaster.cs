
//Created On :: 03, October, 2012
//Private const string ClassName = "IndustryMaster"
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
    public class IndustryMaster
    {
        private string _IndustryName;
        private string _CreatedDate;
        private string _ClientCode;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string IndustryName
        {
            get
            {
                return _IndustryName;
            }
            set
            {
                _IndustryName = value;
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
            this.IndustryName = Convert.ToString(dr["IndustryName"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}