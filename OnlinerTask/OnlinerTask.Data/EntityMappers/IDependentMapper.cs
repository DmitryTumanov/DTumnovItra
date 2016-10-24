namespace OnlinerTask.Data.EntityMappers
{
    public interface IDependentMapper<TK,T> : IMapper<TK,T>
    {
        TK ConvertToModel(T model);
    }
}
