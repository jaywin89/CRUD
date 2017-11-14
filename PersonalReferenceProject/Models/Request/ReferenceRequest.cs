using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Models.Request
{
    public class ReferenceRequest
    {
        public int Id { get; set; }
        public string CategoryType { get; set; }
        public string Keywords { get; set; }
        public string Example { get; set; }
        public int TotalPages { get; set; }
    }
}