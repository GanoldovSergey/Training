using NUnit.Framework;
using Microsoft.Owin.Builder;

namespace Training.WebApp.Tests.App_StartTests
{
    public class IdentityConfigTests
    {
        [Test]
        public void Configuration_Pozitive()
        {
            var config = new IdentityConfig();
            config.Configuration(new AppBuilder());
        }
    }
}
