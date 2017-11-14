using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Models.Request
{
    public class UserNameRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}