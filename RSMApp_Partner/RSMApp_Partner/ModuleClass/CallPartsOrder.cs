
//Created On :: 28, November, 2012
//Private const string ClassName = "CallPartsOrder"
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
    public class CallPartsOrder
    {
        private string _CallCode;
        private int _CallItemNo;
        private string _RepairProcDocCode;
        private int _POItemNo;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _PartnerCode;
        private string _PartnerEmployeeCode;
        private int _OrderQty;
        private string _UOMCode;
        private int _ReceivedQty;
        private int _IsOrdered;
        private string _OrderStatus;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        public string _MatDesc;
        public string _MatGroup1Desc;

        public string CallCode
        {
            get
            {
                return _CallCode;
            }
            set
            {
                _CallCode = value;
            }
        }
        public int CallItemNo
        {
            get
            {
                return _CallItemNo;
            }
            set
            {
                _CallItemNo = value;
            }
        }
        public string RepairProcDocCode
        {
            get
            {
                return _RepairProcDocCode;
            }
            set
            {
                _RepairProcDocCode = value;
            }
        }
        public int POItemNo
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
        public int IsOrdered
        {
            get
            {
                return _IsOrdered;
            }
            set
            {
                _IsOrdered = value;
            }
        }
        public string OrderStatus
        {
            get
            {
                return _OrderStatus;
            }
            set
            {
                _OrderStatus = value;
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

        public string MatDesc
        {
            get
            {
                return _MatDesc;
            }
            set
            {
                _MatDesc = value;
            }
        }

        public string MatGroup1Desc
        {
            get
            {
                return _MatGroup1Desc;
            }
            set
            {
                _MatGroup1Desc = value;
            }

        }


        public void SetObjectInfo(DataRow dr)
        {
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.CallItemNo = Convert.ToInt32(dr["CallItemNo"]);
            this.RepairProcDocCode = Convert.ToString(dr["RepairProcDocCode"]);
            this.POItemNo = Convert.ToInt32(dr["POItemNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.PartnerEmployeeCode = Convert.ToString(dr["PartnerEmployeeCode"]);
            this.OrderQty = Convert.ToInt32(dr["OrderQty"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.ReceivedQty = Convert.ToInt32(dr["ReceivedQty"]);
            this.IsOrdered = Convert.ToInt32(dr["IsOrdered"]);
            this.OrderStatus = Convert.ToString(dr["OrderStatus"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.MatGroup1Desc = Convert.ToString(dr["MatGroup1Desc"]);


        }
    }
}