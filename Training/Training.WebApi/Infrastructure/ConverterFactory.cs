using System;
using System.Collections.Generic;
using Training.WebApi.Interfaces;

namespace Training.WebApi.Infrastructure
{
    public class ConverterFactory : IConverterFactory
    {
        private readonly Dictionary<Tuple<Type, Type>, Func<object>> _converters =
            new Dictionary<Tuple<Type, Type>, Func<object>>();

        public void RegisterConverter<TSource, TTarget>(Func<object> constructor)
        {
            _converters.Add(new Tuple<Type, Type>(typeof(TSource), typeof(TTarget)), constructor);
        }

        public IConverter<TSource, TTarget> GetConverter<TSource, TTarget>()
        {
            var constructor = _converters[new Tuple<Type, Type>(typeof(TSource), typeof(TTarget))];
            return (IConverter<TSource, TTarget>)constructor();
        }
    }
}