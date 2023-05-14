using AutoMapper;
using biletmajster_backend.Controllers;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Database.Repositories;
using biletmajster_backend.Domain;
using biletmajster_backend.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace biletmajster_backend.Tests.Services.Controller
{
    public class ReservationApiControllerTestss
    {
        private readonly IModelEventRepository _eventsRepository;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationApiController> _logger;
        public ReservationApiControllerTestss()
        {
            _eventsRepository = A.Fake<IModelEventRepository>();
            _reservationService = A.Fake<IReservationService>();

            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<ReservationApiController>>();
        }
        [Fact]
        public async void CategoriesApiController_DeleteReservation_ReturnOk()
        {
            A.CallTo(() => _reservationService.DeleteReservationAsync(A<string>._)).Returns(Task.CompletedTask);
            var controller = new ReservationApiController(_eventsRepository, _reservationService, _mapper, _logger);
            var result = await controller.DeleteReservation("wqgwg");
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(StatusCodeResult));
            ((StatusCodeResult)result).StatusCode.Should().Be(204);
        }
        [Fact]
        public async void CategoriesApiController_MakeReservation_ReturnOk()
        {
            A.CallTo(() => _eventsRepository.GetEventByIdAsync(A<long>._)).Returns(Task.FromResult(new Domain.ModelEvent { FreePlace = 12, Status = Domain.EventStatus.InFuture }));
            A.CallTo(() => _reservationService.MakeReservationAsync(A<ModelEvent>._, A<long>._)).Returns(Task.FromResult(A.Fake<Reservation>()));

            var controller = new ReservationApiController(_eventsRepository, _reservationService, _mapper, _logger);
            var result = await controller.MakeReservation(234, 2345);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(201);

        }
    }
}

