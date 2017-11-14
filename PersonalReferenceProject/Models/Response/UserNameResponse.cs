using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Models.Response
{
    public class UserNameResponse
    {
    
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}