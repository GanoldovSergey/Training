using NUnit.Framework;
using System.Web.Optimization;

namespace Training.WebApp.Tests.App_StartTests
{
    public class BundleConfigTests
    {
        [Test]
        public void RegisterBundles_Pozitive()
        {
            BundleConfig.RegisterBundles(new BundleCollection());
        }

    }
}
