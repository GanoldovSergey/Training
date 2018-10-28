namespace Training.WebApi.Interfaces
{
    public interface IConverterFactory
    {
        IConverter<TSource, TTarget> GetConverter<TSource, TTarget>();
    }
}
