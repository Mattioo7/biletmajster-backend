using AutoMapper;
using biletmajster_backend.Controllers;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FakeItEasy;
using Xunit;
using biletmajster_backend.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using biletmajster_backend.Domain;
using biletmajster_backend.Contracts;

namespace biletmajster_backend.Tests.Services.Controller
{
    public class ReservationApiControllerTests
    {
        private readonly IModelEventRepository _eventsRepository;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationApiController> _logger;
        public ReservationApiControllerTests()
        {
            _eventsRepository = A.Fake<IModelEventRepository>();
            _reservationService = A.Fake<IReservationService>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<ReservationApiController>>();
        }
        [Fact]
        public async void ReservationApiController_DeleteReservation_ReturnOk()
        {
            string token = "TemporaryToken";
            A.CallTo(() => _reservationService.DeleteReservationAsync(token));
            var controller = new ReservationApiController(_eventsRepository, _reservationService, _mapper, _logger);

            var result = await controller.DeleteReservation(token);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(StatusCodeResult));
            ((StatusCodeResult)result).StatusCode.Should().Be(204);
        }
        [Fact]
        public async void ReservationApiController_MakeReservation_ReturnOk()
        {
            long id = 2344;
            long pid = 12;
            var @event = A.Fake<ModelEvent>();
            var reservation = A.Fake<Reservation>();

            A.CallTo(() => _eventsRepository.GetEventByIdAsync(id)).Returns(Task.FromResult(new ModelEvent { FreePlace = 10 }));
            A.CallTo(() => _reservationService.MakeReservationAsync(@event, id)).Returns(reservation);
            A.CallTo(() => _mapper.Map<ReservationDto>(reservation)).Returns(A.Fake<ReservationDto>());
            var controller = new ReservationApiController(_eventsRepository, _reservationService, _mapper, _logger);
            var result = await controller.MakeReservation(id, pid);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(201);
        }
    }
}
