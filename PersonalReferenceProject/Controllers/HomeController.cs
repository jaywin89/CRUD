using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalReferenceProject.Controllers
{
    public class HomeController: Controller
    {
        [Route("~/")]
        public ActionResult MemberLogin()
        {
            return View();
        }
        public ActionResult MemberRegister()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
    }
}