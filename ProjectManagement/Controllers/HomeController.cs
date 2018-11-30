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
using Repository.Repositories;

namespace ProjectManagement.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeService employeeService;
        IRoleService roleService;
        public HomeController(IEmployeeService empserv, IRoleService roleserv)
        {
            employeeService = empserv;
            roleService = roleserv;
        }
        public ActionResult Index()
        {

            IEnumerable<RoleDTO> roleDTOs = roleService.GetRoles();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
            var roles = mapper.Map<IEnumerable<RoleDTO>, List<RoleViewModel>>(roleDTOs);
            return View(roles);
        }

        public ActionResult CreateEmployee(int? id)
        {
            RoleDTO role = roleService.GetRoleById(id.Value);
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
                employeeService.CreateEmployee(employeeDTO);
            }
            catch (Exception ex)
            {
                var exstring = ex.Message;
            }
            return View(employee);
        }


        public ActionResult ShowEmployee()
        {
            IEnumerable<EmployeeDTO> employeeDTOs = employeeService.GetAllEmployees();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            var employees = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeDTOs);
            return View(employees);
        }

        protected override void Dispose(bool disposing)
        {
            employeeService.Dispose();
            roleService.Dispose();
            base.Dispose(disposing);
        }
    }
}
