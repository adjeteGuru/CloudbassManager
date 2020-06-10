using Cloudbass.Database.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class CrewType : ObjectType<Crew>
    {
        protected override void Configure(IObjectTypeDescriptor<Crew> descriptor)
        {
            descriptor.Field(x => x.HasRoleId).Type<IdType>();
            descriptor.Field(x => x.JobId).Type<IdType>();


        }
    }
}
