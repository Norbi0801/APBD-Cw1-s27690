using RentItEq.Models.Users;

namespace RentItEq.Services;

public interface IUserProvider
{
    User GetUserOrThrow(Guid userUuid);
}
