using RentItEq.Data;
using RentItEq.Models.Users;
using RentItEq.Types;

namespace RentItEq.Services;

public class UserService : BaseService<User>, IUserProvider
{
    public UserService(IRepository<User> repo) : base(repo)
    {
    }

    public User AddUser(string name, string surname, UserType type)
    {
        User? userExisted = Repo.GetAll().ToList().Find(u => u.Name == name && u.Surname == surname);
        if (userExisted != null)
        {
            throw new Exception("User with the same name, surname already exists");
        }

        var user = new User
        {
            Name = name,
            Surname = surname,
            Type = type
        };
        Repo.Add(user);
        return user;
    }

    public List<User> GetUserList()
    {
        return Repo.GetAll();
    }

    public User GetUserOrThrow(Guid userUuid)
    {
        return Repo.GetById(userUuid)
               ?? throw new KeyNotFoundException($"User with id {userUuid} not found");
    }
}
