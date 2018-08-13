
//Created On :: 01, June, 2012
//Private const string ClassName = "SOSchedule"
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
    public class SOSchedule
    {
        private string _SOSItemNo;
        private string _SODocCode;
        private string _ItemNo;
        private string _ClientCode;
        private string _DeliveryDate;
        private int _SchQuantity;
        private int _DeliveryQuantity;
        private int _RoundedQty;
        private int _ConfirmedQty;
        private string _SchStatus;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string SOSItemNo
        {
            get
            {
                return _SOSItemNo;
            }
            set
            {
                _SOSItemNo = value;
            }
        }

        public string SODocCode
        {
            get
            {
                return _SODocCode;
            }
            set
            {
                _SODocCode = value;
            }
        }
        public string ItemNo
        {
            get
            {
                return _ItemNo;
            }
            set
            {
                _ItemNo = value;
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

        public string DeliveryDate
        {
            get
            {
                return _DeliveryDate;
            }
            set
            {
                _DeliveryDate = value;
            }
        }
        public int SchQuantity
        {
            get
            {
                return _SchQuantity;
            }
            set
            {
                _SchQuantity = value;
            }
        }
        public int DeliveryQuantity
        {
            get
            {
                return _DeliveryQuantity;
            }
            set
            {
                _DeliveryQuantity = value;
            }
        }


        public int RoundedQty
        {
            get
            {
                return _RoundedQty;
            }
            set
            {
                _RoundedQty = value;
            }
        }

        public int ConfirmedQty
        {
            get
            {
                return _ConfirmedQty;
            }
            set
            {
                _ConfirmedQty = value;
            }
        }


        public string SchStatus
        {
            get
            {
                return _SchStatus;
            }
            set
            {
                _SchStatus = value;
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
            this.SOSItemNo = Convert.ToString(dr["SOSItemNo"]);
            this.SODocCode = Convert.ToString(dr["SODocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.DeliveryDate = Convert.ToString(dr["DeliveryDate"]);
            this.SchQuantity = Convert.ToInt32(dr["SchQuantity"]);
            this.RoundedQty = Convert.ToInt32(dr["RoundedQty"]);
            this.ConfirmedQty = Convert.ToInt32(dr["ConfirmedQty"]);
            this.DeliveryQuantity = Convert.ToInt32(dr["DeliveryQuantity"]);
            this.SchStatus = Convert.ToString(dr["SchStatus"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}