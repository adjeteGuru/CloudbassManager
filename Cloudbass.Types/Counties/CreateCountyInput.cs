﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Counties
{
    public class CreateCountyInput
    {
        public CreateCountyInput(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
