using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Counties
{
    public class UpdateCountyInput
    {
        public UpdateCountyInput(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
