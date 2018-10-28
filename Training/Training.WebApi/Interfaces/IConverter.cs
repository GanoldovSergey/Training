namespace Training.WebApi.Interfaces
{
    public interface IConverter<TSource, TTarget>
    {
        TTarget Convert(TSource source);

        TSource Convert(TTarget source);
    }
}

