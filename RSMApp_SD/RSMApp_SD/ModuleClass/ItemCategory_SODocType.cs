
//Created On :: 12, September, 2012
//Private const string ClassName = "ItemCategory_SODocType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    [Serializable]
    public class ItemCategory_SODocType
    {
        private string _SOTypeCode;
        private string _SOTypeDesc;
        private string _ItemCatGroupCode;
        private string _HLItemCategoryCode;
        private string _ItemCategoryCode;
        private string _OptItemCategoryCode1;
        private string _OptItemCategoryCode2;
        private string _OptItemCategoryCode3;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string SOTypeCode
        {
            get
            {
                return _SOTypeCode;
            }
            set
            {
                _SOTypeCode = value;
            }
        }
        public string SOTypeDesc
        {
            get
            {
                return _SOTypeDesc;
            }
            set
            {
                _SOTypeDesc = value;
            }
        }
        public string ItemCatGroupCode
        {
            get
            {
                return _ItemCatGroupCode;
            }
            set
            {
                _ItemCatGroupCode = value;
            }
        }
        public string HLItemCategoryCode
        {
            get
            {
                return _HLItemCategoryCode;
            }
            set
            {
                _HLItemCategoryCode = value;
            }
        }
        public string ItemCategoryCode
        {
            get
            {
                return _ItemCategoryCode;
            }
            set
            {
                _ItemCategoryCode = value;
            }
        }
        public string OptItemCategoryCode1
        {
            get
            {
                return _OptItemCategoryCode1;
            }
            set
            {
                _OptItemCategoryCode1 = value;
            }
        }
        public string OptItemCategoryCode2
        {
            get
            {
                return _OptItemCategoryCode2;
            }
            set
            {
                _OptItemCategoryCode2 = value;
            }
        }
        public string OptItemCategoryCode3
        {
            get
            {
                return _OptItemCategoryCode3;
            }
            set
            {
                _OptItemCategoryCode3 = value;
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
            this.SOTypeCode = Convert.ToString(dr["SOTypeCode"]);
            this.SOTypeDesc = Convert.ToString(dr["SOTypeDesc"]);
            this.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]);
            this.HLItemCategoryCode = Convert.ToString(dr["HLItemCategoryCode"]);
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.OptItemCategoryCode1 = Convert.ToString(dr["OptItemCategoryCode1"]);
            this.OptItemCategoryCode2 = Convert.ToString(dr["OptItemCategoryCode2"]);
            this.OptItemCategoryCode3 = Convert.ToString(dr["OptItemCategoryCode3"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}