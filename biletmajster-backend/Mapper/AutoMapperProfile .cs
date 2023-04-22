using System.Text;
using AutoMapper;
using biletmajster_backend.Database.Entities;
using biletmajster_backend.Domain.DTOS;

namespace biletmajster_backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.DTOS.ModelEventDTO, Database.Entities.ModelEvent>();
            CreateMap<Database.Entities.ModelEvent, Domain.DTOS.ModelEventDTO>();

            CreateMap<Domain.DTOS.CategoryDTO, Database.Entities.Category>();
            CreateMap<Database.Entities.Category, Domain.DTOS.CategoryDTO>();
            CreateMap<Database.Entities.Organizer, Domain.DTOS.OrganizerDTO>();
            CreateMap<Domain.DTOS.OrganizerDTO, Database.Entities.Organizer>();

            CreateMap<Domain.DTOS.PlaceDTO, Database.Entities.Place>();
            CreateMap<Database.Entities.Place, Domain.DTOS.PlaceDTO>();

            CreateMap<EventFormDTO, ModelEvent>();
        }
    }
}