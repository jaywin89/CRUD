using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Models.Request
{
    public class UserNameUpdateRequest: UserNameRequest
    {
        public int Id { get; set; }
    }
}