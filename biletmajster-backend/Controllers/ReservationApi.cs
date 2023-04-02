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
using biletmajster_backend.Attributes;
using biletmajster_backend.Domain.DTOS;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace biletmajster_backend.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ReservationApiController : ControllerBase
    { 
        /// <summary>
        /// Create new reservation
        /// </summary>
        /// <param name="reservationToken">token of reservation</param>
        /// <response code="204">deleted</response>
        /// <response code="404">token not found</response>
        [HttpDelete]
        [Route("/api/v3/reservation")]
        [ValidateModelState]
        [SwaggerOperation("DeleteReservation")]
        public virtual IActionResult DeleteReservation([FromQuery][Required()]string reservationToken)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new reservation
        /// </summary>
        /// <param name="eventId">ID of event</param>
        /// <param name="placeID">ID of place</param>
        /// <response code="201">created</response>
        /// <response code="400">no free place</response>
        /// <response code="404">event not exist or done</response>
        [HttpPost]
        [Route("/api/v3/reservation")]
        [ValidateModelState]
        [SwaggerOperation("MakeReservation")]
        [SwaggerResponse(statusCode: 201, type: typeof(ReservationDTO), description: "created")]
        public virtual IActionResult MakeReservation([FromQuery][Required()]long? eventId, [FromQuery]long? placeID)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(ReservationDTO));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"reservationToken\" : \"df0d69cbe68fb6e2b27aa88f6f94497e\",\n  \"eventId\" : 1,\n  \"placeId\" : 12\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<ReservationDTO>(exampleJson)
                        : default(ReservationDTO);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}