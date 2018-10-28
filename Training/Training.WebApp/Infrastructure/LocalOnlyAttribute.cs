using System.Web;
using System.Web.Mvc;

namespace Training.WebApp.Infrastructure
{
    public class LocalOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.IsLocal;              
        }

    }
}