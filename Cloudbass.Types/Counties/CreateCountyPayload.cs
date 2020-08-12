using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Counties
{
    public class CreateCountyPayload
    {
        public CreateCountyPayload(County county)
        {
            County = county;
        }

        public County County { get; set; }
    }
}
