using System;
using System.Web;
using System.Web.Http;
using Microsoft.Azure.Documents.Client;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Ninject.Web.WebApi;
using Training.BAL;
using Training.BAL.Services;
using Training.DAL.Entities;
using Training.DAL.Services.Interfaces;
using Training.DAL.Services.Repositories;
using Training.WebApi;
using Training.WebApi.Entities;
using Training.WebApi.Infrastructure;
using Training.WebApi.Interfaces;
using Training.WebApi.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Training.WebApi
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            IConfigManager config = new ConfigManager();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel, config);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel, IConfigManager config)
        {
            var factory = new ConverterFactory();
            factory.RegisterConverter<TestEntity, TestDto>(() => new TestConverter());
            factory.RegisterConverter<QuestionEntity, QuestionDto>(() => new QuestionConverter());
            factory.RegisterConverter<AnswerEntity, AnswerDto>(() => new AnswerConverter());
            kernel.Bind<IQuestionService>().To<QuestionService>().WithConstructorArgument("converterFactory", factory);
            kernel.Bind<ITestService>().To<TestService>().WithConstructorArgument("converterFactory", factory);
            kernel.Bind<ILogService>().To<NLogService>().WithConstructorArgument("logService", new NLogService());
            kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("logService", new NLogService());
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
        }
    }
}