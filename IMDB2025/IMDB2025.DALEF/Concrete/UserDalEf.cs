using AutoMapper;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace IMDB2025.DALEF.Concrete
{
    public class UserDalEf : IUserDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;

        public UserDalEf(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }

        public User CreateUser(string email, string username, string password)
        {
            using (var context = new ImdbContext(_connStr))
            {
                if (context.Users.Any(u => u.Login == username))
                {
                    throw new Exception("User already exists!");
                }

                Guid salt = Guid.NewGuid();
                var user = new Models.User
                {
                    Login = username,
                    Email = email,
                    Password = hash(password, salt.ToString()),
                    Salt = salt,
                    RowInsertTime = DateTime.UtcNow,
                    RowUpdateTime = DateTime.UtcNow
                };

                context.Users.Add(user);
                context.SaveChanges();

                return _mapper.Map<User>(user);
            }
        }

        public User GetUserById(int id)
        {
            using (var context = new ImdbContext(_connStr))
            {
                return _mapper.Map<User>(context.Users
                    .Include(db => db.UserPrivileges)
                    .ThenInclude(up => up.Privilege)
                    .SingleOrDefault(u => u.UserId == id));
            }
        }

        public User GetUserByLogin(string username)
        {
            using (var context = new ImdbContext(_connStr))
            {
                return _mapper.Map<User>(context.Users
                    .Include(db => db.UserPrivileges)
                    .ThenInclude(up => up.Privilege)
                    .SingleOrDefault(u => u.Login == username));
            }
        }

        public List<User> GetUsers()
        {
            using (var context = new ImdbContext(_connStr))
            {
                return _mapper.Map<List<User>>(context.Users
                    .Include(db => db.UserPrivileges)
                    .ThenInclude(up => up.Privilege));
            }
        }

        public bool Login(string username, string password)
        {
            using (var context = new ImdbContext(_connStr))
            {
                var user = context.Users.SingleOrDefault(u => u.Login == username);
                return user != null && user.Password.SequenceEqual(hash(password, user.Salt.ToString()));
            }
        }

        private byte[] hash(string password, string salt)
        {
            var alg = SHA512.Create();
            return alg.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }
    }
}
