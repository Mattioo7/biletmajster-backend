/*
 * System rezerwacji miejsc na eventy
 *
 * Niniejsza dokumentacja stanowi opis REST API implemtowanego przez serwer centralny. Endpointy 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: XXX@pw.edu.pl
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using AutoMapper;
using biletmajster_backend.Attributes;
using biletmajster_backend.Contracts;
using biletmajster_backend.Domain;
using biletmajster_backend.Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using biletmajster_backend.Interfaces;

namespace biletmajster_backend.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class EventApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModelEventRepository _modelEventRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IOrganizersRepository _organizersRepository;
        private readonly ILogger<EventApiController> _logger;
        private readonly IStorage _blobStorage;
        private readonly IEventPhotosRepository _eventPhotosRepository;

        public EventApiController(IMapper mapper, IModelEventRepository modelEventRepository,
            ICategoriesRepository categoriesRepository,
            IPlaceRepository placeRepository, ILogger<EventApiController> logger,
            IOrganizersRepository organizersRepository, IStorage storage,
            IEventPhotosRepository eventPhotosRepository)
        {
            _mapper = mapper;
            _modelEventRepository = modelEventRepository;
            _categoriesRepository = categoriesRepository;
            _placeRepository = placeRepository;
            _logger = logger;
            _organizersRepository = organizersRepository;
            _blobStorage = storage;
            _eventPhotosRepository = eventPhotosRepository;
        }

        /// <summary>
        /// Add new event
        /// </summary>
        /// <param name="body">Add event</param>
        /// <response code="201">event created</response>
        /// <response code="400">event can not be created, field invalid</response>
        /// <response code="403">invalid session</response>
        [HttpPost]
        [Route("/events")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("AddEvent")]
        [SwaggerResponse(statusCode: 201, type: typeof(ModelEventDto), description: "event created")]
        public virtual async Task<IActionResult> AddEvent([FromBody] EventFormDto body)
        {
            _logger.LogDebug($"Add event with name: {body.Name}");
            //Event Model:

            var databaseEvent = _mapper.Map<ModelEvent>(body);
            // Places List: (Database)
            // Handling Places
            List<Place> places = new List<Place>();
            for (int i = 0; i < body.MaxPlace; i++)
            {
                var place = new Place()
                {
                    Free = true,
                    SeatNumber = i + 1,
                    Event = databaseEvent
                };
                places.Add(place);
            }

            // Category List:
            // Handling Categories
            List<Category> categoriesList = new List<Category>();
            foreach (var id in body.CategoriesIds.Where(categoryId => categoryId != null)
                         .Select(categoryId => (int)categoryId!).ToList())
            {
                var category = await _categoriesRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", $"Can 't find category with id: {id}");
                    _logger.LogDebug($"Category with id: {id} can not be found");
                    return StatusCode(400, ModelState);
                }

                categoriesList.Add(category);
                category.Events.Add(databaseEvent);
            }

            databaseEvent.Categories = categoriesList;
            databaseEvent.Places = places;

            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var organizer = await _organizersRepository.GetOrganizerByEmailAsync(email);
            databaseEvent.Organizer = organizer;
            organizer.Events.Add(databaseEvent);

            if (await _modelEventRepository.AddEventAsync(databaseEvent))
            {
                return StatusCode(201, _mapper.Map<ModelEventDto>(databaseEvent));
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(400, ModelState);
            }
        }

        /// <summary>
        /// Cancel event
        /// </summary>
        /// <param name="id">id of Event</param>
        /// <response code="204">deleted</response>
        /// <response code="403">invalid session</response>
        /// <response code="404">id not found</response>
        [HttpDelete]
        [Route("/events/{id}")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("CancelEvent")]
        public virtual async Task<IActionResult> CancelEvent([FromRoute][Required] string id)
        {
            _logger.LogDebug($"Cancel event with id: {id}");
            if (await _modelEventRepository.CancelEventAsync(long.Parse(id)))
            {
                return StatusCode(204);
            }

            return StatusCode(404, new ErrorResponse { Message = "Event not found" });
        }

        /// <summary>
        /// Cancel event
        /// </summary>
        /// <param name="id">id of Event</param>
        /// <param name="path">path of photo</param>
        /// <response code="204">deleted</response>
        /// <response code="403">invalid session</response>
        /// <response code="404">id or path not found</response>
        [HttpDelete]
        [Route("/events/{id}/photos")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("DeletePhoto")]
        public virtual async Task<IActionResult> DeletePhoto([FromRoute][Required] string id, [FromHeader][Required()] string path)
        {
            var @event = await _modelEventRepository.GetEventByIdAsync(long.Parse(id));
            if (@event == null)
            {
                _logger.LogDebug($"Event with id {id} not found");
                return StatusCode(404, new ErrorResponse { Message = "Event not found" });
            }
            var photo = @event.EventPhotos.Find(x => x.DownloadLink == path);
            if (photo == null)
            {
                _logger.LogDebug($"Can not find image with link: [{path}] belong to event with id:{id}");
                return StatusCode(404, new ErrorResponse { Message = "Path not found" });
            }
            @event.EventPhotos.Remove(photo);
            if (!await _eventPhotosRepository.DeletePhoto(photo))
            {
                return StatusCode(403, new ErrorResponse { Message = "invalid session" });
            }
            if (!await _modelEventRepository.UpdateEvent(@event))
            {
                return StatusCode(403, new ErrorResponse { Message = "invalid session" });
            }
            return StatusCode(204);
        }

        /// <summary>
        /// Get list of photo of event
        /// </summary>
        /// <remarks>Returns a list of photo paths</remarks>
        /// <param name="id">ID of event to return</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Event not found</response>
        [HttpGet]
        [Route("/events/{id}/photos")]
        [ValidateModelState]
        [SwaggerOperation("GetPhoto")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<string>), description: "successful operation")]
        public virtual async Task<IActionResult> GetPhoto([FromRoute][Required] long? id)
        {
            if (id == null)
            {
                return StatusCode(400, new ErrorResponse { Message = "Invalid ID supplied" });
            }
            var @event = await _modelEventRepository.GetEventByIdAsync((long)id);
            if (@event == null)
            {
                _logger.LogDebug($"Event with id {id} not found");
                return StatusCode(404, new ErrorResponse { Message = "Event not found" });
            }
            var list = new List<string>();
            @event.EventPhotos.ForEach(x => list.Add(x.DownloadLink));
            return StatusCode(200, list);
        }
        /// <summary>
        /// patch existing event
        /// </summary>
        /// <param name="id">id of Event</param>
        /// <param name="path">path of photo</param>
        /// <response code="200">path added</response>
        /// <response code="400">path already exist</response>
        /// <response code="403">invalid session</response>
        /// <response code="404">id not found</response>
        [HttpPost]
        [Route("/events/{id}/photos")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("PutPhoto")]
        [SwaggerResponse(statusCode: 200, type: typeof(EventPhotosDTO), description: "successful operation")]
        public virtual async Task<IActionResult> PutPhoto([FromRoute][Required] string id, [FromForm][Required] IFormFile file)
        {
            var @event = await _modelEventRepository.GetEventByIdAsync(long.Parse(id));
            if (@event == null)
            {
                _logger.LogDebug($"Event with id: {id} not found");
                return StatusCode(404, new ErrorResponse { Message = "Event not found" });
            }
            var Blobresult = await _blobStorage.UploadFileAsync(file, file.FileName);
            if (Blobresult.Error)
            {
                _logger.LogDebug($"Can not connect to Blob");
                return StatusCode(403, new ErrorResponse { Message = "Invalid Session" });
            }
            var PhotoDb = new EventPhotos
            {
                ModelEvent = @event,
                DownloadLink = Blobresult.Blob.Uri
            };
            if (await _eventPhotosRepository.GetPhotoByLink(PhotoDb.DownloadLink) != null)
            {
                return StatusCode(400, new ErrorResponse { Message = "File with this path already exists" });
            }
            @event.EventPhotos.Add(PhotoDb);
            if (!await _eventPhotosRepository.AddPhotoAsync(PhotoDb))
            {
                return StatusCode(400, new ErrorResponse { Message = "Can not save file in database" });
            }
            await _modelEventRepository.UpdateEvent(@event);
            return StatusCode(200, _mapper.Map<EventPhotosDTO>(PhotoDb));
        }

        /// <summary>
        /// Return list of all events in category
        /// </summary>
        /// <param name="categoryId">ID of category</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid category ID supplied</response>
        [HttpGet]
        [Route("/events/getByCategory")]
        [ValidateModelState]
        [SwaggerOperation("GetByCategory")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ModelEventDto>), description: "successful operation")]
        public virtual async Task<IActionResult> GetByCategory([FromHeader][Required] long? categoryId)
        {
            if (categoryId == null)
            {
                return StatusCode(400, new ErrorResponse { Message = "Bad ID" });
            }

            var events = await _modelEventRepository.GetEventsByCategoryAsync(categoryId.Value);
            return StatusCode(200, _mapper.Map<List<ModelEventDto>>(events));
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
        [Route("/events/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetEventById")]
        [SwaggerResponse(statusCode: 200, type: typeof(EventWithPlacesDto), description: "successful operation")]
        public virtual async Task<IActionResult> GetEventById([FromRoute][Required] long? id)
        {
            if (id == null)
            {
                return StatusCode(400, new ErrorResponse { Message = "Bad ID" });
            }

            var @event = await _modelEventRepository.GetEventByIdAsync(id.Value);

            if (@event == null)
            {
                return StatusCode(404, new ErrorResponse { Message = "Event not found" });
            }

            return StatusCode(200, _mapper.Map<EventWithPlacesDto>(@event));
        }

        /// <summary>
        /// Return list of all events
        /// </summary>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/events")]
        [ValidateModelState]
        [SwaggerOperation("GetEvents")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ModelEventDto>), description: "successful operation")]
        public virtual async Task<IActionResult> GetEvents()
        {
            var events = await _modelEventRepository.GetAllEventsAsync();

            return StatusCode(200, events.Select(e => _mapper.Map<ModelEventDto>(e)).ToList());
        }

        /// <summary>
        /// Return list of events made by organizer, according to session
        /// </summary>
        /// <response code="200">successful operation</response>
        /// <response code="403">invalid session</response>
        [HttpGet]
        [Route("/events/my")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("GetMyEvents")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ModelEventDto>), description: "successful operation")]
        public virtual async Task<IActionResult> GetMyEvents()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (email == null)
            {
                return StatusCode(403, new ErrorResponse { Message = "Invalid session" });
            }

            var organizer = await _organizersRepository.GetOrganizerByEmailAsync(email);
            var events = await _modelEventRepository.GetEventsByOrganizerIdAsync(organizer.Id);
            return StatusCode(200, events.Select(e => _mapper.Map<ModelEventDto>(e)).ToList());
        }

        /// <summary>
        /// patch existing event
        /// </summary>
        /// <param name="id">id of Event</param>
        /// <param name="body">Update an existent user in the store</param>
        /// <response code="200">nothing to do, no field to patch</response>
        /// <response code="202">patched</response>
        /// <response code="400">invalid id or fields in body</response>
        /// <response code="403">invalid session</response>
        /// <response code="404">id not found</response>
        [HttpPatch]
        [Route("/events/{id}")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("PatchEvent")]
        public virtual async Task<IActionResult> PatchEvent([FromRoute][Required] string id,
            [FromBody] EventPatchDto body)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (email == null)
            {
                return StatusCode(403, new ErrorResponse { Message = "Invalid session" });
            }

            var organizer = await _organizersRepository.GetOrganizerByEmailAsync(email);

            if (organizer == null)
            {
                return StatusCode(403, new ErrorResponse { Message = "Invalid session" });
            }

            var eventToUpdate = await _modelEventRepository.GetEventByIdAsync(long.Parse(id));
            if (eventToUpdate == null)
            {
                return StatusCode(404, new ErrorResponse { Message = $"Event with id: {long.Parse(id)} not found" });
            }

            if (organizer.Id != eventToUpdate.Organizer.Id)
            {
                return StatusCode(404, new ErrorResponse
                { Message = $"Event with id: {long.Parse(id)} does not belong to you" });
            }

            if (eventToUpdate.Status != EventStatus.InFuture)
            {
                return StatusCode(404, new ErrorResponse
                { Message = "Event has just started, you can not edit ongoing events" });
            }

            List<Place> places = new List<Place>();
            if (eventToUpdate.GetFreePlaces().Count < body.MaxPlace)
            {
                int idx = eventToUpdate.Places.Count;
                for (int i = idx; i < body.MaxPlace; i++)
                {
                    var place = new Place()
                    {
                        Free = true,
                        SeatNumber = i + 1,
                        Event = eventToUpdate
                    };
                    places.Add(place);
                }
            }

            List<Category> categoriesList = new List<Category>();
            foreach (var category in eventToUpdate.Categories)
            {
                category.Events.Remove(eventToUpdate);
                categoriesList.Add(category);
            }

            eventToUpdate.Categories.Clear();
            await _categoriesRepository.UpdateCategoriesAsync(categoriesList);
            if (body.CategoriesIds.Count != 0)
            {
                foreach (var categoryId in body.CategoriesIds.Where(categoryId => categoryId != null)
                             .Select(categoryId => (int)categoryId!).ToList())
                {
                    var currCategory = await _categoriesRepository.GetCategoryByIdAsync(categoryId);
                    if (currCategory == null)
                    {
                        return StatusCode(404,
                            new ErrorResponse { Message = $"Category with id: {categoryId} not found" });
                    }

                    categoriesList.Add(currCategory);
                    currCategory.Events.Add(eventToUpdate);
                    eventToUpdate.Categories.Add(currCategory);
                }
            }

            eventToUpdate.UpdateData(_mapper.Map<ModelEvent>(body));
            await _modelEventRepository.PatchEventAsync(eventToUpdate, places);
            return StatusCode(202, _mapper.Map<ModelEventDto>(eventToUpdate));
        }
    }
}
