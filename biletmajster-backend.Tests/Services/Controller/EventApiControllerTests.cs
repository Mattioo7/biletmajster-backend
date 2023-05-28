using AutoMapper;
using biletmajster_backend.Contracts;
using biletmajster_backend.Controllers;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Domain;
using biletmajster_backend.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace biletmajster_backend.Tests.Services.Controller
{
    public class EventApiControllerTests
    {

        private readonly IModelEventRepository _modelEventRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IOrganizersRepository _organizersRepository;

        private readonly ILogger<EventApiController> _logger;
        private readonly IMapper _mapper;

        private readonly IStorage _blobStorage;
        private readonly IEventPhotosRepository _eventPhotosRepository;

        public EventApiControllerTests()
        {
            _modelEventRepository = A.Fake<IModelEventRepository>();
            _categoriesRepository = A.Fake<ICategoriesRepository>();
            _placeRepository = A.Fake<IPlaceRepository>();
            _organizersRepository = A.Fake<IOrganizersRepository>();
            _blobStorage= A.Fake<IStorage>();
            _eventPhotosRepository= A.Fake<IEventPhotosRepository>();

            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<EventApiController>>();
        }
        [Fact]
        public async void CategoriesApiController_AddEvent_ReturnOk()
        {
            var body = A.Fake<EventFormDto>();
            var fakeUser = A.Fake<ClaimsPrincipal>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "testmail")
                                   }, "TestAuthentication"));

            var modelevent = A.Fake<ModelEvent>();

            A.CallTo(() => _mapper.Map<ModelEvent>(body)).Returns(modelevent);
            A.CallTo(() => _categoriesRepository.GetCategoryByIdAsync(12)).Returns(A.Fake<Category>());
            A.CallTo(() => _organizersRepository.GetOrganizerByEmailAsync(A<string>._)).Returns(Task.FromResult(new Organizer()));
            A.CallTo(() => _modelEventRepository.AddEventAsync(A<ModelEvent>._)).Returns(Task.FromResult(true));
            A.CallTo(() => _mapper.Map<ModelEventDto>(modelevent)).Returns(A.Fake<ModelEventDto>());


            EventApiController controller = new EventApiController(_mapper, _modelEventRepository, _categoriesRepository, _placeRepository, _logger, _organizersRepository,_blobStorage,_eventPhotosRepository);

            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = await controller.AddEvent(new EventFormDto
            {
                CategoriesIds = new List<int?> { 1 },
                EndTime = 23434324,
                Name = "Test",
                StartTime = 23455,
                MaxPlace = 234,
                PlaceSchema = "TestSchema"
            }); ;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(201);
        }
        [Fact]
        public async void CategoriesApiController_CancelEvent_ReturnOk()
        {
            string id = "32432";
            A.CallTo(() => _modelEventRepository.CancelEventAsync(long.Parse(id))).Returns(true);
            var controller = new EventApiController(_mapper, _modelEventRepository, _categoriesRepository, _placeRepository, _logger, _organizersRepository, _blobStorage, _eventPhotosRepository);
            var result = await controller.CancelEvent(id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(StatusCodeResult));
            ((StatusCodeResult)result).StatusCode.Should().Be(204);
        }
        [Fact]
        public async void CategoriesApiController_GetByCategory_ReturnOk()
        {
            A.CallTo(() => _modelEventRepository.GetEventsByCategoryAsync(1234)).Returns(A.Fake<List<ModelEvent>>());
            var controller = new EventApiController(_mapper, _modelEventRepository, _categoriesRepository, _placeRepository, _logger, _organizersRepository, _blobStorage, _eventPhotosRepository);
            var result = await controller.GetByCategory(213);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(200);
        }
        [Fact]
        public async void CategoriesApiController_GetEventById_ReturnOk()
        {
            A.CallTo(() => _modelEventRepository.GetEventByIdAsync(1234)).Returns(A.Fake<ModelEvent>());
            var controller = new EventApiController(_mapper, _modelEventRepository, _categoriesRepository, _placeRepository, _logger, _organizersRepository, _blobStorage, _eventPhotosRepository);
            var result = await controller.GetEventById(213);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(200);
        }
        [Fact]
        public async void CategoriesApiController_GetEvents_ReturnOk()
        {
            A.CallTo(() => _modelEventRepository.GetAllEventsAsync()).Returns(A.Fake<List<ModelEvent>>());
            var controller = new EventApiController(_mapper, _modelEventRepository, _categoriesRepository, _placeRepository, _logger, _organizersRepository, _blobStorage, _eventPhotosRepository);
            var result = await controller.GetEvents();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(200);
        }
        [Fact]
        public async void CategoriesApiController_GetMyEvents_ReturnOk()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "testmail")
                                   }, "TestAuthentication"));

            A.CallTo(() => _organizersRepository.GetOrganizerByEmailAsync(A<string>._)).Returns(A.Fake<Organizer>());
            A.CallTo(() => _modelEventRepository.GetEventsByOrganizerIdAsync(123)).Returns(A.Fake<List<ModelEvent>>());

            var controller = new EventApiController(_mapper, _modelEventRepository, _categoriesRepository, _placeRepository, _logger, _organizersRepository, _blobStorage, _eventPhotosRepository);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = await controller.GetMyEvents();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async void CategoriesApiController_PatchEvent_ReturnOk()
        {
            var body = A.Fake<EventFormDto>();
            var fakeUser = A.Fake<ClaimsPrincipal>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "testmail")
                                   }, "TestAuthentication"));

            var modelevent = A.Fake<ModelEvent>();
            Organizer tmpOrganizer = new Organizer { Id = 123 };

            A.CallTo(() => _organizersRepository.GetOrganizerByEmailAsync(A<string>._)).Returns(Task.FromResult(new Organizer { Id = 123 }));
            A.CallTo(() => _modelEventRepository.GetEventByIdAsync(A<long>._)).Returns(Task.FromResult(new ModelEvent { Status = EventStatus.InFuture, Organizer = new Organizer { Id = 123 } }));
            A.CallTo(() => _categoriesRepository.UpdateCategoriesAsync(A.Fake<List<Category>>())).Returns(Task.FromResult(true));
            A.CallTo(() => _modelEventRepository.PatchEventAsync(A.Fake<ModelEvent>(), A<List<Place>>._)).Returns(Task.FromResult(true));

            EventApiController controller = new EventApiController(_mapper, _modelEventRepository, _categoriesRepository, _placeRepository, _logger, _organizersRepository, _blobStorage, _eventPhotosRepository);

            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = await controller.PatchEvent("12", new EventPatchDto
            {
                CategoriesIds = new List<int?> { 1 },
                EndTime = 23434324,
                Name = "Test",
                StartTime = 23455,
                MaxPlace = 234,
                PlaceSchema = "TestSchema"
            }); ;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(202);
        }
    }
}
