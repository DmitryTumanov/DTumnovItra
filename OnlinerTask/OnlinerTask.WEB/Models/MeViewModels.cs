﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlinerTask.WEB.Models
{
    // Models returned by MeController actions.
    public class GetViewModel
    {
        public string Email { get; set; }
    }
}