﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public string ProjectStatusName { get; set; }
    }
}
