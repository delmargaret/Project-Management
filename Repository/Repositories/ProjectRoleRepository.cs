﻿using System;
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
    public class ProjectRoleRepository : IProjectRoleRepository
    {
        private ManagementContext db;

        public ProjectRoleRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProjectRole> GetAllProjectRoles()
        {
            return db.ProjectRoles;
        }

        public ProjectRole GetProjectRoleById(int id)
        {
            return db.ProjectRoles.Find(id);
        }

        public void CreateProjectRole(ProjectRole projectrole)
        {
            db.ProjectRoles.Add(projectrole);
        }

        public void UpdateProjectRole(ProjectRole projectrole)
        {
            db.Entry(projectrole).State = EntityState.Modified;
        }

        public IEnumerable<ProjectRole> FindProjectRole(Func<ProjectRole, Boolean> predicate)
        {
            return db.ProjectRoles.Where(predicate).ToList();
        }

        public void DeleteProjectRole(int id)
        {
            ProjectRole projectrole = db.ProjectRoles.Find(id);
            if (projectrole != null)
                db.ProjectRoles.Remove(projectrole);
        }
    }
}
