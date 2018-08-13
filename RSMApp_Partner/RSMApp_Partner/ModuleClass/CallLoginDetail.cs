
//Created On :: 18, October, 2012
//Private const string ClassName = "CallLoginDetail"
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    
    [Serializable]
    public class CallLoginDetail
    {
        private string _CallCode;
        private int _ItemNo;
        private string _SerialNo;
        private string _MaterialCode;
        private string _MaterialTypeCode;
        private string _MatGroup1Code;
        private string _WarrantyOn;
        private string _WarrantyEndDate;
        private string _ConditionCode;
        private string _CustComplaint;
        private string _WarrantyStatus;
        private string _OutWarReason;
        private string _CallTypeCode;
        private string _CallFrom;
        private string _CustInvoiceDate;
        private string _CustInvoiceNo;
        private string _CustName;
        private string _CustAddress1;
        private string _CustAddress2;
        private string _CustPhone;
        private string _CustMobile;
        private string _CustEmail;
        private string _CustGender;
        private string _CustCountryCode;
        private string _CustStateCode;
        private string _CustCity;
        private int _IsAssignToTechnician;
        private string _AssignTechNarration;
        private int _IsApproved;
        private int _IsCallClosed;
        private DateTime _CallCloseDate;
        private int _IsGoodsRec;
        private string _GRStatus;
        private DateTime _ReceivedDate;
        private int _Quantity;
        private string _UOMCode;
        private string _PartnerCode;
        private string _HLMaterialCode;
        private int _HLItemNo;
        private string _Narration;
        private string _RepairStatusCode;
        private string _DefectTypeDesc;
        private double _EstTotal;
        private string _EstAppStatus;
        private DateTime _EstAppDate;
        
        private string _AdvRefDocCode;
        private int _AdvRefDocItemNo;
        private string _AdvRecRefDocCode;
        private int _AdvRecRefDocItemNo;
        private string _MaterialDocTypeCode;
        private string _StoreCode;
        private string _StockIndicator;

        private string _ItemsReceived;
        private int _IsCretificateIssued;
        private DateTime _CretificateIssueDate;

        private string _ClientCode;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _CreatedDate;
        private string _ModifiedDate;
        private int _IsDeleted;

        private string _MatDesc;
        private string _MaterialTypeDesc;
        private string _MatGroup1Desc;
        private string _ConditionName;
        private string _CallTypeDesc;

        private string _OutWarrantyOn;


        private string _RepairDocTypeCode;
        private string _MRevisionCode;
        private int _IsRepairable;   


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
        public string MaterialTypeCode
        {
            get
            {
                return _MaterialTypeCode;
            }
            set
            {
                _MaterialTypeCode = value;
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
        public string OutWarReason
        {
            get
            {
                return _OutWarReason;
            }
            set
            {
                _OutWarReason = value;
            }
        }
        public string CallTypeCode
        {
            get
            {
                return _CallTypeCode;
            }
            set
            {
                _CallTypeCode = value;
            }
        }
        public string CallFrom
        {
            get
            {
                return _CallFrom;
            }
            set
            {
                _CallFrom = value;
            }
        }

        public string CustInvoiceDate
        {
            get
            {
                return _CustInvoiceDate;
            }
            set
            {
                _CustInvoiceDate = value;
            }
        }

        public string CustInvoiceNo
        {
            get
            {
                return _CustInvoiceNo;
            }
            set
            {
                _CustInvoiceNo = value;
            }
        }

        public string CustName
        {
            get
            {
                return _CustName;
            }
            set
            {
                _CustName = value;
            }
        }
        public string CustAddress1
        {
            get
            {
                return _CustAddress1;
            }
            set
            {
                _CustAddress1 = value;
            }
        }
        public string CustAddress2
        {
            get
            {
                return _CustAddress2;
            }
            set
            {
                _CustAddress2 = value;
            }
        }
        public string CustPhone
        {
            get
            {
                return _CustPhone;
            }
            set
            {
                _CustPhone = value;
            }
        }
        public string CustMobile
        {
            get
            {
                return _CustMobile;
            }
            set
            {
                _CustMobile = value;
            }
        }
        public string CustEmail
        {
            get
            {
                return _CustEmail;
            }
            set
            {
                _CustEmail = value;
            }
        }
        public string CustGender
        {
            get
            {
                return _CustGender;
            }
            set
            {
                _CustGender = value;
            }
        }
        public string CustCountryCode
        {
            get
            {
                return _CustCountryCode;
            }
            set
            {
                _CustCountryCode = value;
            }
        }
        public string CustStateCode
        {
            get
            {
                return _CustStateCode;
            }
            set
            {
                _CustStateCode = value;
            }
        }
        public string CustCity
        {
            get
            {
                return _CustCity;
            }
            set
            {
                _CustCity = value;
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

        public string AssignTechNarration
        {
            get
            {
                return _AssignTechNarration;
               
            }
            set
            {
                _AssignTechNarration = value;
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

        public DateTime CallCloseDate
        {
            get
            {
                return _CallCloseDate;
            }
            set
            {
                _CallCloseDate = value;
            }
        }

        public int IsGoodsRec
        {
            get
            {
                return _IsGoodsRec;
            }
            set
            {
                _IsGoodsRec = value;
            }
        }

        public string GRStatus
        {
            get
            {
                return _GRStatus;
            }
            set
            {
                _GRStatus = value;
            }
        }

        public DateTime ReceivedDate
        {
            get
            {
                return _ReceivedDate;
            }
            set
            {
                _ReceivedDate = value;
            }
        }

        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
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

         public string HLMaterialCode
        {
            get
            {
                return _HLMaterialCode;
            }
            set
            {
                _HLMaterialCode = value;
            }
        }

        public int HLItemNo
        {
            get
            {
                return _HLItemNo;
            }
            set
            {
                _HLItemNo = value;
            }
        }


        public string Narration
        {
            get
            {
                return _Narration;
            }
            set
            {
                _Narration = value;
            }
        }

        public string RepairStatusCode
        {
            get
            {
                return _RepairStatusCode;
            }
            set
            {
                _RepairStatusCode = value;
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

        public double EstTotal
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

        public string EstAppStatus
        {
            get
            {
                return _EstAppStatus;
            }
            set
            {
                _EstAppStatus = value;
            }
        }

        public DateTime EstAppDate
        {
            get
            {
                return _EstAppDate;
            }
            set
            {
                _EstAppDate = value;
            }
        }



        public string AdvRefDocCode
        {
            get
            {
                return _AdvRefDocCode;
            }
            set
            {
                _AdvRefDocCode = value;
            }
        }

        public int AdvRefDocItemNo
        {
            get
            {
                return _AdvRefDocItemNo;
            }
            set
            {
                _AdvRefDocItemNo = value;
            }
        }

        public string AdvRecRefDocCode
        {
            get
            {
                return _AdvRecRefDocCode;
            }
            set
            {
                _AdvRecRefDocCode = value;
            }
        }

        public int AdvRecRefDocItemNo
        {
            get
            {
                return _AdvRecRefDocItemNo;
            }
            set
            {
                _AdvRecRefDocItemNo = value;
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


        public string ItemsReceived
        {
            get
            {
                return _ItemsReceived;
            }
            set
            {
                _ItemsReceived = value;
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

        public int IsCretificateIssued
        {
            get
            {
                return _IsCretificateIssued;
            }
            set
            {
                _IsCretificateIssued = value;
            }
        }


        public DateTime CretificateIssueDate
        {
            get
            {
                return _CretificateIssueDate;
            }
            set
            {
                _CretificateIssueDate = value;
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

        public string MaterialTypeDesc
        {
            get
            {
                return _MaterialTypeDesc;
            }
            set
            {
                _MaterialTypeDesc = value;
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

        public string CallTypeDesc
        {
            get
            {
                return _CallTypeDesc;
            }
            set
            {
                _CallTypeDesc = value;
            }
        }

        public string ConditionName
        {
            get
            {
                return _ConditionName;
            }
            set
            {
                _ConditionName = value;
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

        public int IsRepairable
        {
            get
            {
                return _IsRepairable;
            }
            set
            {
                _IsRepairable = value;
            }
        }


        public void SetObjectInfo(DataRow dr)
        {
            this.CallCode = Convert.ToString(dr["CallCode"]);
            this.ItemNo = Convert.ToInt32(dr["ItemNo"]);
            this.SerialNo = Convert.ToString(dr["SerialNo"]);
            this.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            this.MaterialTypeCode = Convert.ToString(dr["MaterialTypeCode"]);
            this.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]);
            this.WarrantyOn = Convert.ToString(dr["WarrantyOn"]);
            this.WarrantyEndDate = Convert.ToDateTime(dr["WarrantyEndDate"]).ToString("yyyy-MM-dd");
            this.ConditionCode = Convert.ToString(dr["ConditionCode"]);
            this.CustComplaint = Convert.ToString(dr["CustComplaint"]);
            this.WarrantyStatus = Convert.ToString(dr["WarrantyStatus"]);
            this.OutWarReason = Convert.ToString(dr["OutWarReason"]);
            this.CallTypeCode = Convert.ToString(dr["CallTypeCode"]);
            this.CallFrom = Convert.ToString(dr["CallFrom"]);
            this.CustInvoiceDate = Convert.ToString(dr["CustInvoiceDate"]);
            this.CustInvoiceNo = Convert.ToString(dr["CustInvoiceNo"]);
            this.CustName = Convert.ToString(dr["CustName"]);
            this.CustAddress1 = Convert.ToString(dr["CustAddress1"]);
            this.CustAddress2 = Convert.ToString(dr["CustAddress2"]);
            this.CustPhone = Convert.ToString(dr["CustPhone"]);
            this.CustMobile = Convert.ToString(dr["CustMobile"]);
            this.CustEmail = Convert.ToString(dr["CustEmail"]);
            this.CustGender = Convert.ToString(dr["CustGender"]);
            this.CustCountryCode = Convert.ToString(dr["CustCountryCode"]);
            this.CustStateCode = Convert.ToString(dr["CustStateCode"]);
            this.CustCity = Convert.ToString(dr["CustCity"]);
            this.IsAssignToTechnician = Convert.ToInt32(dr["IsAssignToTechnician"]);
            this.AssignTechNarration = Convert.ToString(dr["AssignTechNarration"]);
            this.IsApproved = Convert.ToInt32(dr["IsApproved"]);
            this.IsCallClosed = Convert.ToInt32(dr["IsCallClosed"]);
            this.CallCloseDate = Convert.ToDateTime(dr["CallCloseDate"]);
            this.IsGoodsRec = Convert.ToInt32(dr["IsGoodsRec"]);
            this.GRStatus = Convert.ToString(dr["GRStatus"]);
            this.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"]);
          
            this.Quantity = Convert.ToInt32(dr["Quantity"]);
            this.UOMCode = Convert.ToString(dr["UOMCode"]);
            this.PartnerCode = Convert.ToString(dr["PartnerCode"]);
            this.HLMaterialCode = Convert.ToString(dr["HLMaterialCode"]);
            this.HLItemNo = Convert.ToInt32(dr["HLItemNo"]);
            this.Narration = Convert.ToString(dr["Narration"]);
            this.RepairStatusCode = Convert.ToString(dr["RepairStatusCode"]);
            this.DefectTypeDesc = Convert.ToString(dr["DefectTypeDesc"]);
            this.EstTotal = Convert.ToDouble(dr["EstTotal"]);
            this.EstAppStatus = Convert.ToString(dr["EstAppStatus"]);
            this.EstAppDate = Convert.ToDateTime(dr["EstAppDate"]);
            this.AdvRefDocCode = Convert.ToString(dr["AdvRefDocCode"]);
            this.AdvRefDocItemNo = Convert.ToInt32(dr["AdvRefDocItemNo"]);
            this.AdvRecRefDocCode = Convert.ToString(dr["AdvRecRefDocCode"]);
            this.AdvRecRefDocItemNo = Convert.ToInt32(dr["AdvRecRefDocItemNo"]);
            this.MaterialDocTypeCode = Convert.ToString(dr["MaterialDocTypeCode"]);
            this.StoreCode = Convert.ToString(dr["StoreCode"]);
            this.StockIndicator = Convert.ToString(dr["StockIndicator"]);
            this.ItemsReceived = Convert.ToString(dr["ItemsReceived"]);
            this.IsCretificateIssued = Convert.ToInt32(dr["IsCretificateIssued"]);
            this.CretificateIssueDate = Convert.ToDateTime(dr["CretificateIssueDate"]);
            this.OutWarrantyOn = Convert.ToString(dr["OutWarrantyOn"]);
            this.ClientCode = Convert.ToString(dr["ClientCode"]);
            this.CreatedBy = Convert.ToString(dr["CreatedBy"]);
            this.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
            this.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            this.MatDesc = Convert.ToString(dr["MatDesc"]);
            this.MaterialTypeDesc = Convert.ToString(dr["MaterialTypeDesc"]);
            this.MatGroup1Desc = Convert.ToString(dr["MatGroup1Desc"]);
            this.ConditionName = Convert.ToString(dr["ConditionName"]);
            this.CallTypeDesc = Convert.ToString(dr["CallTypeDesc"]);

            this.IsRepairable = Convert.ToInt32(dr["IsRepairable"]);
            this.MRevisionCode = Convert.ToString(dr["MRevisionCode"]);
            this.RepairDocTypeCode = Convert.ToString(dr["RepairDocTypeCode"]);

        }
    }
}