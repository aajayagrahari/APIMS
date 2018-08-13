using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSWM.BusinessProcessor.Interface;

namespace MSWMAPI.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : BaseController
    {
        private IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public HttpResponseMessage GetEmployee(int id)
        {
            return CreateHttpResponse(() =>
            {
                return Request.CreateResponse(_employee.GetEmployee(id));
            });
        }

    }
}
