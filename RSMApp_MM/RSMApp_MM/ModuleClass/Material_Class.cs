
//Created On :: 23, June, 2012
//Private const string ClassName = "Material_Class"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    [Serializable]
    public class Material_Class
    {
        private string _MaterialCode;
        private int _idClass;
        private string _ClassType;
        private string _ClassName;
        private string _MCStatus;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        
        public string MaterialCode
        {
            get
            {
                return _MaterialCode;
            }
            set
            {
                _MaterialCode = value;
            }
        }
        public int idClass
        {
            get
            {
                return _idClass;
            }
            set
            {
                _idClass = value;
            }
        }
        public string ClassType
        {
            get
            {
                return _ClassType;
            }
            set
            {
                _ClassType = value;
            }
        }
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                _ClassName = value;
            }
        }
        public string MCStatus
        {
            get
            {
                return _MCStatus;
            }
            set
            {
                _MCStatus = value;
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
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.idClass = Convert.ToInt32(dr["idClass"]);
            this.ClassType = Convert.ToString(dr["ClassType"]);
            this.ClassName = Convert.ToString(dr["ClassName"]);
            this.MCStatus = Convert.ToString(dr["MCStatus"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}