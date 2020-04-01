namespace DataMapper
{
    public interface IMapper<T>
    {
        T GetByID(int id);
        void Save(T t);
        void Delete(T t);
    }
}
