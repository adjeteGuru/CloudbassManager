using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class AuthenticateModel

    {

        [Required]

        public string Name { get; set; }



        [Required]

        public string Password { get; set; }

    }
}
