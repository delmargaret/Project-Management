using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using BLL.Services;
using BLL.DTO;
using Repository.Interfaces;
using Repository.Repositories;
using BLL.Interfaces;
using BLL.Infrastructure;
using Ninject.Modules;
using Ninject;

namespace ConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
            Service service=new Service(uow);

            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeePatronymic = "Сергеевна",
                EmployeeSurname = "Потапова",
                RoleId = 1
            };
            service.CreateEmployee(employee1);
            var employees = service.GetEmployees();
            foreach(var employee in employees)
            {
                Console.WriteLine("{0} {1}  Роль: {2}", employee.EmployeeName, employee.EmployeeSurname, employee.RoleId);
            }
        }
    }
}
