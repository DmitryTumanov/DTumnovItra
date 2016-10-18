namespace OnlinerTask.Data.EntityMappers.Interfaces
{
    public interface IMapper<in TK, out T>
    {
        T ConvertToModel(TK dbmodel);
    }
}
