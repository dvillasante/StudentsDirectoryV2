using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Web.Models;

namespace Web.Controllers
{
    public class StudentController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = await Business.ApiClientManager.GetAllStudents(GetToken());

            List<StudentViewModel> students = new List<StudentViewModel>();
            
            foreach (var item in result)
            {
                StudentViewModel student = new StudentViewModel();
                student.Id = item.Id;
                student.FirstName = item.FirstName;
                student.LastName = item.LastName;
                student.Dob = item.Dob;
                student.Sex = item.Sex;
                students.Add(student);
            }

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(StudentViewModel studentVM)
        {
            string result = string.Empty;
            Student student = new Student(studentVM.Id, studentVM.FirstName, studentVM.LastName, studentVM.Dob, studentVM.Sex);
            
            try
            {
                result = await Business.ApiClientManager.Create(student, GetToken());
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message);
                throw;
            }

            if (result == "Forbidden")
            {
                TempData["Message"] = "Action not allowed for your user role.";
                return RedirectToAction("Create", "Student");
            }

            return RedirectToAction("Index", "Student");
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            Student student = await Business.ApiClientManager.GetById(id,GetToken());

            StudentViewModel studentModel = new StudentViewModel();
            studentModel.Id = student.Id;
            studentModel.FirstName = student.FirstName;
            studentModel.LastName = student.LastName;
            studentModel.Dob = student.Dob;
            studentModel.Sex = student.Sex;
            
            ViewBag.Message = TempData["Message"];

            return View(studentModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StudentViewModel studentVM)
        {
            string result = string.Empty;
            Student student = new Student(studentVM.Id, studentVM.FirstName, studentVM.LastName, studentVM.Dob, studentVM.Sex);
            try
            {
                result = await Business.ApiClientManager.Edit(student, GetToken());
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message);
                throw;
            }

            if (result == "Forbidden")
            {
                TempData["Message"] = "Action not allowed for your user role.";
                return RedirectToAction("Edit", "Student");
            }

            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {

            Student student = await Business.ApiClientManager.GetById(id, GetToken());

            StudentViewModel studentModel = new StudentViewModel();
            studentModel.Id = student.Id;
            studentModel.FirstName = student.FirstName;
            studentModel.LastName = student.LastName;
            studentModel.Dob = student.Dob;
            studentModel.Sex = student.Sex;

            return View(studentModel);
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            Student student = await Business.ApiClientManager.GetById(id, GetToken());

            StudentViewModel studentModel = new StudentViewModel();
            studentModel.Id = student.Id;
            studentModel.FirstName = student.FirstName;
            studentModel.LastName = student.LastName;
            studentModel.Dob = student.Dob;
            studentModel.Sex = student.Sex;
            
            ViewBag.Message = TempData["Message"];

            return View(studentModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(StudentViewModel studentVM)
        {
            string result = string.Empty;
            try
            {
                result = await Business.ApiClientManager.Delete(studentVM.Id, GetToken());
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message);
                throw;
            }

            if (result == "Forbidden")
            {
                TempData["Message"] = "Action not allowed for your user role.";
                return RedirectToAction("Delete", "Student");
            }

            return RedirectToAction("Index", "Student");
        }
    }
}