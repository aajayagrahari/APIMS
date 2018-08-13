
//Created On :: 19, October, 2012
//Private const string ClassName = "InBDeliveryMaster"
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
    public class InBDeliveryMaster
    {
        private string _InBDeliveryDocCode;
        private string _InBDeliveryDocTypeCode;
        private string _ReceiptDate;
        private string _VendorCode;
        private string _SourcePlantCode;
        private string _ReceiptStatus;
        private string _PostingDate;
        private int _TotalQty;
        private int _TotalAmt;
        private string _SAPTranID;
        private int _IsSAPPosted;
        private int _IsPGRDone;
        private string _MovementCode;
        private string _MovementDate;
        private string _PlantCode;
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
        public string InBDeliveryDocTypeCode
        {
            get
            {
                return _InBDeliveryDocTypeCode;
            }
            set
            {
                _InBDeliveryDocTypeCode = value;
            }
        }
        public string ReceiptDate
        {
            get
            {
                return _ReceiptDate;
            }
            set
            {
                _ReceiptDate = value;
            }
        }
        public string VendorCode
        {
            get
            {
                return _VendorCode;
            }
            set
            {
                _VendorCode = value;
            }
        }
        public string SourcePlantCode
        {
            get
            {
                return _SourcePlantCode;
            }
            set
            {
                _SourcePlantCode = value;
            }
        }
        public string ReceiptStatus
        {
            get
            {
                return _ReceiptStatus;
            }
            set
            {
                _ReceiptStatus = value;
            }
        }
        public string PostingDate
        {
            get
            {
                return _PostingDate;
            }
            set
            {
                _PostingDate = value;
            }
        }
        public int TotalQty
        {
            get
            {
                return _TotalQty;
            }
            set
            {
                _TotalQty = value;
            }
        }
        public int TotalAmt
        {
            get
            {
                return _TotalAmt;
            }
            set
            {
                _TotalAmt = value;
            }
        }
        public string SAPTranID
        {
            get
            {
                return _SAPTranID;
            }
            set
            {
                _SAPTranID = value;
            }
        }
        public int IsSAPPosted
        {
            get
            {
                return _IsSAPPosted;
            }
            set
            {
                _IsSAPPosted = value;
            }
        }
        public int IsPGRDone
        {
            get
            {
                return _IsPGRDone;
            }
            set
            {
                _IsPGRDone = value;
            }
        }
        public string MovementCode
        {
            get
            {
                return _MovementCode;
            }
            set
            {
                _MovementCode = value;
            }
        }
        public string MovementDate
        {
            get
            {
                return _MovementDate;
            }
            set
            {
                _MovementDate = value;
            }
        }
        public string PlantCode
        {
            get
            {
                return _PlantCode;
            }
            set
            {
                _PlantCode = value;
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
            this.InBDeliveryDocTypeCode = Convert.ToString(dr["InBDeliveryDocTypeCode"]);
            this.ReceiptDate = Convert.ToString(dr["ReceiptDate"]);
            this.VendorCode = Convert.ToString(dr["VendorCode"]);
            this.SourcePlantCode = Convert.ToString(dr["SourcePlantCode"]);
            this.ReceiptStatus = Convert.ToString(dr["ReceiptStatus"]);
            this.PostingDate = Convert.ToString(dr["PostingDate"]);
            this.TotalQty = Convert.ToInt32(dr["TotalQty"]);
            this.TotalAmt = Convert.ToInt32(dr["TotalAmt"]);
            this.SAPTranID = Convert.ToString(dr["SAPTranID"]);
            this.IsSAPPosted = Convert.ToInt32(dr["IsSAPPosted"]);
            this.IsPGRDone = Convert.ToInt32(dr["IsPGRDone"]);
            this.MovementCode = Convert.ToString(dr["MovementCode"]);
            this.MovementDate = Convert.ToString(dr["MovementDate"]);
            this.PlantCode = Convert.ToString(dr["PlantCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}