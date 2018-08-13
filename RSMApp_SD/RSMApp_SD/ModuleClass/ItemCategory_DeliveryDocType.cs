
//Created On :: 12, September, 2012
//Private const string ClassName = "ItemCategory_DeliveryDocType"
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
    public class ItemCategory_DeliveryDocType
    {
        private string _DeliveryDocTypeCode;
        private string _DeliveryTypeDesc;
        private string _ItemCatGroupCode;
        private string _HLItemCategoryCode;
        private string _DLItemCategoryCode;
        private string _OptDLItemCategoryCode1;
        private string _OptDLItemCategoryCode2;
        private string _OptDLItemCategoryCode3;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string DeliveryDocTypeCode
        {
            get
            {
                return _DeliveryDocTypeCode;
            }
            set
            {
                _DeliveryDocTypeCode = value;
            }
        }
        public string DeliveryTypeDesc
        {
            get
            {
                return _DeliveryTypeDesc;
            }
            set
            {
                _DeliveryTypeDesc = value;
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
        public string DLItemCategoryCode
        {
            get
            {
                return _DLItemCategoryCode;
            }
            set
            {
                _DLItemCategoryCode = value;
            }
        }
        public string OptDLItemCategoryCode1
        {
            get
            {
                return _OptDLItemCategoryCode1;
            }
            set
            {
                _OptDLItemCategoryCode1 = value;
            }
        }
        public string OptDLItemCategoryCode2
        {
            get
            {
                return _OptDLItemCategoryCode2;
            }
            set
            {
                _OptDLItemCategoryCode2 = value;
            }
        }
        public string OptDLItemCategoryCode3
        {
            get
            {
                return _OptDLItemCategoryCode3;
            }
            set
            {
                _OptDLItemCategoryCode3 = value;
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
            this.DeliveryDocTypeCode = Convert.ToString(dr["DeliveryDocTypeCode"]);
            this.DeliveryTypeDesc = Convert.ToString(dr["DeliveryTypeDesc"]);
            this.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]);
            this.HLItemCategoryCode = Convert.ToString(dr["HLItemCategoryCode"]);
            this.DLItemCategoryCode = Convert.ToString(dr["DLItemCategoryCode"]);
            this.OptDLItemCategoryCode1 = Convert.ToString(dr["OptDLItemCategoryCode1"]);
            this.OptDLItemCategoryCode2 = Convert.ToString(dr["OptDLItemCategoryCode2"]);
            this.OptDLItemCategoryCode3 = Convert.ToString(dr["OptDLItemCategoryCode3"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}