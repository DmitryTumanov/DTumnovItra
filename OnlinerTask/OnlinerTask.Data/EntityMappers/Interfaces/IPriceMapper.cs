namespace OnlinerTask.Data.EntityMappers.Interfaces
{
    public interface IPriceMapper<TK, T> : IMapper<TK, T>
    {
        TK ConvertToModel(T model, int pricemaxid, int priceminid);
    }
}
