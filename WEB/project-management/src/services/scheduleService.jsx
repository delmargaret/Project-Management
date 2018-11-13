import * as method from './methods';

export function getSchedules(){
    return method.get('schedules');
}
export function getScheduleById(id){
    return method.get('schedules/'+ id);
}
export function getScheduleOnProjectWork(projectWorkId){
    return method.get('schedules/?projectWorkId=' + projectWorkId);
}
export function getEmployeesFreeDays(employeeId){
    return method.get('schedules/?employeeId=' + employeeId);
}
export function createSchedule(data){
    return method.post('schedules', data);
}
export function deleteById(id){
    return method.del('schedules/' + id);
}
export function deleteScheduleByProjectWorkId(projectWorkId){
    return method.del('schedules/?projectWorkId=' + projectWorkId);
}
export function changeScheduleDay(id, scheduleDayId){
    return method.put('schedules/changeScheduleDay/' + id, scheduleDayId);
}
