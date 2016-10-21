namespace OnlinerTask.Data.EntityMappers
{
    public interface IPriceMapper<TK, T> : IMapper<TK, T>
    {
        TK ConvertToModel(T model, int pricemaxid, int priceminid);
    }
}
