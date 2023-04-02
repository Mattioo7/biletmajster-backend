using System.Text;
using AutoMapper;
using biletmajster_backend.Domain.DTOS;
using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.DTOS.ModelEventDTO, Database.Entities.ModelEvent>();
            CreateMap<Domain.DTOS.Category, Database.Entities.Category>();
            CreateMap<Database.Entities.Category, Domain.DTOS.Category>();
            CreateMap<Database.Entities.Organizer, Domain.DTOS.OrganizerDTO>()
                .ForMember(source => source.Password,
                    m => m.MapFrom(source => Encoding.UTF8.GetString(source.PasswordHash)));
            CreateMap<Domain.DTOS.OrganizerDTO, Database.Entities.Organizer>();
        }
    }
}