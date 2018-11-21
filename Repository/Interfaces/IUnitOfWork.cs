using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IParticipationHistoryRepository ParticipationHistories { get; }
        ICredentialsRepository Credentials { get; }
        IProjectRepository Projects { get; }
        IProjectRoleRepository ProjectRoles { get; }
        IProjectStatusRepository ProjectStatuses { get; }
        IProjectWorkRepository ProjectWorks { get; }
        IRoleRepository Roles { get; }
        IScheduleDayRepository ScheduleDays { get; }
        IScheduleRepository Schedules { get; }
        IPercentOrScheduleRepository WorkLoads { get; }
        void Save();
    }
}
