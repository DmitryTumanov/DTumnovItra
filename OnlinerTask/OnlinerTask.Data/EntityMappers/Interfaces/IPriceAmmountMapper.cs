namespace OnlinerTask.Data.EntityMappers.Interfaces
{
    public interface IPriceAmmountMapper<TK,T> : IMapper<TK, T>
    {
        TK ConvertToModel(T model, int pricemaxid);
    }
}
