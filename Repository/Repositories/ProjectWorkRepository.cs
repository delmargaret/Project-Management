using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;
using Exeption;

namespace Repository.Repositories
{
    public class ProjectWorkRepository : IProjectWorkRepository
    {
        private ManagementContext db;

        public ProjectWorkRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProjectWork> GetAllProjectWorks()
        {
            if (db.ProjectWorks.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ProjectWorks;
        }

        public IEnumerable<ProjectWork> GetEmployeesOnProject(int projectId)
        {
            if (db.ProjectWorks.Where(item => item.ProjectId == projectId).Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ProjectWorks.Where(item => item.ProjectId == projectId);
        }

        public IEnumerable<ProjectWork> GetEmployeesProjects(int employeeId)
        {
            if (db.ProjectWorks.Where(item => item.EmployeeId == employeeId).Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ProjectWorks.Where(item => item.EmployeeId == employeeId);
        }

        public int CalculateEmployeesWorkload(int employeeId)
        {
            int result = 0;
            var projects = db.ProjectWorks.Where(item => item.EmployeeId == employeeId);
            foreach (var project in projects)
            {
                if (db.Projects.Find(project.ProjectId).ProjectStatusId == 1)
                {
                    result += project.WorkLoad.Value;
                }
            }
            if (result == 0)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public string GetWorkload(int employeeId)
        {
            string workload = "";
            int percent = 0;
            var employeesProjects = db.ProjectWorks.Where(item => item.EmployeeId == employeeId).ToList();
            Employee em = db.Employees.Find(employeeId);
            if (em.PercentOrScheduleId == 3)
            {
                workload = "0%";
            }
            if (em.PercentOrScheduleId == 1)
            {
                foreach(var project in employeesProjects)
                {
                    percent += project.WorkLoad.Value;
                }
                if (percent == 0) { workload = "0%"; }
                else workload = percent + "%";
            }
            if (em.PercentOrScheduleId == 2)
            {
                foreach (var project in employeesProjects)
                {
                    var days = db.Schedules.Where(item => item.ProjectWorkId == project.Id).OrderBy(item => item.ScheduleDayId);
                    foreach (var day in days)
                    {
                        workload += db.ScheduleDays.Find(day.ScheduleDayId).ScheduleDayName + " ";
                    }
                }
                if (workload == "") workload = "---";
            }
            return workload;
        }

        public IEnumerable<(int id, string name, string role)> GetNamesOnProject(int projectId)
        {
            List<(int, string, string)> list = new List<(int, string, string)>();
            int id = 0;
            string name = " ";
            string role = " ";
            var employeesOnProject = db.ProjectWorks.Where(item => item.ProjectId == projectId).ToList();
            foreach (var employee in employeesOnProject)
            {
                id = employee.Id;
                name = db.Employees.Find(employee.EmployeeId).EmployeeSurname + " " + db.Employees.Find(employee.EmployeeId).EmployeeName + " " + db.Employees.Find(employee.EmployeeId).EmployeePatronymic;
                role = db.ProjectRoles.Find(employee.ProjectRoleId).ProjectRoleName;
                (int, string, string) tuple = (id, name, role);
                list.Add(tuple);
            }
            if (list.Count() == 0)
            {
                throw new NotFoundException();
            }
            return list;
        }

        public IEnumerable<(int id, int employeeId, string name, string role, string workload)> GetNamesAndLoadOnProject(int projectId)
        {
            List<(int, int, string, string, string)> list = new List<(int, int, string, string, string)>();
            int id = 0;
            int employeeId = 0;
            string name = " ";
            string role = " ";
            string workload = "";
            var employeesOnProject = db.ProjectWorks.Where(item => item.ProjectId == projectId).ToList();
            foreach (var employee in employeesOnProject)
            {
                id = employee.Id;
                employeeId = employee.EmployeeId;
                name = db.Employees.Find(employee.EmployeeId).EmployeeSurname + " " + db.Employees.Find(employee.EmployeeId).EmployeeName + " " + db.Employees.Find(employee.EmployeeId).EmployeePatronymic;
                role = db.ProjectRoles.Find(employee.ProjectRoleId).ProjectRoleName;
                Employee em = db.Employees.Find(employee.EmployeeId);
                if (em.PercentOrScheduleId == 3)
                {
                    workload = "0%";
                }
                if (em.PercentOrScheduleId == 2)
                {
                    var days = db.Schedules.Where(item => item.ProjectWorkId == employee.Id).OrderBy(item => item.ScheduleDayId);
                    foreach (var day in days)
                    {
                        workload += db.ScheduleDays.Find(day.ScheduleDayId).ScheduleDayName + " ";
                    }
                }
                if (em.PercentOrScheduleId == 1)
                {
                    if (employee.WorkLoad == 0 || employee.WorkLoad == null)
                    {
                        workload = "0%";
                    }
                    else workload = employee.WorkLoad + "%";
                }
                (int, int, string, string, string) tuple = (id, employeeId, name, role, workload);
                list.Add(tuple);
                workload = "";
            }
            if (list.Count() == 0)
            {
                throw new NotFoundException();
            }
            return list;
        }

        public IEnumerable<(int id, int projectId, string projectName, string role, string workload)> GetEmployeesProjectsAndLoad(int employeeId)
        {
            List<(int, int, string, string, string)> list = new List<(int, int, string, string, string)>();
            int id = 0;
            int projectId = 0;
            string projectName = " ";
            string role = " ";
            string workload = "";
            var employeesProjects = db.ProjectWorks.Where(item => item.EmployeeId == employeeId).ToList();
            foreach (var project in employeesProjects)
            {
                id = project.Id;
                projectId = project.ProjectId;
                projectName = db.Projects.Find(project.ProjectId).ProjectName;
                role = db.ProjectRoles.Find(project.ProjectRoleId).ProjectRoleName;
                Employee em = db.Employees.Find(project.EmployeeId);
                if (em.PercentOrScheduleId == 3)
                {
                    workload = "0%";
                }
                if (em.PercentOrScheduleId == 2)
                {
                    var days = db.Schedules.Where(item => item.ProjectWorkId == project.Id).OrderBy(item => item.ScheduleDayId);
                    foreach (var day in days)
                    {
                        workload += db.ScheduleDays.Find(day.ScheduleDayId).ScheduleDayName + " ";
                    }
                }
                if (em.PercentOrScheduleId == 1)
                {
                    if (project.WorkLoad == 0 || project.WorkLoad == null)
                    {
                        workload = "0%";
                    }
                    else workload = project.WorkLoad + "%";
                }
                (int, int, string, string, string) tuple = (id, projectId, projectName, role, workload);
                list.Add(tuple);
                workload = "";
            }
            if (list.Count() == 0)
            {
                throw new NotFoundException();
            }
            return list;
        }

        public ProjectWork GetProjectWorkById(int id)
        {
            if (db.ProjectWorks.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.ProjectWorks.Find(id);
        }

        public ProjectWork CreateProjectWork(ProjectWork projectwork)
        {
            var prw = db.ProjectWorks.Add(projectwork);
            return prw;
        }

        public void UpdateProjectWork(ProjectWork projectwork)
        {
            db.Entry(projectwork).State = EntityState.Modified;
        }

        public IEnumerable<ProjectWork> FindProjectWork(Func<ProjectWork, Boolean> predicate)
        {
            if (db.ProjectWorks.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ProjectWorks.Where(predicate).ToList();
        }

        public void DeleteProjectWorkById(int id)
        {
            ProjectWork projectwork = db.ProjectWorks.Find(id);
            if (projectwork == null)
            {
                throw new NotFoundException();
            }
            db.ProjectWorks.Remove(projectwork);
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            List<ProjectWork> list = new List<ProjectWork>();
            list = db.ProjectWorks.Where(item => item.ProjectId == projectId && item.EmployeeId == employeeId).ToList();
            ProjectWork projectwork = list.First();
            if (projectwork == null)
            {
                throw new NotFoundException();
            }
            db.ProjectWorks.Remove(projectwork);
        }

        public void ChangeProject(int projectWorkId, int newProjectId)
        {
            Project project = db.Projects.Find(newProjectId);
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork == null)
            {
                throw new NotFoundException();
            }
            projectWork.ProjectId = newProjectId;
            projectWork.Project = project;
        }

        public void ChangeEmployee(int projectWorkId, int newEmployeeId)
        {
            Employee employee = db.Employees.Find(newEmployeeId);
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork == null)
            {
                throw new NotFoundException();
            }
            projectWork.EmployeeId = newEmployeeId;
            projectWork.Employee = employee;
        }

        public void ChangeEmployeesProjectRole(int projectWorkId, int newProjectRoleId)
        {
            ProjectRole projectRole = db.ProjectRoles.Find(newProjectRoleId);
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork == null)
            {
                throw new NotFoundException();
            }
            projectWork.ProjectRoleId = newProjectRoleId;
            projectWork.ProjectRole = projectRole;
        }

        public void ChangeWorkLoad(int projectWorkId, int newWorkLoad)
        {
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork == null)
            {
                throw new NotFoundException();
            }
            projectWork.WorkLoad = newWorkLoad;
        }

        public void AddWorkLoad(int projectWorkId, int workLoad)
        {
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork == null)
            {
                throw new NotFoundException();
            }
            projectWork.WorkLoad = workLoad;
        }

        public void DeleteWorkLoad(int projectWorkId)
        {
            ProjectWork projectWork = db.ProjectWorks.Find(projectWorkId);
            if (projectWork == null)
            {
                throw new NotFoundException();
            }
            projectWork.WorkLoad = null;
        }
    }
}
