import React, { Component } from 'react';
import {Button, FormGroup, FormControl, Form} from 'react-bootstrap';
import * as projectWorkService from '../../src/services/projectWorkService';
import * as scheduleService from '../../src/services/scheduleService';
import ScheduleDayList from './ScheduleDayList';
import "../styles/ProjectManager.css";
import Loading from './Loading';

class AddWorkLoadForm extends Component{
    constructor(props){
        super(props);
        this.state = {percent: 0, workLoadType: 0, freeDays: [], daysOnProject: []};
 
        this.onSubmit = this.onSubmit.bind(this);
        this.onPercentChange = this.onPercentChange.bind(this);  
        this.renderPercent = this.renderPercent.bind(this);
        this.onDayChange = this.onDayChange.bind(this);
        this.onScheduleSubmit = this.onScheduleSubmit.bind(this);
        this.loadFreeDays = this.loadFreeDays.bind(this);
        this.loadDaysOnProject = this.loadDaysOnProject.bind(this);
        this.renderDaysOnProject = this.renderDaysOnProject.bind(this);
    }
    loadFreeDays(){
        projectWorkService.getProjectWorkById(this.props.workId).then(res =>{
            if(res!==null){
                var empId = JSON.parse(res.data).EmployeeId;
                scheduleService.getEmployeesFreeDays(empId).then(result =>
                     { 
                            this.setState({freeDays: result.data}); }).catch(error => {
                        if (error.response) {
                        return null;
                      }
                });
            }
        });
    }
    loadDaysOnProject(){
        scheduleService.getScheduleOnProjectWork(this.props.workId).then(res =>{
            if(res!==null){
                    this.setState({daysOnProject: res.data});}}).catch(error => {
                    if (error.response) {
                    return null;
                }
        });
    }
    validatePercent() {
        if (parseInt(this.state.percent) < 100) return 'success';
        if (parseInt(this.state.percent) > 100) return 'error';
        if (!parseInt(this.state.percent)) return 'error';
        return null;
    }
    validateDay(){
        if (this.state.dayId !== 0) return 'success';
        if (this.state.dayId === 0) return 'error';
        return null;
    }
    onPercentChange(e) {
        this.setState({percent: e.target.value});
    }
    onDayChange(e){
        this.setState({dayId: e.target.value});
    }
    onScheduleSubmit(e){
        e.preventDefault();
        var day = this.state.dayId;
        var projectWorkId = this.props.workId;
        var days = this.state.daysOnProject;
        days.push(day);
        this.setState({daysOnProject: days});
        if (!day || !projectWorkId) {
            return;
        }

        var data = JSON.stringify({"ProjectWorkId":projectWorkId, 
        "ScheduleDayId":day});
        scheduleService.createSchedule(data).then(res =>{
            if(res!==null){     
                this.loadFreeDays();
                this.props.onScheduleDaySubmit();   
            }
        });
        this.setState({day: 0});
    }
    renderFreeDays(){
        if(this.state.freeDays.length===0){
            return(<div>Свободных дней нет</div> );
            } 
            else return <Form onSubmit={this.onScheduleSubmit} >
            <FormGroup id="ScheduleSelect" validationState={this.validateDay()}>
                <FormControl componentClass="select" onChange={this.onDayChange}>
                <option>Выберите день</option>
                    {
                        this.state.freeDays.map((day) => { 
                            var data = JSON.parse(day);
                            var id = data.Id;                              
                            return <option key={id} value={id} >
                            {data.ScheduleDayName}
                            </option>
                            }) 
                    }
                </FormControl>
            </FormGroup>
            <Button type="submit">Добавить</Button>
            </Form>
    }
    renderDaysOnProject(){
        if(this.state.daysOnProject.length===0){
            return <div>Расписание не добавлено</div>
        }
        else return <ScheduleDayList days={this.state.daysOnProject}/>
    }
    onSubmit(e) {
        e.preventDefault();
        var percent = this.state.percent;

        if (!percent) {
            return;
        }
        this.props.onWorkLoadSubmit(percent);
            this.setState({percent: 0});
    }
    componentDidMount(){
        projectWorkService.getWorkLoadType(this.props.workId).then(res =>{
            if(res!==null){
                var load = JSON.parse(res.data);
                this.setState({workLoadType: load});
                if(load!==1) {this.loadFreeDays();}
            }
        });
        
    }
    renderPercent(){
        return <div id="percent">
            <h5>Добавить процент</h5>
            <Form onSubmit={this.onSubmit}>
                <FormGroup controlId="formBasicPercent"
                validationState={this.validatePercent()}>
                    <FormControl 
                        type="number"
                        placeholder="Процент загруженности"
                        value={this.state.percent}
                        onChange={this.onPercentChange} />
                    <FormControl.Feedback />
                </FormGroup>
                <Button type="submit">Добавить</Button>
            </Form>
        </div>   
    }
    render() {
        if(!this.state.workLoadType) return <Loading />
        if(this.state.workLoadType===3){
            if(!this.state.daysOnProject) return <Loading />
            return(
                <div>
                {this.renderPercent()}
                    <div className="freedays"> 
                    <h5>Добавить расписание</h5>
                        <div>
                            {this.renderFreeDays()}
                            {this.renderDaysOnProject()}
                        </div>
                    </div>
                </div>
            )
        }
        if(this.state.workLoadType===1){
            return(
                <div>{this.renderPercent()}</div>
            )
        }
        if(this.state.workLoadType===2) {
            if(!this.state.daysOnProject) return <Loading />
            return <div className="freedays">
            <h5>Добавить расписание</h5>
            <div>
                {this.renderFreeDays()}
                {this.renderDaysOnProject()}
            </div>
            </div>
        }
    }
}

export default AddWorkLoadForm;