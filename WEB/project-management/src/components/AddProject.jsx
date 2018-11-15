import React, { Component } from 'react';
import {Table, Button, FormGroup, FormControl, Form, Modal} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import "./AddProject.css";

class ProjectList extends Component{
    render(){ 
        return <div id="scroll">
            <Table>
            <thead>
                <tr>
                <th>Название</th>
                <th>Описание</th>
                <th>Дата начала</th>
                <th>Дата окончания</th>
                <th>Статус</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.proj.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Id;  
                        var statusname = "";
                        if(data.ProjectStatusId===1){statusname="Открыт";}
                        if(data.ProjectStatusId===2){statusname="Закрыт";}
                        return <tr key={id}>
                            <td>{data.ProjectName}</td>
                            <td>{data.ProjectDescription}</td>
                            <td>{data.ProjectStartDate}</td>
                            <td>{data.ProjectEndDate}</td>
                            <td>{statusname}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
            }
}

class AddProjectForm extends Component{
    constructor(props){
        super(props);
        this.state = {name: "", description: "", startDate: "",
        endDate: "", projects: []};

        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onStartDateChange = this.onStartDateChange.bind(this);
        this.onEndDateChange = this.onEndDateChange.bind(this);
    }
    validateName(){
        if (this.state.name.length > 2) return 'success';
        if (this.state.name.length > 0) return 'error';
        if (this.state.name.length > 25) return 'error';
        return null;
    }
    validateDescription(){
        if (this.state.description.length > 5) return 'success';
        if (this.state.description.length > 0) return 'error';
        if (this.state.description.length > 1024) return 'error';
        return null;
    }
 
    validateEndDate(){
        if (Date.parse(this.state.endDate)>Date.parse(this.state.startDate)) return 'success';
        if (Date.parse(this.state.endDate)<Date.parse(this.state.startDate)) return 'error';
        if (Date.parse(this.state.endDate)>Date.now()) return 'success';
        if (Date.parse(this.state.endDate)<Date.now()) return 'error';
    }
    onNameChange(e) {
        var val = e.target.value;
        this.setState({name: val});
    }
    onDescriptionChange(e) {
        var val = e.target.value;
        this.setState({description: val});
    }
    onStartDateChange(e) {
        var val = e.target.value;
        this.setState({startDate: val});
    }
    onEndDateChange(e) {
        var val = e.target.value;
        this.setState({endDate: val});
    }

    onSubmit(e) {
        e.preventDefault();
        var name = this.state.name.trim();
        var description = this.state.description.trim();
        var startDate = this.state.startDate;
        var endDate = this.state.endDate;

        if (!name || !description || !startDate || !endDate) {
            return;
        }
        this.props.onProjectSubmit({ name: name, description: description,
            startDate: startDate, endDate: endDate});
            this.setState({name: "", description: "", startDate: "",
            endDate: ""});
    }
    render() {
            return (
                    <Form  onSubmit={this.onSubmit}>
                    <FormGroup controlId="formBasicName"
                     validationState={this.validateName()}>
                        <FormControl
                            type="text"
                            placeholder="Название проекта"
                            value={this.state.name}
                            onChange={this.onNameChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup controlId="formBasicDescription"
                     validationState={this.validateDescription()}>
                        <FormControl
                            componentClass="textarea"
                            placeholder="Описание проекта"
                            value={this.state.description}
                            onChange={this.onDescriptionChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup controlId="formBasicStart"
                     validationState={this.validateEndDate()}>
                        <FormControl
                            type="date"
                            placeholder="Дата начала"
                            value={this.state.startDate}
                            onChange={this.onStartDateChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup controlId="formBasicEnd"
                     validationState={this.validateEndDate()}>
                        <FormControl
                            type="date"
                            placeholder="Дата окончания"
                            value={this.state.endDate}
                            onChange={this.onEndDateChange} />
                        <FormControl.Feedback />
                    </FormGroup>
                <Button type="submit">Добавить</Button>
                    </Form>                
            );        
    }
}

class AddProjectPage extends Component{
    constructor(props){
        super(props);
        this.state = { projects: [], show: false};
        this.onAddProject = this.onAddProject.bind(this);
        this.loadProjects = this.loadProjects.bind(this);
        this.onClick = this.onClick.bind(this);
        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
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
                if (error.response) {
                    return null;
                  }
            });
        }
    renderProjectList(){
        if(this.state.projects.length===0){
            return <div>Проекты не найдены</div>     
        }
        else return <ProjectList proj={this.state.projects}/>
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
                }
            });
        }
    }
    onClick(){
        this.handleShow();
    }
    render(){ 
        return <div>
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
        </div>;
     }
}

export default AddProjectPage;