import * as method from './methods';

export function getRoles(){
    return method.get('projectRoles');
}
export function getRoleById(id){
    return method.get('projectRoles/'+ id);
}