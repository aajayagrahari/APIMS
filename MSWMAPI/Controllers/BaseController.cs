using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSWM.Common.CustomException;
using MSWM.Common.MessageConfig;

namespace MSWMAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected HttpResponseMessage CreateHttpResponse(Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
                if(response != null && response.StatusCode != HttpStatusCode.OK)
                {
                    return GenerateHttpErrorMessage(response.Content.ToString(), HttpStatusCode.OK);
                }
                if (response == null)
                {
                    return GenerateHttpErrorMessage(null, HttpStatusCode.OK);
                }
                return response;
            }
            catch (MSWMException ex)
            {
                if (!string.IsNullOrEmpty(ex.ErrorMessage))
                    return GenerateHttpErrorMessage(ex.ErrorMessage, HttpStatusCode.OK);

                var errorMessage = MessageCollection.Instance.GetMessage(ex.Code, ex.Category);
                return GenerateHttpErrorMessage(errorMessage, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //return GenerateHttpErrorMessage("Error occured while in Operation. ", HttpStatusCode.OK);
                return GenerateHttpErrorMessage(ex.Message, HttpStatusCode.OK);
            }

        }

        protected HttpResponseMessage GenerateHttpErrorMessage(string message, HttpStatusCode statCode)
        {
            var response = new
            {
                ErrorCode = -1,
                ErrorDescription = message
            };

            return Request.CreateResponse(statCode, response);
        }
    }
}
