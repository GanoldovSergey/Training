using Ninject.Extensions.Interception;
using System;
using System.Linq;
using Training.BAL;

namespace Training.WebApi.Infrastructure
{
    public class LoggerInterceptor : IInterceptor
    {
        private readonly ILogService _logService;

        public LoggerInterceptor(ILogService logService)
        {
            _logService = logService;
        }

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Request.Method.Name;
            var parameterNames = invocation.Request.Method.GetParameters().Select(p => p.Name).ToList();
            var parameterValues = invocation.Request.Arguments;
            var inputMessage = $"Method {methodName} called with parameters ";

            if (parameterNames.Count == 0) inputMessage = $"Method {methodName} called without parameters ";

            for (int index = 0; index < parameterNames.Count; index++)
            {
                var name = parameterNames[index];
                var value = parameterValues[index];
                inputMessage += $"<{name}>:<{value}>,";
            }

            invocation.Proceed();

            var outputMessage = $"Method {methodName} returned <{invocation.ReturnValue}>";

            _logService.InfoWriteToLog(DateTime.Now, inputMessage, outputMessage);
        }
    }
}