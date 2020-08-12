using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Counties
{
    public class UpdateCountyPayload
    {
        public UpdateCountyPayload(County county)
        {
            County = county;
        }
        public County County { get; set; }
    }
}
