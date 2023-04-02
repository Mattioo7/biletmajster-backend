using AutoMapper;
using biletmajster_backend.Domain.DTOS;
using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.DTOS.ModelEvent, Database.Entities.ModelEvent>();
            CreateMap<Domain.DTOS.Category, Database.Entities.Category>();
            CreateMap<Database.Entities.Category, Domain.DTOS.Category>();
        }
    }
}
