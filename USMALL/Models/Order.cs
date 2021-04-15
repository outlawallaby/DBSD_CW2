using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USMALL.Models
{
    public class Order
    {
        public string OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public bool Status { get; set; }
    }
}