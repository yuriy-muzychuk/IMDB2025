using IMDB2025.DTO;

namespace IMDB2025.DAL.Interfaces
{
    public interface IUserDal
    {
        User CreateUser(string email, string username, string password);
        bool Login(string username, string password);
        User GetUserByLogin(string username);
        List<User> GetUsers();
        User GetUserById(int id);
    }
}
