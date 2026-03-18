using RentItEq.Types;

namespace RentItEq.Models.Users;

public class User : BaseEntity
{
    public required String Name { get; set; }
    public required String Surname { get; set; }
    public required UserType Type { get; set; }
}
