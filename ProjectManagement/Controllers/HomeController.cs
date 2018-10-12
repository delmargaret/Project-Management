using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using BLL.Interfaces;
using BLL.DTO;
using ProjectManagement.Models;
using AutoMapper;

namespace ProjectManagement.Controllers
{
    public class HomeController : Controller
    {
        IService service;
        public HomeController(IService serv)
        {
            service = serv;
        }
        public ActionResult Index()
        {

            //service.CreateRole("Resource manager");
            //service.CreateRole("Project manager");
            //service.CreateRole("Worker");

            IEnumerable<RoleDTO> roleDTOs = service.GetRoles();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
            var roles = mapper.Map<IEnumerable<RoleDTO>, List<RoleViewModel>>(roleDTOs);
            return View(roles);
        }

        public ActionResult CreateEmployee(int? id)
        {
            RoleDTO role = service.GetRole(id);
            var employee = new EmployeeViewModel { RoleId = role.Id};

            return View(employee);
        }

        [HttpPost]
        public ActionResult CreateEmployee(EmployeeViewModel employee)
        {
            try
            {
                var employeeDTO = new EmployeeDTO {
                    Id = employee.Id,
                    EmployeeName = employee.EmployeeName,
                    EmployeeSurname = employee.EmployeeSurname,
                    EmployeePatronymic = employee.EmployeePatronymic,
                    Email = employee.Email,
                    GitLink = employee.GitLink,
                    PhoneNumber = employee.PhoneNumber,
                    RoleId = employee.RoleId
                };
                service.CreateEmployee(employeeDTO);
            }
            catch (Exception ex)
            {
                var exstring = ex.Message;
            }
            return View(employee);
        }


        public ActionResult ShowEmployee()
        {
            IEnumerable<EmployeeDTO> employeeDTOs = service.GetEmployees();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            var employees = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeDTOs);
            return View(employees);
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}
