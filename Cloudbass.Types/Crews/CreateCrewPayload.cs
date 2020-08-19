using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class CreateCrewPayload
    {
        public CreateCrewPayload(Crew crew)
        {
            Crew = crew;
        }

        public Crew Crew { get; }
    }
}
