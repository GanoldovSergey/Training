using NUnit.Framework;
using System.Web.Mvc;

namespace Training.WebApp.Tests.App_StartTests
{
    public class FilterConfigTests
    {
        [Test]
        public void RegisterBundles_Pozitive()
        {
            FilterConfig.RegisterGlobalFilters(new GlobalFilterCollection());
        }

    }
}
