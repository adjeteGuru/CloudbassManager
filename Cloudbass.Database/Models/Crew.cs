﻿using HotChocolate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Crew
    {
        public Guid Id { get; set; }
        public Guid HasRoleId { get; set; }
        public HasRole HasRole { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }

    }
}
