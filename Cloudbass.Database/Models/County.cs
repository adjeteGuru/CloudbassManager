﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class County
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
