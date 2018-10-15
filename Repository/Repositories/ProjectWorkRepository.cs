using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ProjectWorkRepository : IProjectWorkRepository
    {
        private ManagementContext db;

        public ProjectWorkRepository(ManagementContext context)
        {
            this.db = context;
        }

        public ProjectWork FindSameProjectWork(int projectId, int employeeId, int projectRoleId)
        {
            List<ProjectWork> list = new List<ProjectWork>();
            list = db.ProjectWorks.Where(item => item.ProjectId == projectId && item.EmployeeId == employeeId && item.ProjectRoleId == projectRoleId).ToList();
            return list.First();
        }

        public IEnumerable<ProjectWork> GetAllProjectWorks()
        {
            return db.ProjectWorks;
        }

        public IEnumerable<ProjectWork> GetEmployeesProjects(int employeeId)
        {
            return db.ProjectWorks.Where(item => item.EmployeeId == employeeId);
        }

        public int CalculateEmployeesWorkload(int employeeId)
        {
            int result = 0;
            var projects = db.ProjectWorks.Where(item => item.EmployeeId == employeeId);
            foreach(var project in projects)
            {
                result += project.WorkLoad.Value;
            }
            return result;
        }

        public IEnumerable<(string name, string role)> GetNamesOnProject(int projectId)
        {
            List<(string, string)> list=new List<(string, string)>();
            string name = " ";
            string role = " ";
            var employeesOnProject = db.ProjectWorks.Where(item => item.ProjectId == projectId).ToList();
            foreach(var employee in employeesOnProject)
            {
                name = db.Employees.Find(employee.EmployeeId).EmployeeSurname + " " + db.Employees.Find(employee.EmployeeId).EmployeeName + " " + db.Employees.Find(employee.EmployeeId).EmployeePatronymic;
                role = db.ProjectRoles.Find(employee.ProjectRoleId).ProjectRoleName;
                (string, string) tuple = (name, role);
                list.Add(tuple);
            }
            return list;
        }

        public IEnumerable<(string name, string role, string workload)> GetNamesAndLoadOnProject(int projectId)
        {
            List<(string, string, string)> list = new List<(string, string, string)>();
            string name = " ";
            string role = " ";
            string workload = " ";
            var employeesOnProject = db.ProjectWorks.Where(item => item.ProjectId == projectId).ToList();
            foreach (var employee in employeesOnProject)
            {
                name = db.Employees.Find(employee.EmployeeId).EmployeeSurname + " " + db.Employees.Find(employee.EmployeeId).EmployeeName + " " + db.Employees.Find(employee.EmployeeId).EmployeePatronymic;
                role = db.ProjectRoles.Find(employee.ProjectRoleId).ProjectRoleName;
                Employee em = db.Employees.Find(employee.EmployeeId);
                if (em.PercentOrScheduleId == 3)
                {
                    workload = "0%";
                }
                if (em.PercentOrScheduleId == 2)
                {
                    var days = db.Schedules.Where(item => item.ProjectWorkId == employee.Id);
                    foreach (var day in days)
                    {
                        workload += db.ScheduleDays.Find(day.ScheduleDayId).ScheduleDayName + " ";
                    }
                }
                if (em.PercentOrScheduleId == 1)
                {
                    workload = employee.WorkLoad + "%";
                }
                               (string, string, string) tuple = (name, role, workload);
                list.Add(tuple);
            }
            return list;
        }

        public ProjectWork GetProjectWorkById(int id)
        {
            return db.ProjectWorks.Find(id);
        }

        public void CreateProjectWork(ProjectWork projectwork)
        {
            db.ProjectWorks.Add(projectwork);
        }

        public void UpdateProjectWork(ProjectWork projectwork)
        {
            db.Entry(projectwork).State = EntityState.Modified;
        }

        public IEnumerable<ProjectWork> FindProjectWork(Func<ProjectWork, Boolean> predicate)
        {
            return db.ProjectWorks.Where(predicate).ToList();
        }

        public void DeleteProjectWorkById(int id)
        {
            ProjectWork projectwork = db.ProjectWorks.Find(id);
            if (projectwork != null)
                db.ProjectWorks.Remove(projectwork);
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            List<ProjectWork> list = new List<ProjectWork>();
            list = db.ProjectWorks.Where(item => item.ProjectId == projectId && item.EmployeeId == employeeId).ToList();
            ProjectWork projectwork = list.First();
            if (projectwork != null)
                db.ProjectWorks.Remove(projectwork);
        }

        public void ChangeProject(int projectWorkId, int newProjectId)
        {
            Project project = db.Projects.Find(newProjectId);
            if(project==null)
                Console.WriteLine("проект не найден");
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork != null)
            {
                projectWork.ProjectId = newProjectId;
                projectWork.Project = project;
            }
        }

        public void ChangeEmployee(int projectWorkId, int newEmployeeId)
        {
            Employee employee = db.Employees.Find(newEmployeeId);
            if (employee == null)
                Console.WriteLine("сотрудник не найден");
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork != null)
            {
                projectWork.EmployeeId = newEmployeeId;
                projectWork.Employee = employee;
            }
        }

        public void ChangeEmployeesProjectRole(int projectWorkId, int newProjectRoleId)
        {
            ProjectRole projectRole = db.ProjectRoles.Find(newProjectRoleId);
            if (projectRole == null)
                Console.WriteLine("роль на проекте не найден");
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork != null)
            {
                projectWork.ProjectRoleId = newProjectRoleId;
                projectWork.ProjectRole = projectRole;
            }
        }

        public void ChangeWorkLoad(int projectWorkId, int newWorkLoad)
        {
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork != null)
            {
                projectWork.WorkLoad = newWorkLoad;
            }
        }

        public void AddWorkLoad(int projectWorkId, int workLoad)
        {
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork != null)
            {
                projectWork.WorkLoad = workLoad;
            }
        }

        public void DeleteWorkLoad(int projectWorkId)
        {
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork != null)
            {
                projectWork.WorkLoad = null;
            }
        }
    }
}
