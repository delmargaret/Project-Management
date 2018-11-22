import React, { Component } from 'react';
import {Button} from 'react-bootstrap';
import '../styles/AdminHomePage.css';
import UserPage from './UserPage';
import Menu from './Menu';
import * as method from '../../src/services/methods';
import * as tokenService from '../../src/services/tokenService';
import {Grid, Row, Col} from 'react-bootstrap';


class AdminHomePage extends Component{

      render(){
          return(
            <Grid>
            <Row>
              <Col xs={3} md={3}>{<Menu/>}</Col>
              <Col xs={15} md={9}>
              <div className="span10" id="homepage">
              <h1>Добро пожаловать!</h1>
                  <div id="buttons">
                    <div className="button" id="staffButton">
                        <Button bsSize="large" href="/staff">Добавить сотрудника</Button>
                    </div>
                    <div className="button" id="projectsButton">
                        <Button bsSize="large" href="/projects">Добавить проект</Button>
                    </div>
                  </div>
              </div>
              </Col>
            </Row>
          </Grid>
          );
      };
}

class EmployeeHomePage extends Component{

    render(){
        return(
            <UserPage />
        );
    };
}

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
            return <AdminHomePage />
        }
        else return <EmployeeHomePage />
    }
}
export default HomePage;
