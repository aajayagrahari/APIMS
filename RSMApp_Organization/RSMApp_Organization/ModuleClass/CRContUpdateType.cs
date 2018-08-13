
//Created On :: 10, May, 2012
//Private const string ClassName = "CRContUpdateType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Organization
{
    [Serializable]
    public class CRContUpdateType
    {
        private string _CRUpdateCode;
        private string _UpdateType;
        private string _UpdateDesc;
        private int _OpenSalesOrder;
        private int _OpenDelivery;
        private int _OpenBilling;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string CRUpdateCode
        {
            get
            {
                return _CRUpdateCode;
            }
            set
            {
                _CRUpdateCode = value;
            }
        }
        public string UpdateType
        {
            get
            {
                return _UpdateType;
            }
            set
            {
                _UpdateType = value;
            }
        }
        public string UpdateDesc
        {
            get
            {
                return _UpdateDesc;
            }
            set
            {
                _UpdateDesc = value;
            }
        }
        public int OpenSalesOrder
        {
            get
            {
                return _OpenSalesOrder;
            }
            set
            {
                _OpenSalesOrder = value;
            }
        }
        public int OpenDelivery
        {
            get
            {
                return _OpenDelivery;
            }
            set
            {
                _OpenDelivery = value;
            }
        }
        public int OpenBilling
        {
            get
            {
                return _OpenBilling;
            }
            set
            {
                _OpenBilling = value;
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
            this.CRUpdateCode = Convert.ToString(dr["CRUpdateCode"]);
            this.UpdateType = Convert.ToString(dr["UpdateType"]);
            this.UpdateDesc = Convert.ToString(dr["UpdateDesc"]);
            this.OpenSalesOrder = Convert.ToInt32(dr["OpenSalesOrder"]);
            this.OpenDelivery = Convert.ToInt32(dr["OpenDelivery"]);
            this.OpenBilling = Convert.ToInt32(dr["OpenBilling"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}