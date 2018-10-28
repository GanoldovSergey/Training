using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Training.BAL;
using Ninject;
using Microsoft.Azure.Documents.Client;
using Training.BAL.Services;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Training.DAL.Services.Interfaces;
using Training.DAL.Services.Repositories;

namespace Training.WebApp.Infrastructure
{
    public class NinjectResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        private readonly IConfigManager _config = new ConfigManager();

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<ILogService>().To<NLogService>().WithConstructorArgument("logService", new NLogService());
            _kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("logService", new NLogService());
            _kernel.Bind<IConfigManager>().To<ConfigManager>();
            _kernel.Bind<IUserRepository>().To<UserRepository>()
                .WithConstructorArgument("client",
                    new DocumentClient(new Uri(
                        _config.Endpoint),
                        _config.AuthKey,
                        new ConnectionPolicy { EnableEndpointDiscovery = false }))
                        .WithConstructorArgument("_config", new ConfigManager())
                        .Intercept().With<LoggerInterceptor>();
        }
    }
}