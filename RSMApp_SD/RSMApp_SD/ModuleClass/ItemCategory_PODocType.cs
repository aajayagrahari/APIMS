
//Created On :: 15, October, 2012
//Private const string ClassName = "ItemCategory_PODocType"
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
    public class ItemCategory_PODocType
    {
        private string _PODocTypeCode;
        private string _PODocTypeDesc;
        private string _ItemCatGroupCode;
        private string _HLPOItemCategoryCode;
        private string _POItemCategoryCode;
        private string _OptPOItemCategoryCode1;
        private string _OptPOItemCategoryCode2;
        private string _OptPOItemCategoryCode3;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string PODocTypeCode
        {
            get
            {
                return _PODocTypeCode;
            }
            set
            {
                _PODocTypeCode = value;
            }
        }
        public string PODocTypeDesc
        {
            get
            {
                return _PODocTypeDesc;
            }
            set
            {
                _PODocTypeDesc = value;
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
        public string HLPOItemCategoryCode
        {
            get
            {
                return _HLPOItemCategoryCode;
            }
            set
            {
                _HLPOItemCategoryCode = value;
            }
        }
        public string POItemCategoryCode
        {
            get
            {
                return _POItemCategoryCode;
            }
            set
            {
                _POItemCategoryCode = value;
            }
        }
        public string OptPOItemCategoryCode1
        {
            get
            {
                return _OptPOItemCategoryCode1;
            }
            set
            {
                _OptPOItemCategoryCode1 = value;
            }
        }
        public string OptPOItemCategoryCode2
        {
            get
            {
                return _OptPOItemCategoryCode2;
            }
            set
            {
                _OptPOItemCategoryCode2 = value;
            }
        }
        public string OptPOItemCategoryCode3
        {
            get
            {
                return _OptPOItemCategoryCode3;
            }
            set
            {
                _OptPOItemCategoryCode3 = value;
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
            this.PODocTypeCode = Convert.ToString(dr["PODocTypeCode"]);
            this.PODocTypeDesc = Convert.ToString(dr["PODocTypeDesc"]);
            this.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]);
            this.HLPOItemCategoryCode = Convert.ToString(dr["HLPOItemCategoryCode"]);
            this.POItemCategoryCode = Convert.ToString(dr["POItemCategoryCode"]);
            this.OptPOItemCategoryCode1 = Convert.ToString(dr["OptPOItemCategoryCode1"]);
            this.OptPOItemCategoryCode2 = Convert.ToString(dr["OptPOItemCategoryCode2"]);
            this.OptPOItemCategoryCode3 = Convert.ToString(dr["OptPOItemCategoryCode3"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}