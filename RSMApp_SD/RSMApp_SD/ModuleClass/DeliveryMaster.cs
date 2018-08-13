
//Created On :: 12, July, 2012
//Private const string ClassName = "DeliveryMaster"
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
    public class DeliveryMaster
    {
        private string _DeliveryDocCode;
        private string _DeliveryDocTypeCode;
        private string _DeliveryDate;
        private string _CustomerCode;
        private string _ShipToParty;
        private string _DeliveryStatus;
        private string _PostingDate;
        private int _TotalQty;
        private int _TotalAmt;
        private string _SAPTranID;
        private int _IsSAPPosted;
        private int _IsPGIDone;
        private int _IsEdit;
        private int _IsGIDone;
        private string _MovementCode;
        private string _MovementDate;
        private string _ShippingPointCode;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _CustName1;

        private string _BillToParty;

        public string DeliveryDocCode
        {
            get
            {
                return _DeliveryDocCode;
            }
            set
            {
                _DeliveryDocCode = value;
            }
        }
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
        public string CustomerCode
        {
            get
            {
                return _CustomerCode;
            }
            set
            {
                _CustomerCode = value;
            }
        }
        public string ShipToParty
        {
            get
            {
                return _ShipToParty;
            }
            set
            {
                _ShipToParty = value;
            }
        }
        public string DeliveryStatus
        {
            get
            {
                return _DeliveryStatus;
            }
            set
            {
                _DeliveryStatus = value;
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
        public int IsPGIDone
        {
            get
            {
                return _IsPGIDone;
            }
            set
            {
                _IsPGIDone = value;
            }
        }
        public int IsEdit
        {
            get
            {
                return _IsEdit;
            }
            set
            {
                _IsEdit = value;
            }
        }
        public int IsGIDone
        {
            get
            {
                return _IsGIDone;
            }
            set
            {
                _IsGIDone = value;
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
        public string ShippingPointCode
        {
            get
            {
                return _ShippingPointCode;
            }
            set
            {
                _ShippingPointCode = value;
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

        public string CustName1
        {
            get
            {
                return _CustName1;
            }
            set
            {
                _CustName1 = value;
            }
        }

        public string BillToParty
        {
            get
            {
                return _BillToParty;
            }
            set
            {
                _BillToParty = value;
            }
        }

        public void SetObjectInfo(DataRow dr)
        {
            this.DeliveryDocCode = Convert.ToString(dr["DeliveryDocCode"]);
            this.DeliveryDocTypeCode = Convert.ToString(dr["DeliveryDocTypeCode"]);
            this.DeliveryDate = Convert.ToString(dr["DeliveryDate"]);
            this.CustomerCode = Convert.ToString(dr["CustomerCode"]);
            this.ShipToParty = Convert.ToString(dr["ShipToParty"]);
            this.DeliveryStatus = Convert.ToString(dr["DeliveryStatus"]);
            this.PostingDate = Convert.ToString(dr["PostingDate"]);
            this.TotalQty = Convert.ToInt32(dr["TotalQty"]);
            this.TotalAmt = Convert.ToInt32(dr["TotalAmt"]);
            this.SAPTranID = Convert.ToString(dr["SAPTranID"]);
            this.IsSAPPosted = Convert.ToInt32(dr["IsSAPPosted"]);
            this.IsEdit = Convert.ToInt32(dr["IsEdit"]);
            this.IsGIDone = Convert.ToInt32(dr["IsGIDone"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.MovementCode = Convert.ToString(dr["MovementCode"]);
            this.MovementDate = Convert.ToString(dr["MovementDate"]);
            this.ShippingPointCode = Convert.ToString(dr["ShippingPointCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.CustName1 = Convert.ToString(dr["Name1"]);
            //this.BillToParty = Convert.ToString(dr["BillToParty"]);
        }
    }
}