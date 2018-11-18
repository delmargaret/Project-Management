import * as method from './methods';

export function getProjects(){
    return method.get('projects/getProjects');
}
export function sortByNameAsc(){
    return method.get('projects/sortByNameAsc');
}
export function sortByNameDesc(){
    return method.get('projects/sortByNameDesc');
}
export function sortByStartDateAsc(){
    return method.get('projects/sortByStartDateAsc');
}
export function sortByStartDateDesc(){
    return method.get('projects/sortByStartDateDesc');
}
export function sortByEndDateAsc(){
    return method.get('projects/sortByEndDateAsc');
}
export function sortByEndDateDesc(){
    return method.get('projects/sortByEndDateDesc');
}
export function sortByStatusAsc(){
    return method.get('projects/sortByStatusAsc');
}
export function sortByStatusDesc(){
    return method.get('projects/sortByStatusDesc');
}
export function getProjectById(id){
    return method.get('projects/'+ id);
}
export function GetByStatusId(statusId){
    return method.get('projects/?statusId=' + statusId);
}
export function getEndingInNDays(numberOfDays){
    return method.get('projects/?numberOfDays=' + numberOfDays);
}
export function createProject(data){
    return method.post('projects', data);
}
export function deleteById(id){
    return method.del('projects/' + id);
}
export function changeName(id, projName){
    return method.put('projects/changeName/' + id, projName);
}
export function changeDescription(id, projDescription){
    return method.put('projects/changeDescription/' + id, projDescription);
}
export function changeStartDate(id, start){
    return method.put('projects/changeStartDate/' + id, start);
}
export function changeEndDate(id, end){
    return method.put('projects/changeEndDate/' + id, end);
}
export function changeProjectStatus(id, projStatusId){
    return method.put('projects/changeProjectStatus/' + id, projStatusId);
}
export function closeProject(id){
    return method.put('projects/closeProject/' + id, '');
}
