using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.Models
{
    public class NewPostViewModel
    {
        public HttpPostedFileBase file { get; set; }

        public String Title { get; set; }

        public String Text { get; set; }
    }
}