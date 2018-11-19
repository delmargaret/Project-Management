import * as method from './methods';

export function getEmployees(){
    return method.get('employees/getAllEmployees');
}
export function getEmployeesNotOnProject(projectId){
    return method.get('employees/?projectId=' + projectId);
}
export function sortBySurnameAsc(){
    return method.get('employees/sortBySurnameAsc');
}
export function sortBySurnameDesc(){
    return method.get('employees/sortBySurnameDesc');
}
export function sortByRoleAsc(){
    return method.get('employees/sortByRoleAsc');
}
export function sortByRoleDesc(){
    return method.get('employees/sortByRoleDesc');
}
export function getEmployeeById(id){
    return method.get('employees/'+ id);
}
export function getByEmail(email){
    return method.get('employees/?email=' + email);
}
export function getBySurname(surname){
    return method.get('employees/?surname=' + surname);
}
export function getByRoleId(roleId){
    return method.get('employees/?roleId=' + roleId);
}
export function createEmployee(data){
    return method.post('employees', data);
}
export function deleteById(id){
    return method.del('employees/' + id);
}
export function deleteByEmail(email){
    return method.del('employees/?email=' + email);
}
export function addGitLink(id, git){
    return method.put('employees/addGitLink/' + id, git);
}
export function deleteGitLink(id){
    return method.put('employees/addGitLink/' + id, '');
}
export function addPhoneNumber(id, number){
    return method.put('employees/addPhoneNumber/' + id, number);
}
export function deletePhoneNumber(id){
    return method.put('employees/deletePhoneNumber/' + id, '');
}
export function changeName(id, name){
    return method.put('employees/changeName/' + id, name);
}
export function changeSurname(id, surname){
    return method.put('employees/changeSurname/' + id, surname);
}
export function changePatronymic(id, patronymic){
    return method.put('employees/changePatronymic/' + id, patronymic);
}
export function changeEmail(id, email){
    return method.put('employees/changeEmail/' + id, email);
}
export function changeGitLink(id, git){
    return method.put('employees/changeGitLink/' + id, git);
}
export function changePhoneNumber(id, number){
    return method.put('employees/changePhoneNumber/' + id, number);
}
export function changeRole(id, roleId){
    return method.put('employees/changeRole/' + id, roleId);
}
export function changeWorkLoad(id, loadType){
    return method.put('employees/changeWorkLoad/' + id, loadType);
}