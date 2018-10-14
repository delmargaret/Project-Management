using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.DataContext
{
    public class ManagementContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ParticipationHistory> ParticipationHistories { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectWork> ProjectWorks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleDay> ScheduleDays { get; set; }

        static ManagementContext()
        {
            Database.SetInitializer(new ProjectManagementDbInitializer());
        }

        public ManagementContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class ProjectManagementDbInitializer : DropCreateDatabaseIfModelChanges<ManagementContext>
    {
        protected override void Seed(ManagementContext db)
        {
            db.ProjectRoles.Add(new ProjectRole { ProjectRoleName = "фронтенд разработчик" });
            db.ProjectRoles.Add(new ProjectRole { ProjectRoleName = "бэкенд разработчик" });
            db.ProjectRoles.Add(new ProjectRole { ProjectRoleName = "фулстек" });
            db.ProjectRoles.Add(new ProjectRole { ProjectRoleName = "дизайнер" });
            db.ProjectRoles.Add(new ProjectRole { ProjectRoleName = "проектный менеджер" });

            db.Roles.Add(new Role { RoleName = "Ресурсный менеджер" });
            db.Roles.Add(new Role { RoleName = "Проектный менеджер" });
            db.Roles.Add(new Role { RoleName = "Сотрудник" });

            db.ProjectStatuses.Add(new ProjectStatus { ProjectStatusName = "Открыт" });
            db.ProjectStatuses.Add(new ProjectStatus { ProjectStatusName = "Закрыт" });

            db.ScheduleDays.Add(new ScheduleDay { ScheduleDayName = "Понедельник" });
            db.ScheduleDays.Add(new ScheduleDay { ScheduleDayName = "Вторник" });
            db.ScheduleDays.Add(new ScheduleDay { ScheduleDayName = "Среда" });
            db.ScheduleDays.Add(new ScheduleDay { ScheduleDayName = "Четверг" });
            db.ScheduleDays.Add(new ScheduleDay { ScheduleDayName = "Пятница" });
            db.ScheduleDays.Add(new ScheduleDay { ScheduleDayName = "Суббота" });

            db.SaveChanges();
        }
    }
}
