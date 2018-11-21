using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using DAL.Entities;
using Exeption;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CredentialsService : ICredentialsService
    {
        IUnitOfWork Database { get; set; }
        Map<Credentials, CredentialsDTO> Map = new Map<Credentials, CredentialsDTO>();

        public CredentialsService(IUnitOfWork uow)
        {
            Database = uow;
        }
        PasswordService passwordService = new PasswordService();

        public bool Autenticate(string login, string password)
        {
            if (Database.Credentials.Find(item => item.Login == login).Count() != 0)
            {
                var hash = Database.Credentials.GetLastPassword(login).PasswordString;
                return passwordService.VerifyPassword(hash, password);
            }
            else throw new NotFoundException();
        }

        public CredentialsDTO Registrate(int employeeId, string password)
        {
            string hashPassword = passwordService.GeneratePasswordHash(password);
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Credentials credentials = new Credentials
            {
                Login = employee.Email,
                PasswordString = hashPassword,
                EmployeeId = employeeId,
                Employee = employee
            };
            var cred = Database.Credentials.Create(credentials);
            Database.Save();
            return Map.ObjectMap(cred);
        }
    }
}
