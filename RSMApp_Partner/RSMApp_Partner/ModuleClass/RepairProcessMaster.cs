
//Created On :: 06, November, 2012
//Private const string ClassName = "RepairProcessMaster"
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
    public class RepairProcessMaster
    {
        private string _RepairProcessCode;
        private string _RepairProcessName;
        private int _IsAssignToTechnician;
        private int _IsCallClosed;
        private int _IsRepairCompleted;
        private int _IsApproved;
        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;



        public string RepairProcessCode
        {
            get
            {
                return _RepairProcessCode;
            }
            set
            {
                _RepairProcessCode = value;
            }
        }
        public string RepairProcessName
        {
            get
            {
                return _RepairProcessName;
            }
            set
            {
                _RepairProcessName = value;
            }
        }

        public int IsAssignToTechnician
        {
            get
            {
                return _IsAssignToTechnician;
            }
            set
            {
                _IsAssignToTechnician = value;
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

        public int IsApproved
        {
            get
            {
                return _IsApproved;
            }
            set
            {
                _IsApproved = value;
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
            this.RepairProcessCode = Convert.ToString(dr["RepairProcessCode"]);
            this.RepairProcessName = Convert.ToString(dr["RepairProcessName"]);
            this.IsAssignToTechnician = Convert.ToInt32(dr["IsAssignToTechnician"]);
            this.IsCallClosed = Convert.ToInt32(dr["IsCallClosed"]);
            this.IsRepairCompleted = Convert.ToInt32(dr["IsRepairCompleted"]);
            this.IsApproved = Convert.ToInt32(dr["IsApproved"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.CreatedDate = Convert.ToString(dr["CreatedDate"]);
            this.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

        }
    }
}