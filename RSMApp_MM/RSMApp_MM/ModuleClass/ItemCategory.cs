
//Created On :: 16, May, 2012
//Private const string ClassName = "ItemCategory"
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
    public class ItemCategory
    {
        private string _ItemCategoryCode;
        private string _ItemCategoryDesc;
        private int _ScheduleLineAllowed;
        private int _IsBusinessItem;
        private string _ItemTypeCode;
        private int _DeliveryRelevant;
        private string _BillingRelevantCode;
        private int _IsReturn; 
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string ItemCategoryDesc
        {
            get
            {
                return _ItemCategoryDesc;
            }
            set
            {
                _ItemCategoryDesc = value;
            }
        }

        public int ScheduleLineAllowed
        {
            get
            {
                return _ScheduleLineAllowed;
            }
            set
            {
                _ScheduleLineAllowed = value;
            }
        }
        public int IsBusinessItem
        {
            get
            {
                return _IsBusinessItem;
            }
            set
            {
                _IsBusinessItem = value;
            }
        }
        public string ItemTypeCode
        {
            get
            {
                return _ItemTypeCode;
            }
            set
            {
                _ItemTypeCode = value;
            }
        }
        public int DeliveryRelevant
        {
            get
            {
                return _DeliveryRelevant;
            }
            set
            {
                _DeliveryRelevant = value;
            }
        }
        public string BillingRelevantCode
        {
            get
            {
                return _BillingRelevantCode;
            }
            set
            {
                _BillingRelevantCode = value;
            }
        }
        public int IsReturn
        {
            get
            {
                return _IsReturn;
            }
            set
            {
                _IsReturn = value;
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
            this.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]);
            this.ItemCategoryDesc = Convert.ToString(dr["ItemCategoryDesc"]);

            this.ScheduleLineAllowed = Convert.ToInt32(dr["ScheduleLineAllowed"]);
            this.IsBusinessItem = Convert.ToInt32(dr["IsBusinessItem"]);
            this.ItemTypeCode = Convert.ToString(dr["ItemTypeCode"]);
            this.DeliveryRelevant = Convert.ToInt32(dr["DeliveryRelevant"]);
            this.BillingRelevantCode = Convert.ToString(dr["BillingRelevantCode"]);
            this.IsReturn = Convert.ToInt32(dr["IsReturn"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}