import * as method from './methods';

export function getRoles(){
    return method.get('roles');
}