using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorHandler
{
    public class ErrorHandlerClass
    {
        private string _Type;
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        private int _MsgId;
        public int MsgId
        {
            get
            {
                return _MsgId;
            }
            set
            {
                _MsgId = value;
            }
        }

        private string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }

        private int _RowNo;
        public int RowNo
        {
            get
            {
                return _RowNo;
            }
            set
            {
                _RowNo = value;
            }
        }

        private string _FieldName;
        public string FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
            }
        }

        private string _LogCode;
        public string LogCode
        {
            get
            {
                return _LogCode;
            }
            set
            {
                _LogCode = value;
            }
        }

        private string _ReturnValue;
        public string ReturnValue
        {
            get
            {
                return _ReturnValue;
            }
            set
            {
                _ReturnValue = value;
            }
        }

        private string _Module;
        public string Module
        {
            get
            {
                return _Module;
            }
            set
            {
                _Module = value;
            }
        }

        private string _ModulePart;
        public string ModulePart
        {
            get
            {
                return _ModulePart;
            }
            set
            {
                _ModulePart = value;
            }
        }
    }
}
