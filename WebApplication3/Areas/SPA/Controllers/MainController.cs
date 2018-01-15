using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.SPA;
using WebApplication3.Filters;
using WebApplication3.Models;
using OldViewModel = ViewModel;


namespace WebApplication3.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            MainViewModel v = new ViewModel.SPA.MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new ViewModels.FooterViewModel();
            v.FooterData.CompanyName = "Ultimate Fitness Gym and Swimming pool";
            v.FooterData.Year = DateTime.Now.Year.ToString();
        
            return View("Index",v);
        }

        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empbal = new EmployeeBusinessLayer();
            List<Employee> employees = empbal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<OldViewModel.SPA.EmployeeViewModel>();
            foreach(Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            return View("EmployeeList", employeeListViewModel);

        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel v = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee.cshtml",v);
        }

      

    }
}