/*
 * System rezerwacji miejsc na eventy
 *
 * Niniejsza dokumentacja stanowi opis REST API implemtowanego przez serwer centralny. Endpointy 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: XXX@pw.edu.pl
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.ComponentModel.DataAnnotations;
using AutoMapper;
using biletmajster_backend.Attributes;
using biletmajster_backend.Contracts;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace biletmajster_backend.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="categoryName">name of category</param>
        /// <response code="201">created</response>
        /// <response code="400">category already exist</response>
        /// 
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesApiController> _logger;

        public CategoriesApiController(ICategoriesRepository categoriesRepository, IMapper mapper,ILogger<CategoriesApiController> logger)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="categoryName">name of category</param>
        /// <response code="201">created</response>
        /// <response code="400">category already exist</response>
        /// <response code="403">invalid session</response>
        [HttpPost]
        [Route("/categories")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("AddCategories")]
        [SwaggerResponse(statusCode: 201, type: typeof(CategoryDTO), description: "created")]
        public virtual async Task<IActionResult> AddCategories([FromHeader][Required]string categoryName)
        {
            _logger.LogDebug($"Add Category with name: {categoryName}");

            var tmp = _mapper.Map<Category>(new CategoryDTO()
            {
                Name = categoryName
            });

            if (await _categoriesRepository.GetCategoryByNameAsync(categoryName) != null)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Category already exists");
                _logger.LogDebug($"Category with name: {categoryName} already exists");
                return StatusCode(400, ModelState);
            }

            if (await _categoriesRepository.AddCategoryAsync(tmp))
            {
                return StatusCode(201, _mapper.Map<CategoryDTO>(tmp));
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Something went wrong while savin");
                _logger.LogDebug($"Category with name: {categoryName} was not added, something went wrong");
                return StatusCode(400, ModelState);
            }
        }

        /// <summary>
        /// Return list of all categories
        /// </summary>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/categories")]
        [ValidateModelState]
        [SwaggerOperation("GetCategories")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<CategoryDTO>), description: "successful operation")]
        public virtual async Task<IActionResult> GetCategories()
        {
            var categories = await _categoriesRepository.GetAllCategoriesAsync();
            var resultList = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                resultList.Add(_mapper.Map<CategoryDTO>(category));
            }
            return StatusCode(200,resultList);
        }
    }
}
