
//Created On :: 16, May, 2012
//Private const string ClassName = "MatMovementType"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    [Serializable]
    public class MatMovementType
    {
        private string _MatMovementCode;
        private string _MatMovementDesc;
        private string _OperationType;
        private int _IsMAPUpdated;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string MatMovementCode
        {
            get
            {
                return _MatMovementCode;
            }
            set
            {
                _MatMovementCode = value;
            }
        }
        public string MatMovementDesc
        {
            get
            {
                return _MatMovementDesc;
            }
            set
            {
                _MatMovementDesc = value;
            }
        }
        public string OperationType
        {
            get
            {
                return _OperationType;
            }
            set
            {
                _OperationType = value;
            }
        }
        public int IsMAPUpdated
        {
            get
            {
                return _IsMAPUpdated;
            }
            set
            {
                _IsMAPUpdated = value;
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
            this.MatMovementCode = Convert.ToString(dr["MatMovementCode"]);
            this.MatMovementDesc = Convert.ToString(dr["MatMovementDesc"]);
            this.OperationType = Convert.ToString(dr["OperationType"]);
            this.IsMAPUpdated = Convert.ToInt32(dr["IsMAPUpdated"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}