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
            Console.WriteLine("введите id роли");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("введите фамилию");
            string surname = Console.ReadLine();
            Console.WriteLine("введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("введите отчество");
            string patronymic = Console.ReadLine();

            RoleDTO role = service.GetRole(id);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = name,
                EmployeeSurname = surname,
                EmployeePatronymic = patronymic,
                RoleId = role.Id
            };
            service.CreateEmployee(employee1);

            var employees = service.GetEmployees();
            foreach(var employee in employees)
            {
                Console.WriteLine("{0} Роль: {1}", employee.EmployeeSurname, employee.RoleId);
            }
        }
    }
}
