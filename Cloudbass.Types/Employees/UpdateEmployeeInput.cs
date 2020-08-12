using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Employees
{
    public class UpdateEmployeeInput
    {
        public UpdateEmployeeInput(string email, string fullname, string postNominals, string nextOfkin, string alergy, string bared, Uri photo, Guid countyId)
        {
            Email = email;
            FullName = fullname;
            PostNominals = postNominals;
            NextOfKin = nextOfkin;
            Alergy = alergy;
            Bared = bared;
            Photo = photo;
            CountyId = countyId;
        }

        public string Email { get; set; }
        public string FullName { get; set; }

        public string PostNominals { get; set; }

        public string NextOfKin { get; set; }
        public string Alergy { get; set; }

        public string Bared { get; set; }

        public Uri Photo { get; set; }
        public Guid CountyId { get; set; }
    }
}
