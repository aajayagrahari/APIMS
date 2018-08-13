
//Created On :: 25, June, 2012
//Private const string ClassName = "CharactersticsValueMaster"
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
    public class CharactersticsValueMaster
    {
        private int _idCharactersticsValue;
        private string _ObjectKey;
        private string _ObjectTable;
        private int _idClass;
        private string _ClassType;
        private string _ClassName;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _CharactersticsName;
        private string _CharactersticsValue;
        private string _CharValuetype;

        private string _MaterialCode;

        
        public int idCharactersticsValue
        {
            get
            {
                return _idCharactersticsValue;
            }
            set
            {
                _idCharactersticsValue = value;
            }
        }

        public string ObjectKey
        {
            get
            {
                return _ObjectKey;
            }
            set
            {
                _ObjectKey = value;
            }
        }

        public string ObjectTable
        {
            get
            {
                return _ObjectTable;
            }
            set
            {
                _ObjectTable = value;
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

        public string CharactersticsName
        {
            get
            {
                return _CharactersticsName;
            }
            set
            {
                _CharactersticsName = value;
            }
        }

        public string CharactersticsValue
        {
            get
            {
                return _CharactersticsValue;
            }
            set
            {
                _CharactersticsValue = value;
            }
        }

        public string CharValuetype
        {
            get
            {
                return _CharValuetype;
            }
            set
            {
                _CharValuetype = value;
            }
        }

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

        
        public void SetObjectInfo(DataRow dr)
        {
            this.idCharactersticsValue = Convert.ToInt32(dr["idCharactersticsValue"]);
            this.ObjectKey = Convert.ToString(dr["ObjectKey"]);
            this.ObjectTable = Convert.ToString(dr["ObjectTable"]);
            this.idClass = Convert.ToInt32(dr["idClass"]);
            this.ClassType = Convert.ToString(dr["ClassType"]);
            this.ClassName = Convert.ToString(dr["ClassName"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
        
        public void SetObjectInfo2(DataRow dr)
        {
            this.idCharactersticsValue = Convert.ToInt32(dr["idCharactersticsValue"]);
            this.ObjectKey = Convert.ToString(dr["ObjectKey"]);
            this.ObjectTable = Convert.ToString(dr["ObjectTable"]).Trim();
            this.idClass = Convert.ToInt32(dr["idClass"]);
            this.ClassType = Convert.ToString(dr["ClassType"]);
            this.ClassName = Convert.ToString(dr["ClassName"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CharactersticsName = Convert.ToString(dr["CharactersticsName"]);
            this.CharactersticsValue = Convert.ToString(dr["CharactersticsValue"]);
            this.CharValuetype = Convert.ToString(dr["CharValuetype"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }


    }
}