using System;
using System.ComponentModel;

namespace USMALL.Models
{
    public class Employee
    {
        public int? EmployeeId { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("JobTitle")]
        public string JobTitle { get; set; }
        [DisplayName("Department")]
        public string Department { get; set; }
        [DisplayName("ReportsTo")]
        public int? ReportsTo { get; set; }
        public Employee ReportstoEmployee { get; set; }
        [DisplayName("HireDate")]
        public DateTime HireDate { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Phone")]
        public string Phone { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }

        public byte[] Photo { get; set; }

    }
}