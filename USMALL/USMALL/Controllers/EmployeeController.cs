using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USMALL.DAL;
using USMALL.Models;

namespace USMALL.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var repository = new EmployeeRepository();
            var employees = repository.GetAll();   
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee emp, HttpPostedFileBase imageFile)
        {
            var repository = new EmployeeRepository();
            try
            {
                if (imageFile?.ContentLength>0)
                {
                    using (var stream = new MemoryStream())
                    {

                    imageFile.InputStream.CopyTo(stream);
                    emp.Photo = stream.ToArray();
                    }
                }

                repository.Insert(emp);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new EmployeeRepository();
            var emp = repository.GetById(id);

            var supervisors = repository.GetAll();

            ViewBag.SupervisorsList = supervisors;
            return View(emp);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            var repository = new EmployeeRepository();
            try
            {
                repository.Update(emp);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public FileResult ShowPhoto(int id)
        {
            var repository = new EmployeeRepository();
            var emp = repository.GetById(id);
            if (emp != null && emp.Photo?.Length >0)  
                return File(emp.Photo, "image/jpeg", emp.LastName + ".jpg");
            return null;
        }
    }
}
