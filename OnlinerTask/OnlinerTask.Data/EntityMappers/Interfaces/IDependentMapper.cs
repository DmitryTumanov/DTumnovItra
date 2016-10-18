namespace OnlinerTask.Data.EntityMappers.Interfaces
{
    public interface IDependentMapper<TK,T> : IMapper<TK,T>
    {
        TK ConvertToModel(T model);
    }
}
