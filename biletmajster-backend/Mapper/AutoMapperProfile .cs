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
            CreateMap<ModelEventDto, ModelEvent>();
            CreateMap<ModelEvent, ModelEventDto>();
            CreateMap<EventFormDto, ModelEvent>()
                .ForMember(dest => dest.FreePlace, opt =>opt.MapFrom(src=>src.MaxPlace));

            CreateMap<EventFormDto, ModelEvent>()
                .ForMember(destination => destination.FreePlace, m => m.MapFrom(source => source.MaxPlace));
            CreateMap<ModelEvent, EventFormDto>();

            CreateMap<EventPatchDto, ModelEvent>()
                .ForMember(destination => destination.FreePlace, m => m.MapFrom(source => source.MaxPlace));
            CreateMap<ModelEvent, EventPatchDto>();

            CreateMap<EventWithPlacesDto, ModelEvent>()
                .ForMember(destination => destination.FreePlace, m => m.MapFrom(source => source.MaxPlace));
            CreateMap<ModelEvent, EventWithPlacesDto>();

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Organizer, OrganizerDto>();
            CreateMap<OrganizerDto, Organizer>();

            // TODO: Add mapping using repository
            CreateMap<ReservationDto, Reservation>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(destination => destination.EventId,
                    m => m.MapFrom(source => source.Event.Id))
                .ForMember(destination => destination.PlaceId,
                    m => m.MapFrom(source => source.Place.Id));

            CreateMap<PlaceDto, Place>();
            CreateMap<Place, PlaceDto>();
            
            CreateMap<Organizer, OrganizerFormDto>();
            CreateMap<OrganizerFormDto, Organizer>();

            CreateMap<EventPhotos,EventPhotosDTO>().ForMember(destination => destination.EventId, m => m.MapFrom(source => source.ModelEvent.Id));
        }
    }
}