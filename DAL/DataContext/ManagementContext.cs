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
        public DbSet<PercentOrSchedule> PercentOrSchedules { get; set; }

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
            db.ProjectRoles.Add(new ProjectRole { Id = 1, ProjectRoleName = "Front-end разработчик" });
            db.ProjectRoles.Add(new ProjectRole { Id = 2, ProjectRoleName = "Back-end разработчик" });
            db.ProjectRoles.Add(new ProjectRole { Id = 3, ProjectRoleName = "Full-stack разработчик" });
            db.ProjectRoles.Add(new ProjectRole { Id = 4, ProjectRoleName = "Дизайнер" });
            db.ProjectRoles.Add(new ProjectRole { Id = 5, ProjectRoleName = "Тестировщик" });
            db.ProjectRoles.Add(new ProjectRole { Id = 6, ProjectRoleName = "Менеджер проекта" });
            db.ProjectRoles.Add(new ProjectRole { Id = 7, ProjectRoleName = "Бизнес-аналитик" });


            db.Roles.Add(new Role { Id = 1, RoleName = "Ресурсный менеджер" });
            db.Roles.Add(new Role { Id = 2, RoleName = "Проектный менеджер" });
            db.Roles.Add(new Role { Id = 3, RoleName = "Разработчик" });

            db.ProjectStatuses.Add(new ProjectStatus { Id = 1, ProjectStatusName = "Открыт" });
            db.ProjectStatuses.Add(new ProjectStatus { Id = 2, ProjectStatusName = "Закрыт" });

            db.ScheduleDays.Add(new ScheduleDay { Id = 1, ScheduleDayName = "Понедельник" });
            db.ScheduleDays.Add(new ScheduleDay { Id = 2, ScheduleDayName = "Вторник" });
            db.ScheduleDays.Add(new ScheduleDay { Id = 3, ScheduleDayName = "Среда" });
            db.ScheduleDays.Add(new ScheduleDay { Id = 4, ScheduleDayName = "Четверг" });
            db.ScheduleDays.Add(new ScheduleDay { Id = 5, ScheduleDayName = "Пятница" });
            db.ScheduleDays.Add(new ScheduleDay { Id = 6, ScheduleDayName = "Суббота" });

            db.PercentOrSchedules.Add(new PercentOrSchedule { Id = 1, TypeName = "Проценты" });
            db.PercentOrSchedules.Add(new PercentOrSchedule { Id = 2, TypeName = "Расписание" });
            db.PercentOrSchedules.Add(new PercentOrSchedule { Id = 3, TypeName = "Не выбрано" });

            db.SaveChanges();
        }
    }
}
