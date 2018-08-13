
//Created On :: 18, October, 2012
//Private const string ClassName = "CallLogin"
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
    public class CallLogin
    {
        private string _CallCode;
        private string _RepairDocTypeCode;
        private string _CallDate;
        private string _CallAttendedBY;
        private string _ReceivingDate;
        private string _CallReceivedFrm;
        private string _CallRecName;
        private string _CallRecAddress1;
        private string _CallRecAddress2;
        private string _CallRecPhone;
        private string _CallRecMobile;
        private string _CallRecEmail;
        private string _CallRecGender;
        private string _CallRecCountryCode;
        private string _CallRecStateCode;
        private string _CallRecCity;
        private string _CallStatus;
        private int _IsCallClosed;
        private int _IsInvGen;
        private string _PartnerCode;
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
        public string RepairDocTypeCode
        {
            get
            {
                return _RepairDocTypeCode;
            }
            set
            {
                _RepairDocTypeCode = value;
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
        public string CallAttendedBY
        {
            get
            {
                return _CallAttendedBY;
            }
            set
            {
                _CallAttendedBY = value;
            }
        }
        public string ReceivingDate
        {
            get
            {
                return _ReceivingDate;
            }
            set
            {
                _ReceivingDate = value;
            }
        }
        public string CallReceivedFrm
        {
            get
            {
                return _CallReceivedFrm;
            }
            set
            {
                _CallReceivedFrm = value;
            }
        }
        public string CallRecName
        {
            get
            {
                return _CallRecName;
            }
            set
            {
                _CallRecName = value;
            }
        }
        public string CallRecAddress1
        {
            get
            {
                return _CallRecAddress1;
            }
            set
            {
                _CallRecAddress1 = value;
            }
        }
        public string CallRecAddress2
        {
            get
            {
                return _CallRecAddress2;
            }
            set
            {
                _CallRecAddress2 = value;
            }
        }
        public string CallRecPhone
        {
            get
            {
                return _CallRecPhone;
            }
            set
            {
                _CallRecPhone = value;
            }
        }
        public string CallRecMobile
        {
            get
            {
                return _CallRecMobile;
            }
            set
            {
                _CallRecMobile = value;
            }
        }
        public string CallRecEmail
        {
            get
            {
                return _CallRecEmail;
            }
            set
            {
                _CallRecEmail = value;
            }
        }
        public string CallRecGender
        {
            get
            {
                return _CallRecGender;
            }
            set
            {
                _CallRecGender = value;
            }
        }
        public string CallRecCountryCode
        {
            get
            {
                return _CallRecCountryCode;
            }
            set
            {
                _CallRecCountryCode = value;
            }
        }
        public string CallRecStateCode
        {
            get
            {
                return _CallRecStateCode;
            }
            set
            {
                _CallRecStateCode = value;
            }
        }
        public string CallRecCity
        {
            get
            {
                return _CallRecCity;
            }
            set
            {
                _CallRecCity = value;
            }
        }
        public string CallStatus
        {
            get
            {
                return _CallStatus;
            }
            set
            {
                _CallStatus = value;
            }
        }
        public int IsCallClosed
        {
            get
            {
                return _IsCallClosed;
            }
            set
            {
                _IsCallClosed = value;
            }
        }
        public int IsInvGen
        {
            get
            {
                return _IsInvGen;
            }
            set
            {
                _IsInvGen = value;
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
            this.RepairDocTypeCode = Convert.ToString(dr["RepairDocTypeCode"]);
            this.CallDate = Convert.ToString(dr["CallDate"]);
            this.CallAttendedBY = Convert.ToString(dr["CallAttendedBY"]);
            this.ReceivingDate = Convert.ToString(dr["ReceivingDate"]);
            this.CallReceivedFrm = Convert.ToString(dr["CallReceivedFrm"]);
            this.CallRecName = Convert.ToString(dr["CallRecName"]);
            this.CallRecAddress1 = Convert.ToString(dr["CallRecAddress1"]);
            this.CallRecAddress2 = Convert.ToString(dr["CallRecAddress2"]);
            this.CallRecPhone = Convert.ToString(dr["CallRecPhone"]);
            this.CallRecMobile = Convert.ToString(dr["CallRecMobile"]);
            this.CallRecEmail = Convert.ToString(dr["CallRecEmail"]);
            this.CallRecGender = Convert.ToString(dr["CallRecGender"]);
            this.CallRecCountryCode = Convert.ToString(dr["CallRecCountryCode"]);
            this.CallRecStateCode = Convert.ToString(dr["CallRecStateCode"]);
            this.CallRecCity = Convert.ToString(dr["CallRecCity"]);
            this.CallStatus = Convert.ToString(dr["CallStatus"]);
            this.IsCallClosed = Convert.ToInt32(dr["IsCallClosed"]);
            this.IsInvGen = Convert.ToInt32(dr["IsInvGen"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}