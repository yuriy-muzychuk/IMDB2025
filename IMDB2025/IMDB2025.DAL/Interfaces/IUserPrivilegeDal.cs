using IMDB2025.DTO;

namespace IMDB2025.DAL.Interfaces
{
    public interface IUserPrivilegeDal
    {
        void AddPrivilegeToUser(int userId, PrivilegeType privilegeType);
    }
}
