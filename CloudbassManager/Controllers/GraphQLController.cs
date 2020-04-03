using Cloudbass.Utilities;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
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
    }
}
