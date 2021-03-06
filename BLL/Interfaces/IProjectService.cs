﻿using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetAllProjects();
        IEnumerable<ProjectDTO> GetAllProjectsByStatusId(int statusId);
        IEnumerable<ProjectDTO> GetProjectsEndingInNDays(int numberOfDays);
        ProjectDTO GetProjectById(int id);
        ProjectDTO CreateProject(ProjectDTO item);
        void DeleteProjectById(int id);
        void ChangeProjectName(int projectId, string newProjectName);
        void ChangeProjectDescription(int projectId, string newProjectDescription);
        void ChangeProjectStartDate(int projectId, DateTimeOffset newStartDate);
        void ChangeProjectEndDate(int projectId, DateTimeOffset newEndDate);
        void ChangeProjectStatus(int projectId, int projectStatusId);
        void CloseProject(int projectId);
        void Dispose();
    }
}
