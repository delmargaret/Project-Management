using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ContextUnitOfWork : IUnitOfWork
    {
        private ManagementContext db;
        private EmployeeRepository employeeRepository;
        private ParticipationHistoryRepository participationHistoryRepository;
        private PasswordRepository passwordRepository;
        private ProjectRepository projectRepository;
        private ProjectRoleRepository projectRoleRepository;
        private ProjectWorkRepository projectWorkRepository;
        private RoleRepository roleRepository;

        public ContextUnitOfWork(string connectionString)
        {
            db = new ManagementContext(connectionString);
        }

        public IEmployeeRepository Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        public IParticipationHistoryRepository ParticipationHistories
        {
            get
            {
                if (participationHistoryRepository == null)
                    participationHistoryRepository = new ParticipationHistoryRepository(db);
                return participationHistoryRepository;
            }
        }

        public IPasswordRepository Passwords
        {
            get
            {
                if (passwordRepository == null)
                    passwordRepository = new PasswordRepository(db);
                return passwordRepository;
            }
        }

        public IProjectRepository Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public IProjectRoleRepository ProjectRoles
        {
            get
            {
                if (projectRoleRepository == null)
                    projectRoleRepository = new ProjectRoleRepository(db);
                return projectRoleRepository;
            }
        }

        public IProjectWorkRepository ProjectWorks
        {
            get
            {
                if (projectWorkRepository == null)
                    projectWorkRepository = new ProjectWorkRepository(db);
                return projectWorkRepository;
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
