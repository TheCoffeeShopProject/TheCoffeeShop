﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCoffeeCatBusinessObject.ViewModels
{
    public class AccountVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string RoleName { get; set; }
    }
}
