using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USMALL.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int ProductDetailId { get; set; }
        public byte[] Photo { get; set; }

        public string ProductName { get; set; }
        public string ProductVendor { get; set; }
        public string QuantityInStock { get; set; }
        public string Price { get; set; }
    }
}