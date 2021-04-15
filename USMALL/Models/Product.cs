﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USMALL.Models
{
    public class Product
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductVender { get; set; }

        public int QuantityInStock { get; set; }

        public decimal Price { get; set; }
    }
}