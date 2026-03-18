using RentItEq.Models;

namespace RentItEq.Data;

public interface IRepository<T> where T : BaseEntity
{
    List<T> GetAll();
    T? GetById(Guid id);
    void Add(T entity);
    void SaveChanges();
}
