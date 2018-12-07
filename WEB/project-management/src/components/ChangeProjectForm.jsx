import React, { Component } from 'react';
import {Table, Button, FormGroup, FormControl, Form, Modal, ControlLabel} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import "../styles/AddProject.css";

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
        newendDate: "", changeNameShow: false, changeDescriptionShow: false, 
        changeStartShow: false, changeEndShow: false};

        this.onNameChange = this.onNameChange.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onStartDateChange = this.onStartDateChange.bind(this);
        this.onEndDateChange = this.onEndDateChange.bind(this);
        this.onChangeProjectName = this.onChangeProjectName.bind(this);
        this.changeNameModalShow = this.changeNameModalShow.bind(this);
        this.changeNameModalClose = this.changeNameModalClose.bind(this);
        this.onNameSubmit = this.onNameSubmit.bind(this);
        this.onChangeProjectDescription = this.onChangeProjectDescription.bind(this);
        this.changeDescriptionModalShow = this.changeDescriptionModalShow.bind(this);
        this.changeDescriptionModalClose = this.changeDescriptionModalClose.bind(this);
        this.onDescriptionSubmit = this.onDescriptionSubmit.bind(this);
        this.onChangeProjectStartDate = this.onChangeProjectStartDate.bind(this);
        this.changeStartModalShow = this.changeStartModalShow.bind(this);
        this.changeStartModalClose = this.changeStartModalClose.bind(this);
        this.onStartDateSubmit = this.onStartDateSubmit.bind(this);
        this.onChangeProjectEndDate = this.onChangeProjectEndDate.bind(this);
        this.changeEndModalShow = this.changeEndModalShow.bind(this);
        this.changeEndModalClose = this.changeEndModalClose.bind(this);
        this.onEndDateSubmit = this.onEndDateSubmit.bind(this);
    }
    changeNameModalClose() {
        this.setState({ changeNameShow: false });
      }
    changeNameModalShow() {
        this.setState({ changeNameShow: true });
      }
    onChangeProjectName(){
        this.changeNameModalShow();
    }
    changeDescriptionModalClose() {
        this.setState({ changeDescriptionShow: false });
      }
    changeDescriptionModalShow() {
        this.setState({ changeDescriptionShow: true });
      }
    onChangeProjectDescription(){
        this.changeDescriptionModalShow();
    }
    changeStartModalClose() {
        this.setState({ changeStartShow: false });
      }
    changeStartModalShow() {
        this.setState({ changeStartShow: true });
      }
    onChangeProjectStartDate(){
        this.changeStartModalShow();
    }
    changeEndModalClose() {
        this.setState({ changeEndShow: false });
      }
    changeEndModalShow() {
        this.setState({ changeEndShow: true });
      }
    onChangeProjectEndDate(){
        this.changeEndModalShow();
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
            this.changeNameModalClose();
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
            this.changeDescriptionModalClose();
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
            this.changeStartModalClose();
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
            this.changeEndModalClose();
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
                <Modal show={this.state.changeNameShow} onHide={this.changeNameModalClose}>
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
                <Modal show={this.state.changeDescriptionShow} onHide={this.changeDescriptionModalClose}>
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
                <Modal show={this.state.changeStartShow} onHide={this.changeStartModalClose}>
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
                <Modal show={this.state.changeEndShow} onHide={this.changeEndModalClose}>
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

export default ChangeProjectForm;