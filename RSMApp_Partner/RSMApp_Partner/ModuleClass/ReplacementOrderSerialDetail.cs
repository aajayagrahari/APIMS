
//Created On :: 07, January, 2013
//Private const string ClassName = "ReplacementOrderSerialDetail"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    [Serializable]
    public class ReplacementOrderSerialDetail
    {
        private string _RepOrderCode;
        private int _RepOrderItemNo;
        private string _SerialNo1;
        private string _SerialNo2;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _PartnerCode;
        private string _PartnerEmployeeCode;
        private string _ToPartnerCode;
        private int _OrderQty;
        private string _UOMCode;
        private int _ReceivedQty;
        private string _RepOrderStatus;
        private string _IssueDocCode;
        private int _IssueDocItemNo;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string RepOrderCode
        {
            get
            {
                return _RepOrderCode;
            }
            set
            {
                _RepOrderCode = value;
            }
        }
        public int RepOrderItemNo
        {
            get
            {
                return _RepOrderItemNo;
            }
            set
            {
                _RepOrderItemNo = value;
            }
        }
        public string SerialNo1
        {
            get
            {
                return _SerialNo1;
            }
            set
            {
                _SerialNo1 = value;
            }
        }
        public string SerialNo2
        {
            get
            {
                return _SerialNo2;
            }
            set
            {
                _SerialNo2 = value;
            }
        }
        public string MaterialCode
        {
            get
            {
                return _MaterialCode;
            }
            set
            {
                _MaterialCode = value;
            }
        }
        public string MatGroup1Code
        {
            get
            {
                return _MatGroup1Code;
            }
            set
            {
                _MatGroup1Code = value;
            }
        }
        public string PartnerCode
        {
            get
            {
                return _PartnerCode;
            }
            set
            {
                _PartnerCode = value;
            }
        }
        public string PartnerEmployeeCode
        {
            get
            {
                return _PartnerEmployeeCode;
            }
            set
            {
                _PartnerEmployeeCode = value;
            }
        }
        public string ToPartnerCode
        {
            get
            {
                return _ToPartnerCode;
            }
            set
            {
                _ToPartnerCode = value;
            }
        }
        public int OrderQty
        {
            get
            {
                return _OrderQty;
            }
            set
            {
                _OrderQty = value;
            }
        }
        public string UOMCode
        {
            get
            {
                return _UOMCode;
            }
            set
            {
                _UOMCode = value;
            }
        }
        public int ReceivedQty
        {
            get
            {
                return _ReceivedQty;
            }
            set
            {
                _ReceivedQty = value;
            }
        }
        public string RepOrderStatus
        {
            get
            {
                return _RepOrderStatus;
            }
            set
            {
                _RepOrderStatus = value;
            }
        }
        public string IssueDocCode
        {
            get
            {
                return _IssueDocCode;
            }
            set
            {
                _IssueDocCode = value;
            }
        }
        public int IssueDocItemNo
        {
            get
            {
                return _IssueDocItemNo;
            }
            set
            {
                _IssueDocItemNo = value;
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
            this.RepOrderCode = Convert.ToString(dr["RepOrderCode"]);
            this.RepOrderItemNo = Convert.ToInt32(dr["RepOrderItemNo"]);
            this.SerialNo1 = Convert.ToString(dr["SerialNo1"]);
            this.SerialNo2 = Convert.ToString(dr["SerialNo2"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.ToPartnerCode = Convert.ToString(dr["ToPartnerCode"]);
            this.OrderQty = Convert.ToInt32(dr["OrderQty"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.ReceivedQty = Convert.ToInt32(dr["ReceivedQty"]);
            this.RepOrderStatus = Convert.ToString(dr["RepOrderStatus"]);
            this.IssueDocCode = Convert.ToString(dr["IssueDocCode"]);
            this.IssueDocItemNo = Convert.ToInt32(dr["IssueDocItemNo"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}