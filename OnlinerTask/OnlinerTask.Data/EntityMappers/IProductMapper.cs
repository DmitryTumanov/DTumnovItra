namespace OnlinerTask.Data.EntityMappers
{
    public interface IProductMapper<TK, T> : IMapper<TK, T>
    {
        TK ConvertToModel(T model, string useremail, int pricemaxid, int priceminid);
    }
}
