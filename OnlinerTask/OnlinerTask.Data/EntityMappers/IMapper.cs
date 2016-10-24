namespace OnlinerTask.Data.EntityMappers
{
    public interface IMapper<in TK, out T>
    {
        T ConvertToModel(TK dbmodel);
    }
}
