using Cloudbass.Database.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Employees
{
    public class EmployeeType : ObjectType<Employee>
    {
        protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.FullName).Type<StringType>();
            descriptor.Field(x => x.PostNominals).Type<StringType>();
            descriptor.Field(x => x.Alergy).Type<StringType>();
            descriptor.Field(x => x.Bared).Type<BooleanType>();
            descriptor.Field(x => x.Email).Type<StringType>();
            descriptor.Field(x => x.NextOfKin).Type<StringType>();
            descriptor.Field(x => x.CountyId).Type<IdType>();
            descriptor.Field(x => x.Photo).Type<StringType>();
        }
    }
}
