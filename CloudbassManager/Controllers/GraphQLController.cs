using Cloudbass.Database.Models;
using Cloudbass.Utilities;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Controllers
{
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;

        //create a constructor and assign a variable to instantiate them
        public GraphQLController(ISchema schema,
            IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }



        [HttpPost]
        //it will return an actionResult to give the consumer of 
        //the API clear info about the status of the request
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            //must pass the check to 
            if (query == null)
            {
                throw new ArgumentException(nameof(query));
            }

            var inputs = query.Variables?.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };

            var result = await _documentExecuter
                .ExecuteAsync(executionOptions);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            //status of the request
            //pass the result as a parameter
            return Ok(result);
        }


        //    // heree
        //    [HttpGet("{userId}")]
        //    public async Task GetById(int userId)
        //    {
        //        // Initialise the contact that is going to be returned
        //        User user = null;

        //        // Get the requested ETag
        //        string requestETag = "";
        //        if (Request.Headers.ContainsKey("If-None-Match"))
        //        {
        //            requestETag = Request.Headers["If-None-Match"].First();

        //            if (!string.IsNullOrEmpty(requestETag))
        //            {
        //                // The client has supplied an ETag, so, get this version of the contact from our cache

        //                // Construct the key for the cache which includes the entity type (i.e. "contact"), the contact id and the version of the contact record (i.e. the ETag value)
        //                string oldCacheKey = $"user-{userId}-{requestETag}";

        //                // Get the cached item
        //                string cachedContactJson = await cache.GetStringAsync(oldCacheKey);

        //                // If there was a cached item then deserialise this into our contact object
        //                if (!string.IsNullOrEmpty(cachedContactJson))
        //                {
        //                    user = JsonConvert.DeserializeObject(cachedContactJson);

        //                    Log.Information("User {@user} retrieved from cache", user);
        //                }
        //            }
        //        }

        //        // We have no cached contact, then get the contact from the database
        //        if (user == null)
        //        {
        //            user = await db.Users.GetContactByIdAsync(userId);

        //            Log.Information("User {@user} retrieved from database", user);
        //        }

        //        // If no contact was found in the cache or the database, then return a 404
        //        if (user == null)
        //        {
        //            Log.Information("User {@userId} not found", userId);
        //            return NotFound();
        //        }

        //        // Construct the new ETag
        //        string responseETag = Convert.ToBase64String(user.RowVersion);

        //        // Return a 304 if the ETag of the current record matches the ETag in the "If-None-Match" HTTP header
        //        if (Request.Headers.ContainsKey("If-None-Match") && responseETag == requestETag)
        //        {
        //            return StatusCode((int)HttpStatusCode.NotModified);
        //        }

        //        // Add the contact to the cache for 30 mins
        //        string cacheKey = $"user-{userId}-{responseETag}";
        //        await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(user), new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(30) });
        //        Log.Information("User {@user} added to cache with key {@cacheKey}", user, cacheKey);

        //        // Add the current ETag to the HTTP header
        //        Response.Headers.Add("ETag", responseETag);

        //        return Ok(user);

        //    }// endd

    }
}
