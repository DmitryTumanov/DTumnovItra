namespace OnlinerTask.Data.EntityMappers
{
    public interface IPriceAmmountMapper<TK,T> : IMapper<TK, T>
    {
        TK ConvertToModel(T model, int pricemaxid);
    }
}
