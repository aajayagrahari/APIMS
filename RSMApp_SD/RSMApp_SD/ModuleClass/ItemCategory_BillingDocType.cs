
//Created On :: 12, September, 2012
//Private const string ClassName = "ItemCategory_BillingDocType"
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
    public class ItemCategory_BillingDocType
    {
        private string _BillingDocTypeCode;
        private string _BillingTypeDesc;
        private string _ItemCatGroupCode;
        private string _HLItemCategoryCode;
        private string _BLItemCategoryCode;
        private string _OptBLItemCategoryCode1;
        private string _OptBLItemCategoryCode2;
        private string _OptBLItemCategoryCode3;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string BillingDocTypeCode
        {
            get
            {
                return _BillingDocTypeCode;
            }
            set
            {
                _BillingDocTypeCode = value;
            }
        }
        public string BillingTypeDesc
        {
            get
            {
                return _BillingTypeDesc;
            }
            set
            {
                _BillingTypeDesc = value;
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
        public string BLItemCategoryCode
        {
            get
            {
                return _BLItemCategoryCode;
            }
            set
            {
                _BLItemCategoryCode = value;
            }
        }
        public string OptBLItemCategoryCode1
        {
            get
            {
                return _OptBLItemCategoryCode1;
            }
            set
            {
                _OptBLItemCategoryCode1 = value;
            }
        }
        public string OptBLItemCategoryCode2
        {
            get
            {
                return _OptBLItemCategoryCode2;
            }
            set
            {
                _OptBLItemCategoryCode2 = value;
            }
        }
        public string OptBLItemCategoryCode3
        {
            get
            {
                return _OptBLItemCategoryCode3;
            }
            set
            {
                _OptBLItemCategoryCode3 = value;
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
            this.BillingDocTypeCode = Convert.ToString(dr["BillingDocTypeCode"]);
            this.BillingTypeDesc = Convert.ToString(dr["BillingTypeDesc"]);
            this.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]);
            this.HLItemCategoryCode = Convert.ToString(dr["HLItemCategoryCode"]);
            this.BLItemCategoryCode = Convert.ToString(dr["BLItemCategoryCode"]);
            this.OptBLItemCategoryCode1 = Convert.ToString(dr["OptBLItemCategoryCode1"]);
            this.OptBLItemCategoryCode2 = Convert.ToString(dr["OptBLItemCategoryCode2"]);
            this.OptBLItemCategoryCode3 = Convert.ToString(dr["OptBLItemCategoryCode3"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}