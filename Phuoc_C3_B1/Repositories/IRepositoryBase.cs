using System.Collections.Generic;


namespace Phuoc_C3_B1.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T item);
        T GetById(int id);
        List<T> GetAll();
    }
}
