import React, { Component } from 'react';
import {Button, Modal} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import * as projectWorkService from '../../src/services/projectWorkService';
import NamesAndLoadList from './NamesAndLoadList';
import AddEmployeeForm from './AddEmployeeOnProjectForm';
import OpenProjectList from './OpenProjectList';
import {Grid, Row, Col} from 'react-bootstrap';
import Menu from './Menu';
import "../styles/ProjectManager.css";

class ProjectManagerPage extends Component{
    constructor(props){
    super(props);
    this.state = { projects: [], employees: [], Project: null, show: false, addEmployeeShow: false};
    this.loadOpenProjects = this.loadOpenProjects.bind(this);
    this.onCloseProject = this.onCloseProject.bind(this);
    this.onOpenModal = this.onOpenModal.bind(this);
    this.handleShow = this.handleShow.bind(this);
    this.handleClose = this.handleClose.bind(this);
    this.addEmployeeModalShow = this.addEmployeeModalShow.bind(this);
    this.addEmployeeModalClose = this.addEmployeeModalClose.bind(this);
    this.onAddEmployee = this.onAddEmployee.bind(this);
    this.changeWorkLoad = this.changeWorkLoad.bind(this);
    this.deleteProjectWork = this.deleteProjectWork.bind(this);
    this.changeDay = this.changeDay.bind(this);
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
    addEmployeeModalClose() {
        this.setState({ addEmployeeShow: false });
    }
    addEmployeeModalShow() {
        this.setState({ addEmployeeShow: true });
    }
    onOpenAddEmployeeModal(){
        this.addEmployeeModalShow();
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
    changeWorkLoad(){
        this.loadEmployeesOnProject(this.state.Project.Id);
    }
    renderEmployeeList(){
        if(this.state.employees.length===0){
            return <div>Участники отсутствуют</div>     
        }
        else return <NamesAndLoadList works={this.state.employees} projectId={this.state.Project.Id} 
        changed={this.changeWorkLoad} onDeleteEmployeeFromProject={this.deleteProjectWork}
        changeDay={this.changeDay}/>
        }
    componentDidMount(){
        this.loadOpenProjects();
    }
    deleteProjectWork(id) {
        if (id) {
        projectWorkService.deleteById(id).then(res => {
            if (res !== null) {
                this.loadEmployeesOnProject(this.state.Project.Id);
                }
            });
        }
    }
    changeDay(){
        this.loadEmployeesOnProject(this.state.Project.Id);
    }
    onAddEmployee(work) {
        if (work) {
            var data = JSON.stringify({"EmployeeId":work.EmployeeId, "ProjectId":work.ProjectId,
        "ProjectRoleId": work.ProjectRoleId});
        projectWorkService.createProjectWork(data).then(res => {
            if (res !== null) {
                this.loadEmployeesOnProject(this.state.Project.Id);
                this.addEmployeeModalClose();
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
        return <Grid>
                <Row>
                <Col xs={3} md={3}>{<Menu/>}</Col>
                <Col xs={15} md={9}>
                    <div>
                        <h3>Проекты</h3>
                    {this.renderProjectList()}
                    <Modal bsSize="large" show={this.state.show} onHide={this.handleClose}>
                            <Modal.Header closeButton>{projName}</Modal.Header>
                            <Modal.Body>
                                <Button onClick={() => this.onOpenAddEmployeeModal()}>
                                    Добавить участника
                                </Button>
                                <div>
                                {this.renderEmployeeList()}
                                </div>
                            </Modal.Body>
                        </Modal>

                        <Modal  show={this.state.addEmployeeShow} onHide={this.addEmployeeModalClose}>
                            <Modal.Header closeButton></Modal.Header>
                            <Modal.Body>
                                <AddEmployeeForm projId={projId} onEmployeeSubmit={this.onAddEmployee}/>
                            </Modal.Body>
                        </Modal>
                    </div>
                </Col>
                </Row>
            </Grid>;
    }
}

export default ProjectManagerPage;