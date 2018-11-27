import * as method from './methods';

export function getHistories(){
    return method.get('participationHistories');
}
export function getHistoryById(id){
    return method.get('participationHistories/'+ id);
}
export function getEmployeesHistoriesOnProject(projWorkId){
    return method.get('participationHistories/?projWorkId=' + projWorkId);
}
export function getEmployeesHistory(projectWorkId){
    return method.get('participationHistories/?projectWorkId=' + projectWorkId);
}
export function createHistory(data){
    return method.post('participationHistories', data);
}
export function deleteById(id){
    return method.del('participationHistories/' + id);
}
export function changeHistoryStartDate(id, start){
    return method.put('participationHistories/changeHistoryStartDate/' + id, start);
}
export function changeHistoryEndDate(id, end){
    return method.put('participationHistories/changeHistoryEndDate/' + id, end);
}
