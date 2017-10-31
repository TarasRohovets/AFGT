using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace AFGT.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetNameUser(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("NameUser");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetLinkFotoUser(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("LinkFotoUser");
            return (claim != null) ? claim.Value : string.Empty;
        }




    }
}