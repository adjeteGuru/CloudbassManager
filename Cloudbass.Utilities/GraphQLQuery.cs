using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Utilities
{
    public class GraphQLQuery
    {
        //this state the name of the query
        public string OperationName { get; set; }

        //the body of request where user determine which property he want to fetch
        public string Query { get; set; }

        //this specify property of variables fetch by user
        public JObject Variables { get; set; }
    }
}
