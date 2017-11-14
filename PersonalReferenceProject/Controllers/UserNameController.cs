using PersonalReferenceProject.Models.Domain;
using PersonalReferenceProject.Models.Request;
using PersonalReferenceProject.Models.Response;
using PersonalReferenceProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonalReferenceProject.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/UserName")]
    public class UserNameController : ApiController
    {
        IUserNameService _userNameService;
        //public UserNameController()
        //{

        //}
        public UserNameController(IUserNameService userNameService)
        {
            _userNameService = userNameService;
        }
     


        [Route(), HttpPost]
        public HttpResponseMessage Insert(UserNameRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                UserName response = new UserName();
                 response.Id = _userNameService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response.Id);

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [Route("login"), HttpPost]
        public HttpResponseMessage Login(UserNameRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                //LoginResponse response = new LoginResponse();
                LoginResponse response = _userNameService.Login(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
