using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Interfaces;

namespace biletmajster_backend.Services;

public class ReservationService : IReservationService
{
    private static int _reservationTokenLength = 10;
    private static Random Random = new();

    private readonly IModelEventRepository _modelEventRepository;
    private readonly IPlaceRepository _placeRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly ILogger<ReservationService> _logger;

    public ReservationService(IModelEventRepository modelEventRepository, IPlaceRepository placeRepository,
        IReservationRepository reservationRepository, ILogger<ReservationService> logger)
    {
        _modelEventRepository = modelEventRepository;
        _placeRepository = placeRepository;
        _reservationRepository = reservationRepository;
        _logger = logger;
    }

    public async Task<Reservation> MakeReservationAsync(ModelEvent modelEvent, long? placeId)
    {
        _logger.LogDebug($"Reserving place with id: {placeId}");
        try
        {
            if (placeId == null)
            {
                placeId = await _modelEventRepository.ReserveRandomPlace(modelEvent);
            }
            else
            {
                await _modelEventRepository.ReservePlaceAsync(modelEvent, placeId);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }

        var place = await _placeRepository.GetPlaceByIdAsync(placeId.Value);

        var reservation = new Reservation
        {
            Event = modelEvent,
            Place = place,
            ReservationToken = GenerateReservationToken()
        };

        var addedReservation = await _reservationRepository.AddReservationAsync(reservation);

        return addedReservation;
    }

    public async Task DeleteReservationAsync(string reservationToken)
    {
        var reservation = await _reservationRepository.FindByReservationTokenAsync(reservationToken);
        
        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }
        
        await _modelEventRepository.DeleteReservationAsync(reservation.Event, reservation.Place);
        
        await _reservationRepository.DeleteReservationAsync(reservation);
    }

    private static string GenerateReservationToken()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, _reservationTokenLength)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}