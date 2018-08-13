
//Created On :: 19, June, 2012
//Private const string ClassName = "CharAllowedValue"
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
    public class CharAllowedValue
    {
        private int _idClass;
        private int _idCharacteristic;
        private int _idCharAllowedValue;
        private string _CharacteristicValueChar;
        private int _CharValueNumFrom;
        private int _CharValueNumTo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;

        private int _IsDeleted;


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
        public int idCharacteristic
        {
            get
            {
                return _idCharacteristic;
            }
            set
            {
                _idCharacteristic = value;
            }
        }
        public int idCharAllowedValue
        {
            get
            {
                return _idCharAllowedValue;
            }
            set
            {
                _idCharAllowedValue = value;
            }
        }
        public string CharacteristicValueChar
        {
            get
            {
                return _CharacteristicValueChar;
            }
            set
            {
                _CharacteristicValueChar = value;
            }
        }
        public int CharValueNumFrom
        {
            get
            {
                return _CharValueNumFrom;
            }
            set
            {
                _CharValueNumFrom = value;
            }
        }
        public int CharValueNumTo
        {
            get
            {
                return _CharValueNumTo;
            }
            set
            {
                _CharValueNumTo = value;
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
            this.idClass = Convert.ToInt32(dr["idClass"]);
            this.idCharacteristic = Convert.ToInt32(dr["idCharacteristic"]);
            this.idCharAllowedValue = Convert.ToInt32(dr["idCharAllowedValue"]);
            this.CharacteristicValueChar = Convert.ToString(dr["CharacteristicValueChar"]);
            this.CharValueNumFrom = Convert.ToInt32(dr["CharValueNumFrom"]);
            this.CharValueNumTo = Convert.ToInt32(dr["CharValueNumTo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);

        }
    }
}