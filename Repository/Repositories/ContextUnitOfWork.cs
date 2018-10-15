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
        private ProjectStatusRepository projectStatusRepository;
        private ProjectWorkRepository projectWorkRepository;
        private RoleRepository roleRepository;
        private ScheduleDayRepository scheduleDayRepository;
        private ScheduleRepository scheduleRepository;
        private PercentOrScheduleRepository percentOrScheduleRepository;

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

        public IProjectStatusRepository ProjectStatuses
        {
            get
            {
                if (projectStatusRepository == null)
                    projectStatusRepository = new ProjectStatusRepository(db);
                return projectStatusRepository;
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

        public IScheduleDayRepository ScheduleDays
        {
            get
            {
                if (scheduleDayRepository == null)
                    scheduleDayRepository = new ScheduleDayRepository(db);
                return scheduleDayRepository;
            }
        }

        public IScheduleRepository Schedules
        {
            get
            {
                if (scheduleRepository == null)
                    scheduleRepository = new ScheduleRepository(db);
                return scheduleRepository;
            }
        }

        public IPercentOrScheduleRepository WorkLoads
        {
            get
            {
                if (percentOrScheduleRepository == null)
                    percentOrScheduleRepository = new PercentOrScheduleRepository(db);
                return percentOrScheduleRepository;
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
