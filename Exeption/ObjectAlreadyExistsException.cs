﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException() : base() { }
    }
}
