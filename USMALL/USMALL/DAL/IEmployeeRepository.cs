using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USMALL.Models;

namespace USMALL.DAL
{
    public interface IEmployeeRepository
    {
        IList<Employee> GetAll();

        void Insert(Employee emp);

        Employee GetById(int id);

        void Update(Employee emp);
    }
}