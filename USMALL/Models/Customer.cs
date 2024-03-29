﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USMALL.Models
{
    public class Customer
    {
        public int? CustomerId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int? ReportsTo { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }

        public string City { get; set; }

    }
}