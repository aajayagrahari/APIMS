
//Created On :: 26, November, 2012
//Private const string ClassName = "CallRepairProcess"
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
    public class CallRepairProcess
    {
        private string _CallCode;
        private int _CallItemNo;
        private string _RepairProcDocCode;
        private string _RepairProcessDocTypeCode;
        private string _RepairedBY;
        private string _CallDate;
        private string _TechReceivedDate;
        private string _RepairDate;
        private string _SerialNo;
        private string _MaterialCode;
        private string _MatGroup1Code;
        private string _ConditionCode;
        private string _CustComplaint;
        private string _WarrantyStatus;
        private string _WarrantyOn;
        private string _ServiceLevel;
        private string _OutWarrantyOn;
        private string _DefectTypeDesc;
        private string _RepairTypeDesc;
        private string _RepairProcType;
        private string _WarrantyEndDate;
        private int _EstTotal;
        private string _TechRemark;
        private int _IsRepairCompleted;
        private string _RepairCompleteDate;
        private string _PartnerCode;
        private string _StoreCode;
        private string _StockIndicator;
        private string _ToStoreCode;
        private string _ToStockIndicator;
        private string _MaterialDocTypeCode;
        private string _PGoodsMovementCode;
        private int _GMItemNo;
        private string _MRevisionCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



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
        public string RepairProcessDocTypeCode
        {
            get
            {
                return _RepairProcessDocTypeCode;
            }
            set
            {
                _RepairProcessDocTypeCode = value;
            }
        }
        public string RepairedBY
        {
            get
            {
                return _RepairedBY;
            }
            set
            {
                _RepairedBY = value;
            }
        }
        public string CallDate
        {
            get
            {
                return _CallDate;
            }
            set
            {
                _CallDate = value;
            }
        }
        public string TechReceivedDate
        {
            get
            {
                return _TechReceivedDate;
            }
            set
            {
                _TechReceivedDate = value;
            }
        }
        public string RepairDate
        {
            get
            {
                return _RepairDate;
            }
            set
            {
                _RepairDate = value;
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
        public string ConditionCode
        {
            get
            {
                return _ConditionCode;
            }
            set
            {
                _ConditionCode = value;
            }
        }
        public string CustComplaint
        {
            get
            {
                return _CustComplaint;
            }
            set
            {
                _CustComplaint = value;
            }
        }
        public string WarrantyStatus
        {
            get
            {
                return _WarrantyStatus;
            }
            set
            {
                _WarrantyStatus = value;
            }
        }
        public string WarrantyOn
        {
            get
            {
                return _WarrantyOn;
            }
            set
            {
                _WarrantyOn = value;
            }
        }
        public string ServiceLevel
        {
            get
            {
                return _ServiceLevel;
            }
            set
            {
                _ServiceLevel = value;
            }
        }
        public string OutWarrantyOn
        {
            get
            {
                return _OutWarrantyOn;
            }
            set
            {
                _OutWarrantyOn = value;
            }
        }
        public string DefectTypeDesc
        {
            get
            {
                return _DefectTypeDesc;
            }
            set
            {
                _DefectTypeDesc = value;
            }
        }
        public string RepairTypeDesc
        {
            get
            {
                return _RepairTypeDesc;
            }
            set
            {
                _RepairTypeDesc = value;
            }
        }
        public string RepairProcType
        {
            get
            {
                return _RepairProcType;
            }
            set
            {
                _RepairProcType = value;
            }
        }
        public string WarrantyEndDate
        {
            get
            {
                return _WarrantyEndDate;
            }
            set
            {
                _WarrantyEndDate = value;
            }
        }
        public int EstTotal
        {
            get
            {
                return _EstTotal;
            }
            set
            {
                _EstTotal = value;
            }
        }
        public string TechRemark
        {
            get
            {
                return _TechRemark;
            }
            set
            {
                _TechRemark = value;
            }
        }
        public int IsRepairCompleted
        {
            get
            {
                return _IsRepairCompleted;
            }
            set
            {
                _IsRepairCompleted = value;
            }
        }
        public string RepairCompleteDate
        {
            get
            {
                return _RepairCompleteDate;
            }
            set
            {
                _RepairCompleteDate = value;
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

        public string StoreCode
        {
            get
            {
                return _StoreCode;
            }
            set
            {
                _StoreCode = value;
            }
        }

        public string StockIndicator
        {
            get
            {
                return _StockIndicator;
            }
            set
            {
                _StockIndicator = value;
            }
        }

        public string ToStoreCode
        {
            get
            {
                return _ToStoreCode;
            }
            set
            {
                _ToStoreCode = value;
            }
        }

        public string ToStockIndicator
        {
            get
            {
                return _ToStockIndicator;
            }
            set
            {
                _ToStockIndicator = value;
            }
        }

        public string MaterialDocTypeCode
        {
            get
            {
                return _MaterialDocTypeCode;
            }
            set
            {
                _MaterialDocTypeCode = value;
            }
        }

        public string PGoodsMovementCode
        {
            get
            {
                return _PGoodsMovementCode;
            }
            set
            {
                _PGoodsMovementCode = value;
            }
        }

        public int GMItemNo
        {
            get
            {
                return _GMItemNo;
            }
            set
            {
                _GMItemNo = value;
            }
        }

        public string MRevisionCode
        {
            get
            {
                return _MRevisionCode;
            }
            set
            {
                _MRevisionCode = value;
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
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.CallItemNo = Convert.ToInt32(dr["CallItemNo"]);
            this.RepairProcDocCode = Convert.ToString(dr["RepairProcDocCode"]);
            this.RepairProcessDocTypeCode = Convert.ToString(dr["RepairProcessDocTypeCode"]);
            this.RepairedBY = Convert.ToString(dr["RepairedBY"]);
            this.CallDate = Convert.ToString(dr["CallDate"]);
            this.TechReceivedDate = Convert.ToString(dr["TechReceivedDate"]);
            this.RepairDate = Convert.ToDateTime(dr["RepairDate"]).ToString("yyyy-MM-dd");
            this.SerialNo = Convert.ToString(dr["SerialNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.ConditionCode = Convert.ToString(dr["ConditionCode"]);
            this.CustComplaint = Convert.ToString(dr["CustComplaint"]);
            this.WarrantyStatus = Convert.ToString(dr["WarrantyStatus"]);
            this.WarrantyOn = Convert.ToString(dr["WarrantyOn"]);
            this.ServiceLevel = Convert.ToString(dr["ServiceLevel"]);
            this.OutWarrantyOn = Convert.ToString(dr["OutWarrantyOn"]);
            this.DefectTypeDesc = Convert.ToString(dr["DefectTypeDesc"]);
            this.RepairTypeDesc = Convert.ToString(dr["RepairTypeDesc"]);
            this.RepairProcType = Convert.ToString(dr["RepairProcType"]);
            this.WarrantyEndDate = Convert.ToString(dr["WarrantyEndDate"]);
            this.EstTotal = Convert.ToInt32(dr["EstTotal"]);
            this.TechRemark = Convert.ToString(dr["TechRemark"]);
            this.IsRepairCompleted = Convert.ToInt32(dr["IsRepairCompleted"]);
            this.RepairCompleteDate = Convert.ToString(dr["RepairCompleteDate"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);
            this.ToStoreCode = Convert.ToString(dr["ToStoreCode"]);
            this.ToStockIndicator = Convert.ToString(dr["ToStockIndicator"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);

            this.PGoodsMovementCode = Convert.ToString(dr["PGoodsMovementCode"]);
            this.GMItemNo = Convert.ToInt32(dr["GMItemNo"]);
            this.MRevisionCode = Convert.ToString(dr["MRevisionCode"]);

            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}