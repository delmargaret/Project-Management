import React, { Component } from 'react';
import {Table, Button, FormGroup, FormControl, Form, Modal, ControlLabel} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import "./AddProject.css";

class ChangeProjectForm extends Component{
    _isMounted = false;
    get isMounted() {
        return this._isMounted;
    }
    set isMounted(value) {
        this._isMounted = value;
    }
    constructor(props){
        super(props);
        this.state = {name: "", description: "", startDate: "",
        endDate: "", newname: "", newdescription: "", newstartDate: "",
        newendDate: "", show1: false, show2: false, show3: false, show4: false};

        this.onNameChange = this.onNameChange.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onStartDateChange = this.onStartDateChange.bind(this);
        this.onEndDateChange = this.onEndDateChange.bind(this);
        this.onChangeProjectName = this.onChangeProjectName.bind(this);
        this.handleShow1 = this.handleShow1.bind(this);
        this.handleClose1 = this.handleClose1.bind(this);
        this.onNameSubmit = this.onNameSubmit.bind(this);
        this.onChangeProjectDescription = this.onChangeProjectDescription.bind(this);
        this.handleShow2 = this.handleShow2.bind(this);
        this.handleClose2 = this.handleClose2.bind(this);
        this.onDescriptionSubmit = this.onDescriptionSubmit.bind(this);
        this.onChangeProjectStartDate = this.onChangeProjectStartDate.bind(this);
        this.handleShow3 = this.handleShow3.bind(this);
        this.handleClose3 = this.handleClose3.bind(this);
        this.onStartDateSubmit = this.onStartDateSubmit.bind(this);
        this.onChangeProjectEndDate = this.onChangeProjectEndDate.bind(this);
        this.handleShow4 = this.handleShow4.bind(this);
        this.handleClose4 = this.handleClose4.bind(this);
        this.onEndDateSubmit = this.onEndDateSubmit.bind(this);
    }
    handleClose1() {
        this.setState({ show1: false });
      }
    handleShow1() {
        this.setState({ show1: true });
      }
    onChangeProjectName(){
        this.handleShow1();
    }
    handleClose2() {
        this.setState({ show2: false });
      }
    handleShow2() {
        this.setState({ show2: true });
      }
    onChangeProjectDescription(){
        this.handleShow2();
    }
    handleClose3() {
        this.setState({ show3: false });
      }
    handleShow3() {
        this.setState({ show3: true });
      }
    onChangeProjectStartDate(){
        this.handleShow3();
    }
    handleClose4() {
        this.setState({ show4: false });
      }
    handleShow4() {
        this.setState({ show4: true });
      }
    onChangeProjectEndDate(){
        this.handleShow4();
    }
    validateName(){
        if (this.state.newname.length > 2) return 'success';
        if (this.state.newname.length > 0) return 'error';
        if (this.state.newname.length > 25) return 'error';
        return null;
    }
    validateDescription(){
        if (this.state.newdescription.length > 5) return 'success';
        if (this.state.newdescription.length > 0) return 'error';
        if (this.state.newdescription.length > 1024) return 'error';
        return null;
    }
    validateStartDate(){
        if (Date.parse(this.state.newendDate)>Date.parse(this.state.newstartDate)) return 'success';
        if (Date.parse(this.state.endDate)>Date.parse(this.state.newstartDate)) return 'success';
        if (Date.parse(this.state.endDate)<Date.parse(this.state.newstartDate)) return 'error';
        if (Date.parse(this.state.newendDate)<Date.parse(this.state.newstartDate)) return 'error';
    }
    validateEndDate(){
        if (Date.parse(this.state.newendDate)<Date.now()) return 'error';
        if (Date.parse(this.state.newendDate)>Date.parse(this.state.newstartDate)) return 'success';
        if (Date.parse(this.state.newendDate)>Date.parse(this.state.startDate)) return 'success';
        if (Date.parse(this.state.newendDate)<Date.parse(this.state.newstartDate)) return 'error';
        if (Date.parse(this.state.newendDate)<Date.parse(this.state.startDate)) return 'error';
        if (Date.parse(this.state.newendDate)>Date.now()) return 'success';
    }
    onNameChange(e) {
        var val = e.target.value;
        this.setState({newname: val});
    }
    onDescriptionChange(e) {
        var val = e.target.value;
        this.setState({newdescription: val});
    }
    onStartDateChange(e) {
        var val = e.target.value;
        this.setState({newstartDate: val});
    }
    onEndDateChange(e) {
        var val = e.target.value;
        this.setState({newendDate: val});
    }
    componentDidMount(){ 
        projectService.getProjectById(this.props.proj.Id).then(res =>{
            if(res!==null){
                var proj = JSON.parse(res.data);
                this.setState({project: proj, name: proj.ProjectName, 
                    description: proj.ProjectDescription, startDate: proj.ProjectStartDate,
                endDate: proj.ProjectEndDate});
            }
        });
        this.isMounted = true;
    }
    componentWillUnmount(){
        this.isMounted = false;
    }
    onNameSubmit(e) {
        e.preventDefault();
        var name = this.state.newname.trim();
        this.setState({name: name});
        var projectId = this.props.proj.Id;
        if (!name) {
            return;
        }
        this.props.onSubmitName(projectId, name);
            this.setState({newname: ""});
            this.handleClose1();
    }
    onDescriptionSubmit(e) {
        e.preventDefault();
        var description = this.state.newdescription.trim();
        this.setState({description: description});
        var projectId = this.props.proj.Id;
        if (!description) {
            return;
        }
        this.props.onSubmitDescription(projectId, description);
            this.setState({newdescription: ""});
            this.handleClose2();
    }
    onStartDateSubmit(e) {
        e.preventDefault();
        var start = this.state.newstartDate;
        var startdate = start.toString().replace(/^(\d+)-(\d+)-(\d+)+$/, '$3.$2.$1'); 
        this.setState({startDate: startdate});
        var projectId = this.props.proj.Id;
        if (!start) {
            return;
        }
        this.props.onSubmitStartDate(projectId, start);
            this.setState({newstartDate: ""});
            this.handleClose3();
    }
    onEndDateSubmit(e) {
        e.preventDefault();
        var end = this.state.newendDate;
        var enddate = end.toString().replace(/^(\d+)-(\d+)-(\d+)+$/, '$3.$2.$1'); 
        this.setState({endDate: enddate});
        var projectId = this.props.proj.Id;
        if (!end) {
            return;
        }
        this.props.onSubmitEndDate(projectId, end);
            this.setState({newendDate: ""});
            this.handleClose4();
    }
    render(){ 
        var project = this.state.project;
        if(!project) return <div>Загрузка...</div>
        var id = project.Id;
        var start = this.state.startDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
        var end = this.state.endDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
        return <div>
            <Table>
            <thead>
            </thead>
                <tbody>
                        <tr>
                            <td>{this.state.name}</td>
                            <td><Button onClick={() => this.onChangeProjectName()}>
                            Изменить</Button>
                            </td>
                        </tr>
                        <tr>
                            <td>{this.state.description}</td>
                            <td><Button onClick={() => this.onChangeProjectDescription()}>
                            Изменить</Button>
                            </td>
                        </tr>
                        <tr>
                            <td>{start}</td>
                            <td><Button onClick={() => this.onChangeProjectStartDate()}>
                            Изменить</Button>
                            </td>
                        </tr>
                        <tr>
                            <td>{end}</td>
                            <td><Button onClick={() => this.onChangeProjectEndDate(id)}>
                            Изменить</Button>
                            </td>
                        </tr>
                    </tbody>
                </Table>
                <Modal show={this.state.show1} onHide={this.handleClose1}>
                        <Modal.Header closeButton>Изменить название</Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.onNameSubmit}>
                                <FormGroup controlId="formBasicName"
                                validationState={this.validateName()}>
                                    <FormControl
                                    type="text"
                                    placeholder="Новое название проекта"
                                    value={this.state.newname}
                                    onChange={this.onNameChange} />
                                    <FormControl.Feedback />
                                </FormGroup>
                                <Button type="submit">Изменить</Button>
                            </Form>
                        </Modal.Body>
                </Modal>
                <Modal show={this.state.show2} onHide={this.handleClose2}>
                        <Modal.Header closeButton>Изменить описание</Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.onDescriptionSubmit}>
                                <FormGroup controlId="formBasicDescription"
                                validationState={this.validateDescription()}>
                                    <FormControl
                                    type="text"
                                    placeholder="Новое описание проекта"
                                    value={this.state.newdescription}
                                    onChange={this.onDescriptionChange} />
                                    <FormControl.Feedback />
                                </FormGroup>
                                <Button type="submit">Изменить</Button>
                            </Form>
                        </Modal.Body>
                </Modal>
                <Modal show={this.state.show3} onHide={this.handleClose3}>
                        <Modal.Header closeButton>Изменить дату начала</Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.onStartDateSubmit}>
                                <FormGroup controlId="formBasicStart"
                                validationState={this.validateStartDate()}>
                                <ControlLabel>Новая дата начала проекта</ControlLabel>
                                    <FormControl
                                    type="date"
                                    value={this.state.newstartDate}
                                    onChange={this.onStartDateChange} />
                                    <FormControl.Feedback />
                                </FormGroup>
                                <Button type="submit">Изменить</Button>
                            </Form>
                        </Modal.Body>
                </Modal>
                <Modal show={this.state.show4} onHide={this.handleClose4}>
                        <Modal.Header closeButton>Изменить дату окончания</Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.onEndDateSubmit}>
                                <FormGroup controlId="formBasicEnd"
                                validationState={this.validateEndDate()}>
                                <ControlLabel>Новая дата окончания проекта</ControlLabel>
                                    <FormControl
                                    type="date"
                                    value={this.state.newendDate}
                                    onChange={this.onEndDateChange} />
                                    <FormControl.Feedback />
                                </FormGroup>
                                <Button type="submit">Изменить</Button>
                            </Form>
                        </Modal.Body>
                </Modal>
        </div>
    }
}

class ProjectList extends Component{
    constructor(props){
        super(props);
        this.state = {project: null, show: false};

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
    onClick(data){
        this.handleShow();
        this.setState({project: data});
    }
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
                <th></th>
                <th></th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.proj.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Id;  
                        var statusname = "";
                        var start = data.ProjectStartDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
                        var end = data.ProjectEndDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
                        if(data.ProjectStatusId===1){statusname="Открыт";}
                        if(data.ProjectStatusId===2){statusname="Закрыт";}
                        return <tr key={id}>
                            <td>{data.ProjectName}</td>
                            <td>{data.ProjectDescription}</td>
                            <td>{start}</td>
                            <td>{end}</td>
                            <td>{statusname}</td>
                            <td><Button onClick={() => this.onClick(data)}>
                            Редактировать</Button>
                            </td>
                            <td><Button onClick={() => this.props.onDeleteProject(id)}>
                            Удалить</Button>
                            </td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
                <Modal show={this.state.show} onHide={this.handleClose}>
                        <Modal.Header closeButton>Редактирование проекта</Modal.Header>
                        <Modal.Body>
                            <ChangeProjectForm proj={this.state.project} onSubmitName={this.props.onAddName}
                            onSubmitDescription={this.props.onAddDescription} 
                            onSubmitStartDate={this.props.onAddStartDate}
                            onSubmitEndDate={this.props.onAddEndDate}/>
                        </Modal.Body>
                </Modal>
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
        if (Date.parse(this.state.endDate)<Date.now()) return 'error';
        if (Date.parse(this.state.endDate)>Date.parse(this.state.startDate)) return 'success';
        if (Date.parse(this.state.endDate)<Date.parse(this.state.startDate)) return 'error';
        if (Date.parse(this.state.endDate)>Date.now()) return 'success';
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
                        <ControlLabel>Дата начала</ControlLabel>
                        <FormControl
                            type="date"
                            placeholder="Дата начала"
                            value={this.state.startDate}
                            onChange={this.onStartDateChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup controlId="formBasicEnd"
                     validationState={this.validateEndDate()}>
                        <ControlLabel>Дата окончания</ControlLabel>
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
                if (error.response) {
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