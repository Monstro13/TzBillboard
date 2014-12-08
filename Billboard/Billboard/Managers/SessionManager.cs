using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.Managers
{
    public static class SessionManager
    {
        public static String GetUserId()
        {
            const String userIdField = "UserId";
            var context = HttpContext.Current;
            var user = context.User.Identity;

            if (user.IsAuthenticated)
            {
                if (context.Session[userIdField] != null)
                {
                    return context.Session[userIdField].ToString();
                }
                else
                {
                    String userId = user.Name;
                    context.Session[userIdField] = userId;
                    return userId;
                }
            }

            return "";
        }
    }
}