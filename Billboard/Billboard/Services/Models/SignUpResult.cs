using Billboard.Repositories.DTO;
using Billboard.Repositories.LkpEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.Services.Models
{
    public class SignUpResult
    {
        public StatusCode Code { get; set; }

        public CatUser User { get; set; }
    }
}