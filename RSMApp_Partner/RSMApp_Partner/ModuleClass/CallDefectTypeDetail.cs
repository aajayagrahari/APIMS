
//Created On :: 20, October, 2012
//Private const string ClassName = "CallDefectTypeDetail"
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
    public class CallDefectTypeDetail
    {
        private string _CallCode;
        private int _ItemNo;
        private int _DfItemNo;
        private string _DefectTypeCode;
        private string _DefectTypeDesc;
        private string _DMarkInd;
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
        public int DfItemNo
        {
            get
            {
                return _DfItemNo;
            }
            set
            {
                _DfItemNo = value;
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

        public string DMarkInd
        {
            get
            {
                return _DMarkInd;
            }
            set
            {
                _DMarkInd = value;
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
            this.DfItemNo = Convert.ToInt32(dr["DfItemNo"]);
            this.DefectTypeCode = Convert.ToString(dr["DefectTypeCode"]);
            this.DefectTypeDesc = Convert.ToString(dr["DefectTypeDesc"]);
            this.DMarkInd = Convert.ToString(dr["DMarkInd"]);
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