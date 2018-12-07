import React, { Component } from 'react';
import * as method from '../../src/services/methods';
import * as tokenService from '../../src/services/tokenService';
import * as projectWorkService from '../services/projectWorkService';
import {Table, Button, Modal, OverlayTrigger, Popover} from 'react-bootstrap';
import '../styles/UserPage.css';

class Workload extends Component{
    constructor(props){
        super(props);
        this.state = {workLoad: "---"};
    }
    componentWillMount(){
        projectWorkService.getEmployeesWorkLoad(this.props.employeeId).then(load =>{
            if(load.data!==""){
                this.setState({workLoad: load.data});
            }
        });
    }
    render(){
        return <div>{this.state.workLoad}</div>
    }
}

class NamesAndLoadList extends Component{
    constructor(props){
        super(props);
        this.state = {namesList: []};
    }
    componentWillMount(){
        projectWorkService.getNamesAndLoad(this.props.actualProject).then(res =>{
            if(res.data!==""){
                this.setState({namesList: res.data});
            }
        })
    }
    getEmployeeWorkLoad(id){
        projectWorkService.getEmployeesWorkLoad(id).then(load =>{
            if(load.data!==""){
                return load.data;
            }
        });
    }
    render(){
        if(!this.state.namesList) return <div>Загрузка...</div>
        return <div>
            <Table>
            <thead>
                <tr>
                <th>Имя</th>
                <th>Роль</th>
                <th>Загруженность</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.state.namesList.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Item1;  
                        
                        return <tr key={id}>
                            <td>
                            <OverlayTrigger trigger="click" placement="top" overlay={
                                <Popover id="popover-positioned-top" title="Общая загруженность">
                                <Workload employeeId={data.Item2}/>
                              </Popover>
                            }>
                            <Button>{data.Item3}</Button>
                            </OverlayTrigger>
                            </td>
                            <td>{data.Item4}</td>
                            <td>{data.Item5}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
    }
 
}

class NamesList extends Component{
    constructor(props){
        super(props);
        this.state = {namesList: []};
    }
    componentWillMount(){
        projectWorkService.getNames(this.props.actualProject).then(res =>{
            if(res.data!==""){
                this.setState({namesList: res.data});
            }
        })
    }
    render(){
        if(!this.state.namesList) return <div>Загрузка...</div>
        return <div>
            <Table>
            <thead>
                <tr>
                <th>Имя</th>
                <th>Роль</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.state.namesList.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Item1;  
                        
                        return <tr key={id}>
                            <td>{data.Item2}</td>
                            <td>{data.Item3}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
    }
 
}

class UserPage extends Component{
    constructor(props){
        super(props);
        this.state = {employeeId: 0, employee: null, projects: [], workLoad: "---",
    activeProject: 0, projectName: " ", namesAndLoadShow: false, namesShow: false, roleId: 0};
 
        this.renderProjects = this.renderProjects.bind(this);
        this.namesAndLoadListModalClose = this.namesAndLoadListModalClose.bind(this);
        this.namesListModalClose = this.namesListModalClose.bind(this);
        this.namesAndLoadListModalShow = this.namesAndLoadListModalShow.bind(this);
        this.namesListModalShow = this.namesListModalShow.bind(this);
        this.onShowNamesOnProject = this.onShowNamesOnProject.bind(this);
    }
    namesAndLoadListModalClose() {
        this.setState({ namesAndLoadShow: false });
      }
    namesAndLoadListModalShow() {
        this.setState({ namesAndLoadShow: true });
      }
    namesListModalClose() {
        this.setState({ namesShow: false });
      }
    namesListModalShow() {
        this.setState({ namesShow: true });
      }
    onShowNamesOnProject(id, name){
        this.setState({activeProject: id,
        projectName: name});
        if(this.state.roleId===2){
            this.namesAndLoadListModalShow();
        }
        else this.namesListModalShow();
    }
    componentWillMount(){
        var token = method.getToken();
      if(token){
          tokenService.getRoleId().then(res=>{
              if(res.data!==""){
                  this.setState({roleId: res.data});
              }
          });
        tokenService.getUser().then(res =>{
          if(res.data!==""){
              var id = JSON.parse(res.data).Id;
            this.setState({employee: res.data,
                employeeId: id});
                projectWorkService.getEmployeesProjects(id).then(proj=>{
                    if(proj.data!==""){
                        this.setState({projects: proj.data});
                    }
                });
                projectWorkService.getEmployeesWorkLoad(id).then(load =>{
                    if(load.data!==""){
                        this.setState({workLoad: load.data});
                    }
                });
          }
      });
    }
    }
    renderProjects(){
        if(this.state.projects.length===0) return <div>Проекты не найдены</div>
        else return <div id="scrolluserpage">
            <h4>Проекты</h4>
            <Table>
            <thead>
                <tr>
                <th>Проект</th>
                <th>Роль</th>
                <th>Загруженность</th>
                <th>Период работы</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.state.projects.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Item1;  
                        
                        return <tr key={id}>
                            <td>
                            <Button onClick={() => this.onShowNamesOnProject(data.Item2, data.Item3)}>
                            {data.Item3}</Button>
                            </td>
                            <td>{data.Item4}</td>
                            <td>{data.Item5}</td>
                            <td>{data.Item6}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
    }
    render(){
       if(!this.state.employee) return <div>Загрузка...</div>
       if(!this.state.roleId) return <div>Загрузка...</div>
       var employee = JSON.parse(this.state.employee);
       var git = "";
        var phone = "";
        var workLoad = this.state.workLoad;
        if(employee.GitLink===null){
            git = "---";
        }
        else git = employee.GitLink;
        if(employee.PhoneNumber===null){
            phone = "---";
        }
        else phone = employee.PhoneNumber;
       return <div id="userpage">
           <h3>{employee.EmployeeSurname + " " + employee.EmployeeName}</h3>
           <br></br>
       <div>
       <h5>E-mail: {employee.Email}</h5>
       <h5>Git: {git}</h5>
       <h5>Телефон: {phone}</h5>
       <h5>Общая загруженность: {workLoad}</h5>
       </div>
       <hr />
       {this.renderProjects()}
       <Modal show={this.state.namesAndLoadShow} onHide={this.namesAndLoadListModalClose}>
            <Modal.Header closeButton>{this.state.projectName}</Modal.Header>
                <Modal.Body>
                <NamesAndLoadList actualProject={this.state.activeProject}/>
                </Modal.Body>
        </Modal>
        <Modal show={this.state.namesShow} onHide={this.namesListModalClose}>
            <Modal.Header closeButton>{this.state.projectName}</Modal.Header>
                <Modal.Body>
                    <NamesList actualProject={this.state.activeProject}/>
                </Modal.Body>
        </Modal>
   </div>
    }
}

export default UserPage;