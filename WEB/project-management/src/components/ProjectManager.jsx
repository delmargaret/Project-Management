import React, { Component } from 'react';
import {Table, Button, Modal, FormGroup, FormControl} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import * as projectWorkService from '../../src/services/projectWorkService';
import * as projectRoleService from '../../src/services/projectRoleService';
import * as employeeService from '../../src/services/employeeService';
import "./ProjectManager.css";

class NamesAndLoadList extends Component{
    render(){ 
        return <div id="namesandloadscroll">
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
                    this.props.works.map((employee) => {  
                        var data = JSON.parse(employee); 
                        var id = data.Item1;  
                        return <tr key={id}>
                            <td>{data.Item2}</td>
                            <td>{data.Item3}</td>
                            <td>{data.Item4}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
            }
}

class OpenProjectList extends Component{
    render(){ 
        return <div id="scrollpm">
            <Table>
            <thead>
                <tr>
                <th>Название</th>
                <th>Описание</th>
                <th>Дата начала</th>
                <th>Дата окончания</th>
                <th></th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.proj.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Id;  
                        return <tr key={id}>
                            <td>
                                <Button onClick={() => this.props.openModal(data)}>
                                {data.ProjectName}
                                </Button>
                            </td>
                            <td>{data.ProjectDescription}</td>
                            <td>{data.ProjectStartDate}</td>
                            <td>{data.ProjectEndDate}</td>
                            <td><Button onClick={() => this.props.onCl(id)}>
                                Завершить проект</Button>
                            </td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
            }
}

class AddEmployeeForm extends Component{
 
    constructor(props){
        super(props);
        this.state = {employeeId: 0, projectRoleId: 0, employees: [], roles: []};
 
        this.onSubmit = this.onSubmit.bind(this);
        this.onEmployeeIdChange = this.onEmployeeIdChange.bind(this);
        this.onProjectRoleIdChange = this.onProjectRoleIdChange.bind(this);
        this.loadEmployees = this.loadEmployees.bind(this);
        this.loadRoles = this.loadRoles.bind(this);     
    }
    validateEmployee() {
        if (this.state.employeeId !== 0) return 'success';
        if (this.state.employeeId === 0) return 'error';
        return null;
      }
    validateRole() {
        if (this.state.projectRoleId !== 0) return 'success';
        if (this.state.projectRoleId === 0) return 'error';
        return null;
      }
    onEmployeeIdChange(e) {
        this.setState({employeeId: e.target.value});
    }
    onProjectRoleIdChange(e) {
        this.setState({projectRoleId: e.target.value});
    }
    loadEmployees(){
        employeeService.getEmployees().then(res => { this.setState({employees: res.data}) }).catch(error => {
            if (error.response) {
                return null;
              }
        });
    }
    loadRoles(){
        projectRoleService.getRoles().then(res => { this.setState({roles: res.data}) }).catch(error => {
            if (error.response) {
                return null;
              }
        });
    }
    componentDidMount(){
        this.loadEmployees();
        this.loadRoles();
    }
    onSubmit(e) {
        e.preventDefault();
        var employeeId = this.state.employeeId;
        var projectRoleId = this.state.projectRoleId;
        var projectId = this.props.projId;

        if (!employeeId || !projectRoleId || !projectId) {
            return;
        }
        this.props.onEmployeeSubmit({ EmployeeId: employeeId, ProjectId: projectId,
            ProjectRoleId: projectRoleId});
            this.setState({employeeId: 0, projectId: 0, projectRoleId: 0});
    }
    render() {
        return (
          <form onSubmit={this.onSubmit}>
                    <FormGroup id="formControlsSelect1" validationState={this.validateEmployee()}>
                        <FormControl componentClass="select" onChange={this.onEmployeeIdChange}>
                        <option>Выберите сотрудника</option>
                            {
                                this.state.employees.map((employee) => {  
                                var data = JSON.parse(employee);
                                var id = data.Id;  
                                return <option key={id} value={id} >
                                {data.EmployeeSurname + " " + data.EmployeeName}
                                </option>
                                }) 
                            }  
                        </FormControl>
                    </FormGroup>

                    <FormGroup id="formControlsSelect2" validationState={this.validateRole()}>
                        <FormControl componentClass="select" onChange={this.onProjectRoleIdChange}>
                        <option>Выберите роль</option>
                            {
                                this.state.roles.map((role) => { 
                                var data = JSON.parse(role);
                                var id = data.Id;  
                                return <option key={id} value={id} >
                                {data.ProjectRoleName}
                                </option>
                                }) 
                            }
                        </FormControl>
                    </FormGroup>
            <Button type="submit">Добавить</Button>
          </form>
        );
    }
}

class ProjectManagerPage extends Component{
    constructor(props){
        super(props);
        this.state = { projects: [], employees: [], Project: null, show: false, show2: false};
        this.loadOpenProjects = this.loadOpenProjects.bind(this);
        this.onCloseProject = this.onCloseProject.bind(this);
        this.onOpenModal = this.onOpenModal.bind(this);
        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleShow2 = this.handleShow2.bind(this);
        this.handleClose2 = this.handleClose2.bind(this);
        this.onAddEmployee = this.onAddEmployee.bind(this);
    }
    handleClose() {
        this.setState({ show: false });
      }
    handleShow() {
        this.setState({ show: true });
      }
    onOpenModal(proj){
        if(proj){
            this.setState({Project: proj});
            this.loadEmployeesOnProject(proj.Id);
            this.handleShow();
        }
    }
    handleClose2() {
        this.setState({ show2: false });
      }
    handleShow2() {
        this.setState({ show2: true });
      }
    onOpenModal2(){
            this.handleShow2();
    }
    loadEmployeesOnProject(id){
        projectWorkService.getNamesAndLoad(id).then(res => { 
            if(res!==null) this.setState({employees: res.data}) }).catch(error => {
                if (error.response) {
                    return null;
                  }
            });
    }
    loadOpenProjects() {
        projectService.GetByStatusId(1).then(res => { 
            if(res!==null) this.setState({projects: res.data}) }).catch(error => {
                if (error.response) {
                    return null;
                  }
            });
        }
    renderProjectList(){
        if(this.state.projects.length===0){
            return <div>Проекты не найдены</div>     
        }
        else return <OpenProjectList proj={this.state.projects} onCl={this.onCloseProject} 
            openModal={this.onOpenModal}/>
    }
    renderEmployeeList(){
        if(this.state.employees.length===0){
            return <div>Участники отсутствуют</div>     
        }
        else return <NamesAndLoadList works={this.state.employees}/>
    }
    componentDidMount(){
        this.loadOpenProjects();
    }
    onAddEmployee(work) {
        if (work) {
            console.log(work);
            var data = JSON.stringify({"EmployeeId":work.EmployeeId, "ProjectId":work.ProjectId,
        "ProjectRoleId": work.ProjectRoleId});
        projectWorkService.createProjectWork(data).then(res => {
            if (res !== null) {
                this.loadEmployeesOnProject(this.state.Project.Id);
                }
            });
        }
    }
    onCloseProject(id) {
        if (id) {
        projectService.closeProject(id).then(res => {
            if (res !== null) {
                this.loadOpenProjects();
                }
            });
        }
    }
    render(){ 
        var projName = "";
        var projId = 0;
        if(this.state.Project!==null){projName=this.state.Project.ProjectName;
        projId = this.state.Project.Id;}
        return <div>
                    <h3>Проекты</h3>
                {this.renderProjectList()}
                <Modal bsSize="large" show={this.state.show} onHide={this.handleClose}>
                        <Modal.Header closeButton>{projName}</Modal.Header>
                        <Modal.Body>
                            <Button onClick={() => this.onOpenModal2()}>
                                Добавить участника
                            </Button>
                            <div>
                            {this.renderEmployeeList()}
                            </div>
                        </Modal.Body>
                    </Modal>

                    <Modal  show={this.state.show2} onHide={this.handleClose2}>
                        <Modal.Header closeButton></Modal.Header>
                        <Modal.Body>
                            <AddEmployeeForm projId={projId} onEmployeeSubmit={this.onAddEmployee}/>
                        </Modal.Body>
                    </Modal>
        </div>;
     }
}

export default ProjectManagerPage;