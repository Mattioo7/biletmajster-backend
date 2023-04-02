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
using biletmajster_backend.Database.Repositories.Interfaces;
using biletmajster_backend.Domain.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace biletmajster_backend.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class EventApiController : ControllerBase
    {
        /// <summary>
        /// Add new event
        /// </summary>
        /// <param name="title">title of Event</param>
        /// <param name="name">title of Event</param>
        /// <param name="freePlace">No of free places</param>
        /// <param name="startTime">Unix time stamp of begin of event</param>
        /// <param name="endTime">Unix time stamp of end of event</param>
        /// <param name="latitude">Latitude of event</param>
        /// <param name="longitude">Longitude of event</param>
        /// <param name="categories">Array of id of categories that event belong to.</param>
        /// <param name="placeSchema">seralized place schema</param>
        /// <response code="201">event created</response>
        /// <response code="400">event can not be created</response>
        /// 

        private readonly IMapper _mapper;
        private readonly IModelEventRepository _modelEventRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IPlaceRepository _placeRepository;

        public EventApiController(IMapper mapper, IModelEventRepository modelEventRepository, ICategoriesRepository categoriesRepository,
            IPlaceRepository placeRepository)
        {
            _mapper = mapper;
            _modelEventRepository = modelEventRepository;
            _categoriesRepository = categoriesRepository;
            _placeRepository = placeRepository;
        }

        [HttpPost]
        [Route("/api/v3/events")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("AddEvent")]
        [SwaggerResponse(statusCode: 201, type: typeof(ModelEvent), description: "event created")]
        public virtual async Task<IActionResult> AddEvent([FromQuery][Required()]string title, [FromQuery][Required()]string name, 
            [FromQuery][Required()]int? freePlace, [FromQuery][Required()]int? startTime, [FromQuery][Required()]int? endTime, 
            [FromQuery][Required()]string latitude,[FromQuery][Required()]string longitude, [FromQuery][Required()]List<int?> categories,
            [FromQuery]string placeSchema)
        {
            //Event Model:
            Domain.DTOS.ModelEvent modelEvent = new Domain.DTOS.ModelEvent()
            {
                Title = title,
                Name = name,
                FreePlace = freePlace,
                StartTime = startTime,
                EndTime = endTime,
                Latitude = latitude,
                Longitude = longitude,
                PlaceSchema = placeSchema
            };
            var databaseEvent = _mapper.Map<Database.Entities.ModelEvent>(modelEvent);

            // Places List: (Database)
            // Handling Places
            List<Database.Entities.Place> places = new List<Database.Entities.Place>();
            for (int i = 0; i < freePlace; i++)
            {
                var place = new Database.Entities.Place()
                {
                    Free = true,
                    SeatNumber = i+1,
                    Event = databaseEvent
                };
                places.Add(place);
            }

            // Category List:
            // Handling Categories
            List<Database.Entities.Category> categoriesList = new List<Database.Entities.Category>();
            foreach(var id in categories)
            {
                var category = await _categoriesRepository.GetCategoryById((int)id);
                if (id==null || category == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", $"Can 't find category with id: {id}");
                    return BadRequest(ModelState);
                }
                categoriesList.Add(category);
                category.Events.Add(databaseEvent);
            }
            databaseEvent.Categories = categoriesList;
            databaseEvent.Places = places;

            if (await _modelEventRepository.AddEvent(databaseEvent))
            {
                return Ok("Successfully created");
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// Cancel event
        /// </summary>
        /// <param name="id">id of Event</param>
        /// <response code="204">deleted</response>
        /// <response code="404">id not found</response>
        [HttpDelete]
        [Route("/api/v3/events/{id}")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("CancelEvent")]
        public virtual IActionResult CancelEvent([FromRoute][Required]string id)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Return list of all events in category
        /// </summary>
        /// <param name="categoryId">ID of category</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid category ID supplied</response>
        [HttpGet]
        [Route("/api/v3/events/getByCategory")]
        [ValidateModelState]
        [SwaggerOperation("GetByCategory")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ModelEvent>), description: "successful operation")]
        public virtual IActionResult GetByCategory([FromQuery][Required()]long? categoryId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<ModelEvent>));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "[ {\n  \"latitude\" : \"40.4775315\",\n  \"freePlace\" : 2,\n  \"title\" : \"Short description of Event\",\n  \"placeSchema\" : \"Seralized place schema\",\n  \"places\" : [ {\n    \"id\" : 21,\n    \"free\" : true\n  }, {\n    \"id\" : 21,\n    \"free\" : true\n  } ],\n  \"name\" : \"Long description of Event\",\n  \"startTime\" : 1673034164,\n  \"id\" : 10,\n  \"endTime\" : 1683034164,\n  \"categories\" : [ {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  } ],\n  \"longitude\" : \"-3.7051359\",\n  \"status\" : \"done\",\n  \"maxPlace\" : 100\n}, {\n  \"latitude\" : \"40.4775315\",\n  \"freePlace\" : 2,\n  \"title\" : \"Short description of Event\",\n  \"placeSchema\" : \"Seralized place schema\",\n  \"places\" : [ {\n    \"id\" : 21,\n    \"free\" : true\n  }, {\n    \"id\" : 21,\n    \"free\" : true\n  } ],\n  \"name\" : \"Long description of Event\",\n  \"startTime\" : 1673034164,\n  \"id\" : 10,\n  \"endTime\" : 1683034164,\n  \"categories\" : [ {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  } ],\n  \"longitude\" : \"-3.7051359\",\n  \"status\" : \"done\",\n  \"maxPlace\" : 100\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<ModelEvent>>(exampleJson)
                        : default(List<ModelEvent>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find event by ID
        /// </summary>
        /// <remarks>Returns a single event</remarks>
        /// <param name="id">ID of event to return</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Event not found</response>
        [HttpGet]
        [Route("/api/v3/events/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetEventById")]
        [SwaggerResponse(statusCode: 200, type: typeof(ModelEvent), description: "successful operation")]
        public virtual IActionResult GetEventById([FromRoute][Required]long? id)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(ModelEvent));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"latitude\" : \"40.4775315\",\n  \"freePlace\" : 2,\n  \"title\" : \"Short description of Event\",\n  \"placeSchema\" : \"Seralized place schema\",\n  \"places\" : [ {\n    \"id\" : 21,\n    \"free\" : true\n  }, {\n    \"id\" : 21,\n    \"free\" : true\n  } ],\n  \"name\" : \"Long description of Event\",\n  \"startTime\" : 1673034164,\n  \"id\" : 10,\n  \"endTime\" : 1683034164,\n  \"categories\" : [ {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  } ],\n  \"longitude\" : \"-3.7051359\",\n  \"status\" : \"done\",\n  \"maxPlace\" : 100\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<ModelEvent>(exampleJson)
                        : default(ModelEvent);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Return list of all events
        /// </summary>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/api/v3/events")]
        [ValidateModelState]
        [SwaggerOperation("GetEvents")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ModelEvent>), description: "successful operation")]
        public virtual IActionResult GetEvents()
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<ModelEvent>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"latitude\" : \"40.4775315\",\n  \"freePlace\" : 2,\n  \"title\" : \"Short description of Event\",\n  \"placeSchema\" : \"Seralized place schema\",\n  \"places\" : [ {\n    \"id\" : 21,\n    \"free\" : true\n  }, {\n    \"id\" : 21,\n    \"free\" : true\n  } ],\n  \"name\" : \"Long description of Event\",\n  \"startTime\" : 1673034164,\n  \"id\" : 10,\n  \"endTime\" : 1683034164,\n  \"categories\" : [ {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  } ],\n  \"longitude\" : \"-3.7051359\",\n  \"status\" : \"done\",\n  \"maxPlace\" : 100\n}, {\n  \"latitude\" : \"40.4775315\",\n  \"freePlace\" : 2,\n  \"title\" : \"Short description of Event\",\n  \"placeSchema\" : \"Seralized place schema\",\n  \"places\" : [ {\n    \"id\" : 21,\n    \"free\" : true\n  }, {\n    \"id\" : 21,\n    \"free\" : true\n  } ],\n  \"name\" : \"Long description of Event\",\n  \"startTime\" : 1673034164,\n  \"id\" : 10,\n  \"endTime\" : 1683034164,\n  \"categories\" : [ {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  } ],\n  \"longitude\" : \"-3.7051359\",\n  \"status\" : \"done\",\n  \"maxPlace\" : 100\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<ModelEvent>>(exampleJson)
                        : default(List<ModelEvent>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Return list of events made by organizer, according to session
        /// </summary>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/api/v3/events/my")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("GetMyEvents")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ModelEvent>), description: "successful operation")]
        public virtual IActionResult GetMyEvents()
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<ModelEvent>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"latitude\" : \"40.4775315\",\n  \"freePlace\" : 2,\n  \"title\" : \"Short description of Event\",\n  \"placeSchema\" : \"Seralized place schema\",\n  \"places\" : [ {\n    \"id\" : 21,\n    \"free\" : true\n  }, {\n    \"id\" : 21,\n    \"free\" : true\n  } ],\n  \"name\" : \"Long description of Event\",\n  \"startTime\" : 1673034164,\n  \"id\" : 10,\n  \"endTime\" : 1683034164,\n  \"categories\" : [ {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  } ],\n  \"longitude\" : \"-3.7051359\",\n  \"status\" : \"done\",\n  \"maxPlace\" : 100\n}, {\n  \"latitude\" : \"40.4775315\",\n  \"freePlace\" : 2,\n  \"title\" : \"Short description of Event\",\n  \"placeSchema\" : \"Seralized place schema\",\n  \"places\" : [ {\n    \"id\" : 21,\n    \"free\" : true\n  }, {\n    \"id\" : 21,\n    \"free\" : true\n  } ],\n  \"name\" : \"Long description of Event\",\n  \"startTime\" : 1673034164,\n  \"id\" : 10,\n  \"endTime\" : 1683034164,\n  \"categories\" : [ {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"Sport\",\n    \"id\" : 1\n  } ],\n  \"longitude\" : \"-3.7051359\",\n  \"status\" : \"done\",\n  \"maxPlace\" : 100\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<ModelEvent>>(exampleJson)
                        : default(List<ModelEvent>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// patch existing event
        /// </summary>
        /// <param name="id">id of Event</param>
        /// <param name="body">Update an existent user in the store</param>
        /// <response code="202">patched</response>
        /// <response code="404">id not found</response>
        [HttpPatch]
        [Route("/api/v3/events/{id}")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("PatchEvent")]
        public virtual IActionResult PatchEvent([FromRoute][Required]string id, [FromBody]ModelEvent body)
        { 
            //TODO: Uncomment the next line to return response 202 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(202);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }
    }
}
