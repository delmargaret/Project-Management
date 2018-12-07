import React, { Component } from 'react';
import {Button, FormGroup, FormControl, Form, Modal} from 'react-bootstrap';
import * as projectService from '../services/projectService';
import ProjectList from './ProjectList';
import AddProjectForm from './AddProjectForm';
import "../styles/AddProject.css";
import {Grid, Row, Col} from 'react-bootstrap';
import Menu from './Menu';

class AddProjectPage extends Component{
    constructor(props){
        super(props);
        this.state = { projects: [], show: false, sortId: 0};
        this.onAddProject = this.onAddProject.bind(this);
        this.loadProjects = this.loadProjects.bind(this);
        this.onClick = this.onClick.bind(this);
        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.deleteProject = this.deleteProject.bind(this);
        this.onAddNewName = this.onAddNewName.bind(this);
        this.onAddNewDescription = this.onAddNewDescription.bind(this);
        this.onAddNewStartDate = this.onAddNewStartDate.bind(this);
        this.onAddNewEndDate = this.onAddNewEndDate.bind(this);
        this.sortByNameAsc = this.sortByNameAsc.bind(this);
        this.sortByNameDesc = this.sortByNameDesc.bind(this);
        this.sortByStartDateAsc = this.sortByStartDateAsc.bind(this);
        this.sortByStartDateDesc = this.sortByStartDateDesc.bind(this);
        this.sortByEndDateAsc = this.sortByEndDateAsc.bind(this);
        this.sortByEndDateDesc = this.sortByEndDateDesc.bind(this);
        this.sortByStatusAsc = this.sortByStatusAsc.bind(this);
        this.sortByStatusDesc = this.sortByStatusDesc.bind(this);
        this.onSortChange = this.onSortChange.bind(this);
        this.onSort = this.onSort.bind(this);
    }
    handleClose() {
        this.setState({ show: false });
      }
    handleShow() {
        this.setState({ show: true });
      }
    loadProjects() {
        projectService.getProjects().then(res => { 
            if(res!==null) this.setState({projects: res.data}) }).catch(error => {
                console.log(error);

                if (error) {
                    return null;
                  }
            });
        }
    sortByNameAsc(){
        projectService.sortByNameAsc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    sortByNameDesc(){
        projectService.sortByNameDesc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    sortByStartDateAsc(){
        projectService.sortByStartDateAsc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    sortByStartDateDesc(){
        projectService.sortByStartDateDesc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    sortByEndDateAsc(){
        projectService.sortByEndDateAsc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    sortByEndDateDesc(){
        projectService.sortByEndDateDesc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    sortByStatusAsc(){
        projectService.sortByStatusAsc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    sortByStatusDesc(){
        projectService.sortByStatusDesc().then(res => { this.setState({projects: res.data}) }).catch(error => {
            if (error.response) {
                return null;
                }
        });
    }
    onSortChange(e){
        this.setState({sortId: e.target.value});
    }
    onSort(){
        var sortId = this.state.sortId;
        if(sortId==="1"){
            this.sortByNameAsc();
        }
        if(sortId==="2"){
            this.sortByNameDesc();
        }
        if(sortId==="3"){
            this.sortByStartDateAsc();
        }
        if(sortId==="4"){
            this.sortByStartDateDesc();
        }
        if(sortId==="5"){
            this.sortByEndDateAsc();
        }
        if(sortId==="6"){
            this.sortByEndDateDesc();
        }
        if(sortId==="7"){
            this.sortByStatusAsc();
        }
        if(sortId==="8"){
            this.sortByStatusDesc();
        }
        else this.loadProjects();
    }
    onAddNewName(id, name){
            if (name) {
                var projName = JSON.stringify(name);
            projectService.changeName(id, projName).then(res => {
                if (res) {
                    this.loadProjects();
                    }
                });
            }
    }
    onAddNewDescription(id, description){
            if (description) {
                var projDescription = JSON.stringify(description);
            projectService.changeDescription(id, projDescription).then(res => {
                if (res) {
                    this.loadProjects();
                    }
                });
            }
    }
    onAddNewStartDate(id, start){
            if (start) {
                var projStart = JSON.stringify(start);
            projectService.changeStartDate(id, projStart).then(res => {
                if (res) {
                    this.loadProjects();
                    }
                });
            }
    }
    onAddNewEndDate(id, end){
            if (end) {
                var projEnd = JSON.stringify(end);
            projectService.changeEndDate(id, projEnd).then(res => {
                if (res) {
                    this.loadProjects();
                    }
                });
            }
    }
    renderProjectList(){
        if(this.state.projects.length===0){
            return <div>Проекты не найдены</div>     
        }
        else return <div>
            <Form id="sortProjectsSelect">
                    <FormGroup>
                        <FormControl componentClass="select" value={this.state.sortId}
                        onChange={this.onSortChange} onClick={() => this.onSort()}>
                        <option value={0}>Сортировать по</option>
                        <option value={1}>Названию(по возрастанию)</option>
                        <option value={2}>Названию(по убыванию)</option>
                        <option value={3}>Дате начала(по возрастанию)</option>
                        <option value={4}>Дате начала(по убыванию)</option>
                        <option value={5}>Дате окончания(по возрастанию)</option>
                        <option value={6}>Дате окончания(по убыванию)</option>
                        <option value={7}>Статусу(сначала открытые)</option>
                        <option value={8}>Статусу(сначала закрытые)</option>
                        </FormControl>
                    </FormGroup>
                    </Form>
                    <ProjectList proj={this.state.projects} onDeleteProject={this.deleteProject}
        onAddName={this.onAddNewName} onAddDescription={this.onAddNewDescription}
        onAddStartDate={this.onAddNewStartDate} onAddEndDate={this.onAddNewEndDate}/>
        </div>
    }
    componentDidMount(){
        this.loadProjects();
    }
    onAddProject(project) {
        if (project) {
            var data = JSON.stringify({"ProjectName":project.name, "ProjectDescription":project.description,
        "ProjectStartDate": project.startDate, "ProjectEndDate": project.endDate});
        projectService.createProject(data).then(res => {
            if (res !== null) {
                this.loadProjects();
                this.handleClose();
                }
            });
        }
    }
    deleteProject(id) {
        if (id) {
        projectService.deleteById(id).then(res => {
            if (res !== null) {
                this.loadProjects();
                }
            });
        }
    }
    onClick(){
        this.handleShow();
    }
    render(){ 
        return <Grid>
                    <Row>
                    <Col xs={3} md={3}>{<Menu/>}</Col>
                    <Col xs={15} md={9}>
                    <div>
                        <h2>Добавить проект</h2>
                        <div>
                            <Button  bsSize="large" id="addprojectbtn" onClick={() => this.onClick()}>Новый проект</Button>
                        </div>
                        <div>
                            <h3>Проекты</h3>
                        {this.renderProjectList()}
                        </div>
                            <Modal show={this.state.show} onHide={this.handleClose}>
                                <Modal.Header closeButton></Modal.Header>
                                <Modal.Body>
                                    <AddProjectForm onProjectSubmit={this.onAddProject}/>
                                </Modal.Body>
                            </Modal>
                    </div>
                    </Col>
                    </Row>
                </Grid>;
     }
}

export default AddProjectPage;