import React, { Component } from 'react';
import {Button, FormGroup, FormControl, Form, ControlLabel} from 'react-bootstrap';
import "../styles/AddProject.css";;

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
                    <Form  onSubmit={this.onSubmit} id="add-new-project">
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
                        <ControlLabel className="ControlLabel">Дата начала</ControlLabel>
                        <FormControl
                            type="date"
                            placeholder="Дата начала"
                            value={this.state.startDate}
                            onChange={this.onStartDateChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup controlId="formBasicEnd"
                     validationState={this.validateEndDate()}>
                        <ControlLabel className="ControlLabel">Дата окончания</ControlLabel>
                        <FormControl
                            type="date"
                            placeholder="Дата окончания"
                            value={this.state.endDate}
                            onChange={this.onEndDateChange} />
                        <FormControl.Feedback />
                    </FormGroup>
                <Button type="submit" id="project-form-btn">Добавить</Button>
                    </Form>                
            );        
    }
}

export default AddProjectForm;