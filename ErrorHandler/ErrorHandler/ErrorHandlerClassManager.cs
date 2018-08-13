using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorHandler
{
    public class ErrorHandlerClassManager
    {
        public ICollection<ErrorHandlerClass> colGetCountry(List<ErrorHandlerClass> lst, ErrorHandlerClass objErrorHandlerClass)
        {
            if (objErrorHandlerClass != null)
            {
                lst.Add(objErrorHandlerClass);
            }

            return lst;
        }      
    }
}
