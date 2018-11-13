import axios from 'axios';

const host = 'http://localhost:12124/';

export function get(url){
    var getUrl = host + url;
    let options = {
        url: getUrl,
        method: 'GET',
        type: 'json',
        headers: {"Content-Type": "application/json"}
    }
    const response = axios(options);
    return response;
}

export function post(url, data){
    var postUrl = host + url;
    const options = {
        url: postUrl,
        method: 'POST',
        data: data,
        headers: {"Content-Type": "application/json"}
    }
    const response = axios(options).catch((error) => {
        if (error.response) {
            console.log(error.response.data);
            return null;
          }
    });
    return response;
}

export function del(url){
    var deleteUrl = host + url;
    const options = {
        url: deleteUrl,
        method: 'DELETE',
        headers: {"Content-Type": "application/json"}
    }
    const response = axios(options).catch((error) => {
        if (error.response) {
            console.log(error.response.data);
            return null;
          }
    });
    return response;
}

export function put(url, data){
    var putUrl = host + url;
    const options = {
        url: putUrl,
        method: 'PUT',
        data: data,
        headers: {"Content-Type": "application/json"}
    }
    const response = axios(options).catch((error) => {
        if (error.response) {
            console.log(error.response.data);
            return null;
          }
    });
    return response;
}