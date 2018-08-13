
//Created On :: 25, June, 2012
//Private const string ClassName = "CharactersticValueCurrency"
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
    public class CharactersticValueCurrency
    {
        private int _idCharactersticsValue;
        private string _CharactersticsName;
        private int _CharactersticsValue;
        private string _ClientCode;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;
        
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
        public int CharactersticsValue
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
            this.idCharactersticsValue = Convert.ToInt32(dr["idCharactersticsValue"]);
            this.CharactersticsName = Convert.ToString(dr["CharactersticsName"]);
            this.CharactersticsValue = Convert.ToInt32(dr["CharactersticsValue"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}