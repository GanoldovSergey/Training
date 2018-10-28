using System;
using System.Collections.Generic;
using Training.BAL;
using Microsoft.Azure.Documents.Client;
using Training.BAL.Services;
using Training.DAL.Services;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Training.WebApi.Services;
using Training.WebApi.Interfaces;
using Ninject;
using System.Web.Http.Dependencies;

namespace Training.WebApi.Infrastructure
{
    public class NinjectResolver : IDependencyResolver
    {
        // TODO Naming Convention
        private readonly IKernel kernel;
        private readonly IConfigManager config = new ConfigManager();

        public NinjectResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ILogService>().To<NLogService>().WithConstructorArgument("logService", new NLogService());
            kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("logService", new NLogService());
            kernel.Bind<IConverter>().To<Converter>().WithConstructorArgument("converter", new Converter());
            kernel.Bind<ITestService>().To<TestService>().WithConstructorArgument("converter", new Converter());
            kernel.Bind<IQuestionService>().To<QuestionService>().WithConstructorArgument("converter", new Converter());
            kernel.Bind<IAnswerService>().To<AnswerService>().WithConstructorArgument("converter", new Converter());
            kernel.Bind<IConfigManager>().To<ConfigManager>();

            kernel.Bind<IUserRepository>().To<UserRepository>()
                .WithConstructorArgument("client",
                    new DocumentClient(new Uri(
                        config.Endpoint),
                        config.AuthKey,
                        new ConnectionPolicy { EnableEndpointDiscovery = false }))
                        .WithConstructorArgument("config", new ConfigManager())
                        .Intercept().With<LoggerInterceptor>();

            kernel.Bind<ITestRepository>().To<TestRepository>()
                .WithConstructorArgument("client",
                    new DocumentClient(new Uri(
                        config.Endpoint),
                        config.AuthKey,
                        new ConnectionPolicy { EnableEndpointDiscovery = false }))
                        .WithConstructorArgument("config", new ConfigManager())
                        .Intercept().With<LoggerInterceptor>();

            kernel.Bind<IQuestionRepository>().To<QuestionRepository>()
                .WithConstructorArgument("client",
                    new DocumentClient(new Uri(
                        config.Endpoint),
                        config.AuthKey,
                        new ConnectionPolicy { EnableEndpointDiscovery = false }))
                        .WithConstructorArgument("config", new ConfigManager())
                        .Intercept().With<LoggerInterceptor>();

            kernel.Bind<IAnswerRepository>().To<AnswerRepository>()
                .WithConstructorArgument("client",
                    new DocumentClient(new Uri(
                        config.Endpoint),
                        config.AuthKey,
                        new ConnectionPolicy { EnableEndpointDiscovery = false }))
                        .WithConstructorArgument("config", new ConfigManager())
                        .Intercept().With<LoggerInterceptor>();
        }
    }
}