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
        IPasswordRepository Passwords { get; }
        IProjectRepository Projects { get; }
        IProjectRoleRepository ProjectRoles { get; }
        IProjectWorkRepository ProjectWorks { get; }
        IRoleRepository Roles { get; }
        void Save();
    }
}
