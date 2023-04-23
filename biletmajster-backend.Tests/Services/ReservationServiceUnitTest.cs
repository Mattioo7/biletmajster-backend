using System.Collections.Generic;
using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories.Interfaces;
using biletmajster_backend.Domain.DTOS;
using biletmajster_backend.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace biletmajster_backend.Tests.Services;

public class ReservationServiceUnitTest
{
    [Fact]
    public async void ShouldMakeReservationForGivenPlace()
    {
        // Given 
        var @event = new ModelEvent
        {
            Id = 1,
            Places = new List<Place>(),
        };
        var place = new Place
        {
            Event = @event,
            Free = true,
            Id = 1,
            SeatNumber = 1
        };
        var reservation = new Reservation
        {
            Event = @event,
            Place = place,
            ReservationToken = "token"
        };

        var modelEventRepositoryMock = new Mock<IModelEventRepository>();
        modelEventRepositoryMock.Setup(m => m.ReservePlaceAsync(It.IsAny<ModelEvent>(), It.IsAny<long>()))
            .ReturnsAsync(true);

        var placeRepositoryMock = new Mock<IPlaceRepository>();
        placeRepositoryMock.Setup(p => p.GetPlaceByIdAsync(It.Is<long>(pp => pp == place.Id)))
            .ReturnsAsync(place);

        var reservationRepositoryMock = new Mock<IReservationRepository>();
        reservationRepositoryMock.Setup(r =>
                r.AddReservationAsync(It.Is<Reservation>(rr => rr.Event == @event && rr.Place == place)))
            .ReturnsAsync(reservation);

        var loggerMock = new Mock<ILogger<ReservationService>>();

        // When
        var reservationService = new ReservationService(modelEventRepositoryMock.Object, placeRepositoryMock.Object,
            reservationRepositoryMock.Object, loggerMock.Object);

        var result = await reservationService.MakeReservationAsync(@event, place.Id);

        // Then
        modelEventRepositoryMock.Verify(m => m.ReservePlaceAsync(It.IsAny<ModelEvent>(), It.IsAny<long>()), Times.Once);

        placeRepositoryMock.Verify(p => p.GetPlaceByIdAsync(It.Is<long>(pp => pp == place.Id)), Times.Once);

        reservationRepositoryMock.Verify(r =>
            r.AddReservationAsync(It.Is<Reservation>(rr => rr.Event == @event && rr.Place == place)), Times.Once);

        Assert.Equal(reservation, result);
    }

    [Fact]
    public async void ShouldMakeReservationForRandomPlace()
    {
        var @event = new ModelEvent
        {
            Id = 1,
            Places = new List<Place>(),
        };
        var place = new Place
        {
            Event = @event,
            Free = true,
            Id = 1,
            SeatNumber = 1
        };
        var reservation = new Reservation
        {
            Event = @event,
            Place = place,
            ReservationToken = "token"
        };

        var modelEventRepositoryMock = new Mock<IModelEventRepository>();
        modelEventRepositoryMock.Setup(m => m.ReserveRandomPlace(It.IsAny<ModelEvent>()))
            .ReturnsAsync(place.Id);

        var placeRepositoryMock = new Mock<IPlaceRepository>();
        placeRepositoryMock.Setup(p => p.GetPlaceByIdAsync(It.Is<long>(pp => pp == place.Id)))
            .ReturnsAsync(place);

        var reservationRepositoryMock = new Mock<IReservationRepository>();
        reservationRepositoryMock.Setup(r =>
                r.AddReservationAsync(It.Is<Reservation>(rr => rr.Event == @event && rr.Place == place)))
            .ReturnsAsync(reservation);

        var loggerMock = new Mock<ILogger<ReservationService>>();

        // When
        var reservationService = new ReservationService(modelEventRepositoryMock.Object, placeRepositoryMock.Object,
            reservationRepositoryMock.Object, loggerMock.Object);

        var result = await reservationService.MakeReservationAsync(@event, place.Id);

        // Then
        modelEventRepositoryMock.Verify(m => m.ReservePlaceAsync(It.IsAny<ModelEvent>(), It.IsAny<long>()), Times.Once);

        placeRepositoryMock.Verify(p => p.GetPlaceByIdAsync(It.Is<long>(pp => pp == place.Id)), Times.Once);

        reservationRepositoryMock.Verify(r =>
            r.AddReservationAsync(It.Is<Reservation>(rr => rr.Event == @event && rr.Place == place)), Times.Once);

        Assert.Equal(reservation, result);
    }

    [Fact]
    public async void ShouldDeleteReservation()
    {
        // 
        string reservationToken = "someRandomToken";
        
        var @event = new ModelEvent
        {
            Id = 1,
            Places = new List<Place>(),
        };
        var place = new Place
        {
            Event = @event,
            Free = true,
            Id = 1,
            SeatNumber = 1
        };
        var reservation = new Reservation
        {
            Event = @event,
            Place = place,
            ReservationToken = reservationToken
        };
        
        var reservationRepositoryMock = new Mock<IReservationRepository>();
        reservationRepositoryMock.Setup(r => r.FindByReservationTokenAsync(It.Is<string>(rr => rr == reservationToken)))
            .ReturnsAsync(reservation);

        var modelEventRepositoryMock = new Mock<IModelEventRepository>();

        var placeRepositoryMock = new Mock<IPlaceRepository>();
        
        var loggerMock = new Mock<ILogger<ReservationService>>();
        
        // When 
        var reservationService = new ReservationService(modelEventRepositoryMock.Object, placeRepositoryMock.Object,
            reservationRepositoryMock.Object, loggerMock.Object);
        
        await reservationService.DeleteReservationAsync(reservationToken);
        
        // Then
        reservationRepositoryMock.Verify(r => r.FindByReservationTokenAsync(It.Is<string>(rr => rr == reservationToken)), Times.Once);
        
        modelEventRepositoryMock.Verify(m =>
            m.DeleteReservationAsync(It.Is<ModelEvent>(e => e == @event), It.Is<Place>(p => p == place)), Times.Once);
    }
}