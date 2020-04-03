using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public abstract class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
