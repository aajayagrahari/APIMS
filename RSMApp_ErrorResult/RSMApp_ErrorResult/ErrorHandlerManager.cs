using System;
using System.Collections.Generic;
using System.Text;

namespace RSMApp_ErrorResult
{
    class ErrorHandlerManager
    {
        public ICollection<ErrorHandler> colGetCountry(List<ErrorHandler> lst, ErrorHandler objErrhandler)
        {
            if (objErrhandler != null)
            {
                lst.Add(objErrhandler);
            }

            return lst;
        }       

    }
}
