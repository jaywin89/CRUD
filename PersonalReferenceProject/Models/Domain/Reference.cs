using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Models.Domain
{
    public class Reference
    {
        public int Id { get; set; }
        public int CategoryTypeId { get; set; }
        public string Keywords { get; set; }
        public string Example { get; set; }

    }
}