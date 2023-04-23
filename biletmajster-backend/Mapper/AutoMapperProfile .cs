using AutoMapper;
using biletmajster_backend.Contracts;
using biletmajster_backend.Domain;

namespace biletmajster_backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        // TODO: Inject repositories
        public AutoMapperProfile()
        {
            CreateMap<ModelEventDTO, ModelEvent>();
            CreateMap<ModelEvent, ModelEventDTO>()
                .ForMember(dest => dest.FreePlace, opt => opt.MapFrom(src => src.MaxPlace));

            CreateMap<EventFormDTO, ModelEvent>()
                .ForMember(destination => destination.FreePlace, m => m.MapFrom(source => source.MaxPlace));
            CreateMap<ModelEvent, EventFormDTO>();

            CreateMap<EventPatchDTO, ModelEvent>()
                .ForMember(destination => destination.FreePlace, m => m.MapFrom(source => source.MaxPlace));
            CreateMap<ModelEvent, EventPatchDTO>();

            CreateMap<EventWithPlacesDTO, ModelEvent>()
                .ForMember(destination => destination.FreePlace, m => m.MapFrom(source => source.MaxPlace));
            CreateMap<ModelEvent, EventWithPlacesDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Organizer, OrganizerDTO>();
            CreateMap<OrganizerDTO, Organizer>();

            // TODO: Add mapping using repository
            CreateMap<ReservationDTO, Reservation>();
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(destination => destination.EventId,
                    m => m.MapFrom(source => source.Event.Id))
                .ForMember(destination => destination.PlaceId,
                    m => m.MapFrom(source => source.Place.Id));

            CreateMap<PlaceDTO, Place>();
            CreateMap<Place, PlaceDTO>();
            
            CreateMap<Organizer, OrganizerFormDTO>();
            CreateMap<OrganizerFormDTO, Organizer>();
        }
    }
}