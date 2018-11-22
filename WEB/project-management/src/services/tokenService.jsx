import * as method from './methods';

export function login(login, password){
    var data = JSON.stringify({"Login": login, "Password":password});
    return method.post('token/authenticate', data);
}
export function getRoleId()
{
    return method.get('token/getRoleId');
}
export function getUser()
{
    return method.get('token/getEmployee');
}
export function registrate(login, password){
    var data = JSON.stringify({"Login": login, "Password":password});
    return method.post('token/registrate', data);
}
export function changePassword(login, password){
    var data = JSON.stringify({"Login": login, "Password":password});
    return method.post('token/ChangePassword', data).then(res =>{
        var token = JSON.parse(res.data);
        method.setToken(token);
    });
}