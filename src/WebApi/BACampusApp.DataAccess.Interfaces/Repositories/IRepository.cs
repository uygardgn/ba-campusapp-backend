namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        bool Add(T item);
        bool Edit(T item);
        bool Remove(T item);

        List<T> GetAll();
        T GetByID(int id);
    }
}
