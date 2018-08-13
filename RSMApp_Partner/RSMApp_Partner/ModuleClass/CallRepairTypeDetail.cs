
//Created On :: 21, November, 2012
//Private const string ClassName = "CallRepairTypeDetail"
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
    public class CallRepairTypeDetail
    {
        private string _CallCode;
        private int _ItemNo;
        private int _RPItemNo;
        private string _RepairTypeCode;
        private string _RepairTypeDesc;
        private string _DefectTypeCode;
        private string _ServiceLevel;
        private string _RMarkInd;
        private string _RepairProcDocCode;
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
        public int ItemNo
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
        public int RPItemNo
        {
            get
            {
                return _RPItemNo;
            }
            set
            {
                _RPItemNo = value;
            }
        }
        public string RepairTypeCode
        {
            get
            {
                return _RepairTypeCode;
            }
            set
            {
                _RepairTypeCode = value;
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

        public string DefectTypeCode
        {
            get
            {
                return _DefectTypeCode;
            }
            set
            {
                _DefectTypeCode = value;
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

        public string RMarkInd
        {
            get
            {
                return _RMarkInd;
            }
            set
            {
                _RMarkInd = value;
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
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.RPItemNo = Convert.ToInt32(dr["RPItemNo"]);
            this.RepairTypeCode = Convert.ToString(dr["RepairTypeCode"]);
            this.RepairTypeDesc = Convert.ToString(dr["RepairTypeDesc"]);
            this.DefectTypeCode = Convert.ToString(dr["DefectTypeCode"]);
            this.ServiceLevel = Convert.ToString(dr["ServiceLevel"]);
            this.RMarkInd = Convert.ToString(dr["RMarkInd"]);
            this.RepairProcDocCode = Convert.ToString(dr["RepairProcDocCode"]);
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