using AutoMapper;

namespace IMDB2025.DALEF.MapperProfiles
{
    public class ActorProfile_Back: Profile
    {
        public ActorProfile_Back()
        {
            CreateMap<DTO.Actor, Models.Actor>().ReverseMap();
        }
    }
}
