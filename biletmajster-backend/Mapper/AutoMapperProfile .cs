using AutoMapper;
using biletmajster_backend.Contracts;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Domain;

namespace biletmajster_backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        private readonly IModelEventRepository _modelEventRepository;
        private readonly IPlaceRepository _placeRepository;

        public AutoMapperProfile(IModelEventRepository modelEventRepository, IPlaceRepository placeRepository)
        {
            _modelEventRepository = modelEventRepository;
            _placeRepository = placeRepository;
            
            CreateMap<ModelEventDto, ModelEvent>();
            CreateMap<ModelEvent, ModelEventDto>();
            CreateMap<EventFormDto, ModelEvent>()
                .ForMember(dest => dest.FreePlace, opt => opt.MapFrom(src => src.MaxPlace));

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

            CreateMap<ReservationDto, Reservation>()
                .ForMember(destination => destination.Event,
                    m => m.MapFrom(source => _modelEventRepository.GetEventByIdAsync(source.EventId.Value).Result))
                .ForMember(destination => destination.Place,
                    m => m.MapFrom(source => _placeRepository.GetPlaceByIdAsync(source.PlaceId.Value).Result));

            CreateMap<Reservation, ReservationDto>()
                .ForMember(destination => destination.EventId,
                    m => m.MapFrom(source => source.Event.Id))
                .ForMember(destination => destination.PlaceId,
                    m => m.MapFrom(source => source.Place.Id));

            CreateMap<PlaceDto, Place>()
                .ForMember(destination => destination.Event,
                    m => m.MapFrom(source =>
                        _modelEventRepository.GetEventByPlaceIdAsync(source.Id.Value).Result
                    ));
            CreateMap<Place, PlaceDto>();

            CreateMap<Organizer, OrganizerFormDto>();
            CreateMap<OrganizerFormDto, Organizer>();
        }
    }
}