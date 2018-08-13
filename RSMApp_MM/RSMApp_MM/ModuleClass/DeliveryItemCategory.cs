
//Created On :: 06, September, 2012
//Private const string ClassName = "DeliveryItemCategory"
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
    public class DeliveryItemCategory
    {
        private string _DItemCategoryCode;
        private string _DItemCategoryDesc;
        private int _BillingRelevant;
        private int _IsReturn;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string DItemCategoryCode
        {
            get
            {
                return _DItemCategoryCode;
            }
            set
            {
                _DItemCategoryCode = value;
            }
        }
        public string DItemCategoryDesc
        {
            get
            {
                return _DItemCategoryDesc;
            }
            set
            {
                _DItemCategoryDesc = value;
            }
        }
        public int BillingRelevant
        {
            get
            {
                return _BillingRelevant;
            }
            set
            {
                _BillingRelevant = value;
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
            this.DItemCategoryCode = Convert.ToString(dr["DItemCategoryCode"]);
            this.DItemCategoryDesc = Convert.ToString(dr["DItemCategoryDesc"]);
            this.BillingRelevant = Convert.ToInt32(dr["BillingRelevant"]);
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