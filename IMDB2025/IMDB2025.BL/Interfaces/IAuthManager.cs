using IMDB2025.DTO;

namespace IMDB2025.BL.Interfaces
{
    public interface IAuthManager
    {
        bool Login(string username, string password);
        User CreateUser(string email, string username, string password, PrivilegeType privilegeType);
        User GetUserByLogin(string username);
        User GetUserById(int id);
        List<User> GetUsers();
    }
}
