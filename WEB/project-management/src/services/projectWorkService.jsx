import * as method from './methods';

export function getProjectWorks(){
    return method.get('projectWorks');
}
export function getProjectWorkById(id){
    return method.get('projectWorks/'+ id);
}
export function getNames(projId){
    return method.get('projectWorks/?projId=' + projId);
}
export function getNamesAndLoad(projectId){
    return method.get('projectWorks/?projectId=' + projectId);
}
export function getEmployeesProjects(empId){
    return method.get('projectWorks/?empId=' + empId);
}
export function getWorkLoadType(projectWorkId){
    return method.get('projectWorks/?projectWorkId=' + projectWorkId);
}
export function getEmployeesOnProject(prjctId){
    return method.get('projectWorks/?prjctId=' + prjctId);
}
export function calculateEmployeesWorkload(employeeId){
    return method.get('projectWorks/?employeeId=' + employeeId);
}
export function createProjectWork(data){
    return method.post('projectWorks', data);
}
export function deleteById(id){
    return method.del('projectWorks/' + id);
}
export function deleteEmployeeFromProject(prId, emId){
    return method.del('projectWorks/?prId=' + prId + '&&emId=' + emId);
}
export function changeProject(id, projectId){
    return method.put('projectWorks/changeProject/' + id, projectId);
}
export function changeEmployee(id, employeeId){
    return method.put('projectWorks/changeEmployee/' + id, employeeId);
}
export function changeEmployeesProjectRole(id, projectRoleId){
    return method.put('projectWorks/changeEmployeesProjectRole/' + id, projectRoleId);
}
export function changeWorkLoad(id, newWorkload){
    return method.put('projectWorks/changeWorkLoad/' + id, newWorkload);
}
export function addWorkLoad(id, workload){
    return method.put('projectWorks/addWorkLoad/' + id, workload);
}
export function deleteWorkLoad(id){
    return method.put('projectWorks/deleteWorkLoad/' + id, '');
}