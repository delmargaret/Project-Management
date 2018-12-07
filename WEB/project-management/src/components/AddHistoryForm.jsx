import React, { Component } from 'react';
import {Button, FormGroup, FormControl, Form, ControlLabel} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import * as projectWorkService from '../../src/services/projectWorkService';
import "../styles/ProjectManager.css";

class AddHistoryForm extends Component{
    constructor(props){
        super(props);
        this.state = {start: "", end: "", startDate: "", endDate: ""};

        this.onSubmit = this.onSubmit.bind(this);
        this.onStartDateChange = this.onStartDateChange.bind(this);
        this.onEndDateChange = this.onEndDateChange.bind(this);
    }
    validateStartDate(){
        if (Date.parse(this.state.startDate)<Date.parse(this.state.start)) return 'error';
        if (Date.parse(this.state.startDate)>Date.parse(this.state.end)) return 'error';
        if (Date.parse(this.state.startDate)>=Date.parse(this.state.start)) return 'success';
        if (Date.parse(this.state.startDate)<=Date.parse(this.state.end)) return 'success';
    }
    validateEndDate(){
        if (Date.parse(this.state.endDate)<Date.now()) return 'error';
        if(this.state.endDate===this.state.end) return 'success';
        if (Date.parse(this.state.endDate)<Date.parse(this.state.startDate)) return 'error';
        if (Date.parse(this.state.endDate)>Date.parse(this.state.end)) return 'error';
        if (Date.parse(this.state.endDate)<=Date.parse(this.state.end)) return 'success';
        if (Date.parse(this.state.endDate)>Date.parse(this.state.startDate)) return 'success';
        if (Date.parse(this.state.endDate)>=Date.now()) return 'success';
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
        var startDate = this.state.startDate;
        var endDate = this.state.endDate;
        var work = this.props.workId;

        if (!startDate || !endDate) {
            return;
        }
        this.props.onHistorySubmit({workId: work, startDate: startDate, endDate: endDate});
            this.setState({startDate: "", endDate: ""});
    }
    componentDidMount(){
        projectWorkService.getProjectWorkById(this.props.workId).then(res =>{
            if(res!==null){
                var work = JSON.parse(res.data);
                projectService.getProjectById(work.ProjectId).then(result=> {
                    if(result.data!==""){
                        var project = JSON.parse(result.data);
                        this.setState({start: project.ProjectStartDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3'),
                        end: project.ProjectEndDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3'),
                        startDate: project.ProjectStartDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3'),
                        endDate: project.ProjectEndDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3')});
                    }
                });
            }
        });      
    }
    render(){
        if(!this.state.start) return <div>Загрузка...</div>
        return <Form  onSubmit={this.onSubmit}>
        <FormGroup controlId="formBasicStart"
         validationState={this.validateStartDate()}>
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
    }
}

export default AddHistoryForm;