using RentItEq.Data;
using RentItEq.Models;

namespace RentItEq.Services;

public class BaseService<T> where T : BaseEntity
{
    protected readonly IRepository<T> Repo;

    public BaseService(IRepository<T> repo)
    {
        Repo = repo;
    }
}