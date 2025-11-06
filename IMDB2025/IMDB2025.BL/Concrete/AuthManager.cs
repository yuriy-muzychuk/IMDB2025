using IMDB2025.BL.Interfaces;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;

namespace IMDB2025.BL.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserDal _userDal;
        private readonly IUserPrivilegeDal _userPrivilegeDal;

        public AuthManager(IUserDal userDal, IUserPrivilegeDal userPrivilegeDal)
        {
            _userDal = userDal;
            _userPrivilegeDal = userPrivilegeDal;
        }

        public User CreateUser(string email, string username, string password, PrivilegeType privilegeType)
        {
            var user = _userDal.CreateUser(email, username, password);
            if (user == null || user.UserId <= 0)
            {
                throw new Exception("User creation failed.");
            }
            _userPrivilegeDal.AddPrivilegeToUser(user.UserId, privilegeType);

            return _userDal.GetUserById(user.UserId);
        }

        public User GetUserById(int id)
        {
            return _userDal.GetUserById(id);
        }

        public User GetUserByLogin(string username)
        {
            return _userDal.GetUserByLogin(username);
        }

        public List<User> GetUsers()
        {
            return _userDal.GetUsers();
        }

        public bool Login(string username, string password)
        {
            return _userDal.Login(username, password);
        }
    }
}
