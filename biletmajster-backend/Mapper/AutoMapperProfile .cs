using AutoMapper;
using biletmajster_backend.Contracts;
using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        // TODO: Inject repositories
        public AutoMapperProfile()
        {
            CreateMap<ModelEventDTO, Database.Entities.ModelEvent>();
            CreateMap<Database.Entities.ModelEvent, ModelEventDTO>();

            CreateMap<CategoryDTO, Database.Entities.Category>();
            CreateMap<Database.Entities.Category, CategoryDTO>();
            CreateMap<Database.Entities.Organizer, OrganizerDTO>();
            CreateMap<OrganizerDTO, Database.Entities.Organizer>();
            
            // TODO: Add mapping using repository
            CreateMap<ReservationDTO, Database.Entities.Reservation>();
            CreateMap<Database.Entities.Reservation, ReservationDTO>()
                .ForMember(destination => destination.EventId,
                    m => m.MapFrom(source => source.Event.Id))
                .ForMember(destination => destination.PlaceId,
                    m => m.MapFrom(source => source.Place.Id));

            CreateMap<PlaceDTO, Database.Entities.Place>();
            CreateMap<Database.Entities.Place, PlaceDTO>();

            CreateMap<EventFormDTO, ModelEvent>();
        }
    }
}