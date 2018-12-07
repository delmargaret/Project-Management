import React, { Component } from 'react';
import '../styles/AdminHomePage.css';
import UserPage from './UserPage';
import AdminPage from './AdminPage';
import * as method from '../services/methods';
import * as tokenService from '../services/tokenService';

class HomePage extends Component{
    constructor(props){
        super(props);
        this.state = {roleId: 0};
    }
    componentWillMount(){
        var token = method.getToken();
      if(token){
          tokenService.getRoleId().then(res=>{
              if(res.data!==""){
                  this.setState({roleId: res.data});
              }
          });
        }
    }
    render(){
        if(this.state.roleId===0) return <div>Загрузка...</div>
        if(this.state.roleId===1){
            return <AdminPage />
        }
        else return <UserPage />
    }
}
export default HomePage;
