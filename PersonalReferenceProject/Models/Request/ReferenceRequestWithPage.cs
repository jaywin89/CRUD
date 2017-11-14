using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Models.Request
{
    public class ReferenceRequestWithPage
    {
        public string Keywords { get; set; }
        public string CategoryType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}