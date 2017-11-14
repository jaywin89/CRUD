using HtmlAgilityPack;
using PersonalReferenceProject.Models.Domain;
using PersonalReferenceProject.Models.Request;
using PersonalReferenceProject.Models.Response;
using PersonalReferenceProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace PersonalReferenceProject.Controllers
{

    [RoutePrefix("api/reference")]
    public class ReferenceController : ApiController
    {
        IReferenceService _referenceService;

        public ReferenceController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }
        [Route("{zipcode}"), HttpPost]
        public HttpResponseMessage FindZipcode(string zipcode)
        {
            WebScrapeResponse response = new WebScrapeResponse();
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString("https://weather.com/weather/today/l/" + zipcode + ":4:US"));
            var root = html.DocumentNode;
            var p = root.Descendants()
                .Where(n => n.GetAttributeValue("class", "").Equals("today_nowcard-temp"))
                .Single()
                .Descendants("span")
                .Single();
            var content = p.InnerText;
            response.Degrees = Regex.Match(content, @"\d+").Value;

            var location = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("h4 today_nowcard-location")).Single();
            response.Address = location.InnerText;

         
      


            //var location = root.Descendants()
            //  .Where(n => n.GetAttributeValue("class", "").Equals("today_nowcard-location"))
            //  .Single()
            //  .Descendants("h1")
            //  .Single();
            //var content2 = location.InnerText;

            //var city = Regex.Match(content2, @"\d+").Value;
            //var degrees = Regex.Match(content, @"\d+").Value;






            return Request.CreateResponse(HttpStatusCode.OK, response);

        }



        [Route(), HttpPost]
        public HttpResponseMessage Insert(ReferenceRequest model)
        {

          


            try
            {
                int response = _referenceService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Route(), HttpPut]
        public HttpResponseMessage Update(ReferenceUpdateRequest model)
        {

            try
            {
                _referenceService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _referenceService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Route("get"), HttpPost]
        public HttpResponseMessage GetAllByType(ReferenceRequestWithPage model)
        {
            try
            {
                IEnumerable<ReferenceRequest> response = _referenceService.GetAllReferenceByType(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetCurrentReference(int id)
        {
            try
            {
                ReferenceResponse response = _referenceService.GetCurrentReference(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}