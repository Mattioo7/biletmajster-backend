using AutoMapper;
using biletmajster_backend.Contracts;
using biletmajster_backend.Controllers;
using biletmajster_backend.Database.Interfaces;
using Castle.Core.Logging;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using biletmajster_backend.Domain;

namespace biletmajster_backend.Tests.Services.Controller
{
    public class CategoriesApiControllerTests
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesApiController> _logger;
        public CategoriesApiControllerTests()
        {
            _categoriesRepository = A.Fake<ICategoriesRepository>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<CategoriesApiController>>();
        }
        [Fact]
        public async void CategoriesApiController_GetCategories_ReturnOk()
        {
            var categories = A.Fake<ICollection<CategoryDto>>();
            var categoriesList = A.Fake<List<CategoryDto>>();
            A.CallTo(() => _mapper.Map<List<CategoryDto>>(categories)).Returns(categoriesList);

            var controller = new CategoriesApiController(_categoriesRepository, _mapper, _logger);

            var result = await controller.GetCategories();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(200);
        }
        [Fact]
        public async void CategoriesApiController_AddCategories_ReturnOk()
        {
            string categoryName = "Template2";

            var category = A.Fake<Category>();
            var categoryDto = A.Fake<CategoryDto>();
            Category nullval = null;
            var categoryTask = A.Fake<Task<Category>>();

            A.CallTo(() => _mapper.Map<Category>(categoryDto)).Returns(category);
            A.CallTo(() => _categoriesRepository.GetCategoryByNameAsync(categoryName)).Returns(Task.FromResult<Category>(null));
            A.CallTo(() => _categoriesRepository.AddCategoryAsync(A<Category>._)).Returns(Task.FromResult(true));
            A.CallTo(() => _mapper.Map<CategoryDto>(A<Category>._)).Returns(categoryDto);

            var controller = new CategoriesApiController(_categoriesRepository, _mapper, _logger);

            var result = await controller.AddCategories(categoryName);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.Should().Be(201);
        }
    }
}
