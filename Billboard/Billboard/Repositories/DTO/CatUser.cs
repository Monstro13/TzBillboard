using Billboard.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.Repositories.DTO
{
    public class CatUser
    {
        public CatUser(User user)
        {
            Id = user == null ? "" : user._id.ToString();
            Login = user == null ? "" : user.Login;
        }

        public String Id { get; set; }

        public String Login { get; set; }
    }
}