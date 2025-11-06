using AutoMapper;
using IMDB2025.DTO;

namespace IMDB2025.DALEF.MapperProfiles
{
    public class UserProfile_Back : Profile
    {
        public UserProfile_Back()
        {
            CreateMap<User, Models.User>()
                .ForMember(dest => dest.UserPrivileges,
                src => src.MapFrom(u => u.Privileges.Select(p => new Models.UserPrivilege
                {
                    UserId = u.UserId,
                    PrivilegeId = p.PrivilegeId,
                    RowInsertTime = DateTime.UtcNow
                })));
            CreateMap<Models.User, User>()
                .ForMember(dest => dest.Privileges,
                src => src.MapFrom(u => u.UserPrivileges.Select(up => up.Privilege)));
        }
    }
}
