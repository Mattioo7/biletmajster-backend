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
using biletmajster_backend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace biletmajster_backend.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class EventOrganizerApiController : ControllerBase
    { 
        /// <summary>
        /// Confirm orginizer account
        /// </summary>
        /// <param name="id">id of Organizer</param>
        /// <param name="code">code from email</param>
        /// <response code="201">account confirmed</response>
        /// <response code="400">code wrong</response>
        [HttpPost]
        [Route("/api/v3/organizer/{id}")]
        [ValidateModelState]
        [SwaggerOperation("Confirm")]
        [SwaggerResponse(statusCode: 201, type: typeof(Organizer), description: "account confirmed")]
        public virtual IActionResult Confirm([FromRoute][Required]string id, [FromQuery][Required()]string code)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Organizer));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"password\" : \"12345\",\n  \"name\" : \"theUser\",\n  \"id\" : 10,\n  \"email\" : \"john@email.com\",\n  \"events\" : [ {\n    \"latitude\" : \"40.4775315\",\n    \"freePlace\" : 2,\n    \"title\" : \"Short description of Event\",\n    \"placeSchema\" : \"Seralized place schema\",\n    \"places\" : [ {\n      \"id\" : 21,\n      \"free\" : true\n    }, {\n      \"id\" : 21,\n      \"free\" : true\n    } ],\n    \"name\" : \"Long description of Event\",\n    \"startTime\" : 1673034164,\n    \"id\" : 10,\n    \"endTime\" : 1683034164,\n    \"categories\" : [ {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    }, {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    } ],\n    \"longitude\" : \"-3.7051359\",\n    \"status\" : \"done\",\n    \"maxPlace\" : 100\n  }, {\n    \"latitude\" : \"40.4775315\",\n    \"freePlace\" : 2,\n    \"title\" : \"Short description of Event\",\n    \"placeSchema\" : \"Seralized place schema\",\n    \"places\" : [ {\n      \"id\" : 21,\n      \"free\" : true\n    }, {\n      \"id\" : 21,\n      \"free\" : true\n    } ],\n    \"name\" : \"Long description of Event\",\n    \"startTime\" : 1673034164,\n    \"id\" : 10,\n    \"endTime\" : 1683034164,\n    \"categories\" : [ {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    }, {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    } ],\n    \"longitude\" : \"-3.7051359\",\n    \"status\" : \"done\",\n    \"maxPlace\" : 100\n  } ],\n  \"status\" : \"pending\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Organizer>(exampleJson)
                        : default(Organizer);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Confirm orginizer account
        /// </summary>
        /// <param name="id">id of Organizer</param>
        /// <response code="204">deleted</response>
        /// <response code="404">id not found</response>
        [HttpDelete]
        [Route("/api/v3/organizer/{id}")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("DeleteOrganizer")]
        public virtual IActionResult DeleteOrganizer([FromRoute][Required]string id)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Get organizer account (my account)
        /// </summary>
        /// <response code="200">successful operation</response>
        /// <response code="400">invalid session</response>
        [HttpGet]
        [Route("/api/v3/organizer")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("GetOrganizer")]
        [SwaggerResponse(statusCode: 200, type: typeof(Organizer), description: "successful operation")]
        public virtual IActionResult GetOrganizer()
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Organizer));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"password\" : \"12345\",\n  \"name\" : \"theUser\",\n  \"id\" : 10,\n  \"email\" : \"john@email.com\",\n  \"events\" : [ {\n    \"latitude\" : \"40.4775315\",\n    \"freePlace\" : 2,\n    \"title\" : \"Short description of Event\",\n    \"placeSchema\" : \"Seralized place schema\",\n    \"places\" : [ {\n      \"id\" : 21,\n      \"free\" : true\n    }, {\n      \"id\" : 21,\n      \"free\" : true\n    } ],\n    \"name\" : \"Long description of Event\",\n    \"startTime\" : 1673034164,\n    \"id\" : 10,\n    \"endTime\" : 1683034164,\n    \"categories\" : [ {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    }, {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    } ],\n    \"longitude\" : \"-3.7051359\",\n    \"status\" : \"done\",\n    \"maxPlace\" : 100\n  }, {\n    \"latitude\" : \"40.4775315\",\n    \"freePlace\" : 2,\n    \"title\" : \"Short description of Event\",\n    \"placeSchema\" : \"Seralized place schema\",\n    \"places\" : [ {\n      \"id\" : 21,\n      \"free\" : true\n    }, {\n      \"id\" : 21,\n      \"free\" : true\n    } ],\n    \"name\" : \"Long description of Event\",\n    \"startTime\" : 1673034164,\n    \"id\" : 10,\n    \"endTime\" : 1683034164,\n    \"categories\" : [ {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    }, {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    } ],\n    \"longitude\" : \"-3.7051359\",\n    \"status\" : \"done\",\n    \"maxPlace\" : 100\n  } ],\n  \"status\" : \"pending\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Organizer>(exampleJson)
                        : default(Organizer);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Logs organizer into the system
        /// </summary>
        /// <param name="email">The organizer email for login</param>
        /// <param name="password">the password</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid email/password supplied</response>
        [HttpGet]
        [Route("/api/v3/organizer/login")]
        [ValidateModelState]
        [SwaggerOperation("LoginOrganizer")]
        [SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "successful operation")]
        public virtual IActionResult LoginOrganizer([FromQuery][Required()]string email, [FromQuery][Required()]string password)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse200));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"sessionToken\" : \"sessionToken\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
                        : default(InlineResponse200);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Patch orginizer account
        /// </summary>
        /// <param name="id">id of Organizer</param>
        /// <param name="body">Update an existent user in the store</param>
        /// <response code="202">patched</response>
        /// <response code="404">id not found</response>
        [HttpPatch]
        [Route("/api/v3/organizer/{id}")]
        [Authorize]
        [ValidateModelState]
        [SwaggerOperation("PatchOrganizer")]
        public virtual IActionResult PatchOrganizer([FromRoute][Required]string id, [FromBody]Organizer body)
        { 
            //TODO: Uncomment the next line to return response 202 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(202);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Create orginizer account
        /// </summary>
        /// <param name="name">name of Organizer</param>
        /// <param name="email">email of Organizer</param>
        /// <param name="password">password of Organizer</param>
        /// <response code="201">successful operation</response>
        /// <response code="400">organizer already exist</response>
        [HttpPost]
        [Route("/api/v3/organizer")]
        [ValidateModelState]
        [SwaggerOperation("SignUp")]
        [SwaggerResponse(statusCode: 201, type: typeof(Organizer), description: "successful operation")]
        public virtual IActionResult SignUp([FromQuery][Required()]string name, [FromQuery][Required()]string email, [FromQuery][Required()]string password)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Organizer));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"password\" : \"12345\",\n  \"name\" : \"theUser\",\n  \"id\" : 10,\n  \"email\" : \"john@email.com\",\n  \"events\" : [ {\n    \"latitude\" : \"40.4775315\",\n    \"freePlace\" : 2,\n    \"title\" : \"Short description of Event\",\n    \"placeSchema\" : \"Seralized place schema\",\n    \"places\" : [ {\n      \"id\" : 21,\n      \"free\" : true\n    }, {\n      \"id\" : 21,\n      \"free\" : true\n    } ],\n    \"name\" : \"Long description of Event\",\n    \"startTime\" : 1673034164,\n    \"id\" : 10,\n    \"endTime\" : 1683034164,\n    \"categories\" : [ {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    }, {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    } ],\n    \"longitude\" : \"-3.7051359\",\n    \"status\" : \"done\",\n    \"maxPlace\" : 100\n  }, {\n    \"latitude\" : \"40.4775315\",\n    \"freePlace\" : 2,\n    \"title\" : \"Short description of Event\",\n    \"placeSchema\" : \"Seralized place schema\",\n    \"places\" : [ {\n      \"id\" : 21,\n      \"free\" : true\n    }, {\n      \"id\" : 21,\n      \"free\" : true\n    } ],\n    \"name\" : \"Long description of Event\",\n    \"startTime\" : 1673034164,\n    \"id\" : 10,\n    \"endTime\" : 1683034164,\n    \"categories\" : [ {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    }, {\n      \"name\" : \"Sport\",\n      \"id\" : 1\n    } ],\n    \"longitude\" : \"-3.7051359\",\n    \"status\" : \"done\",\n    \"maxPlace\" : 100\n  } ],\n  \"status\" : \"pending\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Organizer>(exampleJson)
                        : default(Organizer);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
