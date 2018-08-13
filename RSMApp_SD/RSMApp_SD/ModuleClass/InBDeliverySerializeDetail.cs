
//Created On :: 19, October, 2012
//Private const string ClassName = "InBDeliverySerializeDetail"
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
    public class InBDeliverySerializeDetail
    {
        private string _InBDeliveryDocCode;
        private string _ItemNo;
        private string _POItemNo;
        private string _DLItemNo;
        private string _SerialNo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string InBDeliveryDocCode
        {
            get
            {
                return _InBDeliveryDocCode;
            }
            set
            {
                _InBDeliveryDocCode = value;
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
        public string POItemNo
        {
            get
            {
                return _POItemNo;
            }
            set
            {
                _POItemNo = value;
            }
        }
        public string DLItemNo
        {
            get
            {
                return _DLItemNo;
            }
            set
            {
                _DLItemNo = value;
            }
        }
        public string SerialNo
        {
            get
            {
                return _SerialNo;
            }
            set
            {
                _SerialNo = value;
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
            this.InBDeliveryDocCode = Convert.ToString(dr["InBDeliveryDocCode"]);
            this.ItemNo = Convert.ToString(dr["ItemNo"]);
            this.POItemNo = Convert.ToString(dr["POItemNo"]);
            this.DLItemNo = Convert.ToString(dr["DLItemNo"]);
            this.SerialNo = Convert.ToString(dr["SerialNo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}