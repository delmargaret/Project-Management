import React, { Component } from 'react';
import {Table, Button, Modal, FormGroup, FormControl, Form, ControlLabel} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import * as projectWorkService from '../../src/services/projectWorkService';
import * as projectRoleService from '../../src/services/projectRoleService';
import * as employeeService from '../../src/services/employeeService';
import * as scheduleService from '../../src/services/scheduleService';
import * as historyService from '../../src/services/participationHistoryService';
import "../styles/ProjectManager.css";
import Loading from './Loading';

class ChangeProjectWorkForm extends Component{
    constructor(props){
        super(props);
        this.state = {projWorkId:0, projectName: "", employeeName: "", role: "", workLoad: "",
        roles: [], percentOrScheduleId: 0, projectRoleId: 0, changeRoleShow: false, changeWorkloadShow: false,
        percent: 0, daysOnProject: [], freeDays: [], dayId: 0, history: null, changeStartShow: false, 
        changeEndShow: false, startDate: "", endDate: "", projectstart: "", projectend: "", start: "", end: ""};

        this.loadRoles = this.loadRoles.bind(this);
        this.changeRoleModalClose = this.changeRoleModalClose.bind(this);
        this.changeRoleModalShow = this.changeRoleModalShow.bind(this);
        this.onChangeProjectRole = this.onChangeProjectRole.bind(this);
        this.changeWorkloadModalClose = this.changeWorkloadModalClose.bind(this);
        this.changeWorkloadModalShow = this.changeWorkloadModalShow.bind(this);
        this.onChangeWorkLoad = this.onChangeWorkLoad.bind(this);
        this.changeStartModalClose = this.changeStartModalClose.bind(this);
        this.changeStartModalShow = this.changeStartModalShow.bind(this);
        this.onChangeStartDate = this.onChangeStartDate.bind(this);
        this.changeEndModalClose = this.changeEndModalClose.bind(this);
        this.changeEndModalShow = this.changeEndModalShow.bind(this);
        this.onChangeEndDate = this.onChangeEndDate.bind(this);
        this.onRoleSubmit = this.onRoleSubmit.bind(this);
        this.onProjectRoleIdChange = this.onProjectRoleIdChange.bind(this);
        this.renderChangeForm = this.renderChangeForm.bind(this);
        this.renderWorkLoad = this.renderWorkLoad.bind(this);
        this.onNewPercentSubmit = this.onNewPercentSubmit.bind(this);
        this.onPercentChange = this.onPercentChange.bind(this);
        this.loadDaysOnProject = this.loadDaysOnProject.bind(this);
        this.onDayDelete = this.onDayDelete.bind(this);
        this.validateDay = this.validateDay.bind(this);
        this.loadFreeDays = this.loadFreeDays.bind(this);
        this.renderFreeDays = this.renderFreeDays.bind(this);
        this.onDayChange = this.onDayChange.bind(this);
        this.onScheduleSubmit = this.onScheduleSubmit.bind(this);
        this.onStartDateChange = this.onStartDateChange.bind(this);
        this.onEndDateChange = this.onEndDateChange.bind(this);
        this.onStartSubmit = this.onStartSubmit.bind(this);
        this.onEndSubmit = this.onEndSubmit.bind(this);
    }
    changeRoleModalClose() {
        this.setState({ changeRoleShow: false });
      }
    changeRoleModalShow() {
        this.setState({ changeRoleShow: true });
      }
    onChangeProjectRole(){
        this.changeRoleModalShow();
    }
    changeWorkloadModalClose() {
        this.setState({ changeWorkloadShow: false });
      }
    changeWorkloadModalShow() {
        this.setState({ changeWorkloadShow: true });
      }
    onChangeWorkLoad(){
        this.changeWorkloadModalShow();
    }
    changeStartModalClose() {
        this.setState({ changeStartShow: false });
      }
    changeStartModalShow() {
        this.setState({ changeStartShow: true });
      }
    onChangeStartDate(){
        this.changeStartModalShow();
    }
    changeEndModalClose() {
        this.setState({ changeEndShow: false });
      }
    changeEndModalShow() {
        this.setState({ changeEndShow: true });
      }
    onChangeEndDate(){
        this.changeEndModalShow();
    }
    validateStartDate(){
        if (Date.parse(this.state.startDate)<Date.parse(this.state.projectstart)) return 'error';
        if (Date.parse(this.state.startDate)>Date.parse(this.state.projectend)) return 'error';
        if (Date.parse(this.state.startDate)>=Date.parse(this.state.projectstart)) return 'success';
        if (Date.parse(this.state.startDate)<=Date.parse(this.state.projectend)) return 'success';
    }
    validateEndDate(){
        if (Date.parse(this.state.endDate)<Date.now()) return 'error';
        if(this.state.endDate===this.state.projectend) return 'success';
        if (Date.parse(this.state.endDate)<Date.parse(this.state.startDate)) return 'error';
        if (Date.parse(this.state.endDate)>Date.parse(this.state.projectend)) return 'error';
        if (Date.parse(this.state.endDate)<=Date.parse(this.state.projectend)) return 'success';
        if (Date.parse(this.state.endDate)>Date.parse(this.state.startDate)) return 'success';
        if (Date.parse(this.state.endDate)>=Date.now()) return 'success';
    }
    validateRole() {
        if (this.state.projectRoleId !== 0) return 'success';
        if (this.state.projectRoleId === 0) return 'error';
        return null;
    }
    validateDay(){
        if (this.state.dayId !== 0) return 'success';
        if (this.state.dayId === 0) return 'error';
        return null;
    }
    onStartDateChange(e) {
        var val = e.target.value;
        this.setState({startDate: val});
    }
    onEndDateChange(e) {
        var val = e.target.value;
        this.setState({endDate: val});
    }
    onProjectRoleIdChange(e) {
        this.setState({projectRoleId: e.target.value});
    }
    loadRoles(){
        projectRoleService.getRoles().then(res => { this.setState({roles: res.data}) }).catch(error => {
            if (error.response) {
                return null;
            }
        });
    }
    loadDaysOnProject(){
        scheduleService.getScheduleOnProjectWork(this.state.projWorkId).then(res => 
            { this.setState({daysOnProject: res.data}) }).catch(error => {
            if (error.response) {
                return null;
            }
        });
    }
    loadFreeDays(){
        projectWorkService.getProjectWorkById(this.state.projWorkId).then(res =>{
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
    onDayChange(e){
        this.setState({dayId: e.target.value});
    }
    onStartSubmit(e) {
        e.preventDefault();
        var startDate = this.state.startDate;
        var id = JSON.parse(this.state.history).Id;
        if (!startDate || !id) {
            return;
        }
        this.props.onStartDaySubmit({historyId: id, startDate: startDate});
        this.changeStartModalClose();
            this.setState({startDate: ""});
            this.setState({start: startDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1')});
    }
    onEndSubmit(e) {
        e.preventDefault();
        var endDate = this.state.endDate;
        var id = JSON.parse(this.state.history).Id;
        if (!endDate || !id) {
            return;
        }
        this.props.onEndDaySubmit({historyId: id, endDate: endDate});
        this.changeEndModalClose();
            this.setState({endDate: ""});
            this.setState({end: endDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1')});
    }
    onScheduleSubmit(e){
        e.preventDefault();
        var day = this.state.dayId;
        var projectWorkId = this.state.projWorkId;
        var dayName = "";
                        if(day==="1"){dayName="Понедельник";}
                        if(day==="2"){dayName="Вторник";}
                        if(day==="3"){dayName="Среда";}
                        if(day==="4"){dayName="Четверг";}
                        if(day==="5"){dayName="Пятница";}
                        if(day==="6"){dayName="Суббота";}
        if (!day || !projectWorkId) {
            return;
        }

        var data = JSON.stringify({"ProjectWorkId":projectWorkId, 
        "ScheduleDayId":day});
        scheduleService.createSchedule(data).then(res =>{
            if(res!==null){     
                this.loadFreeDays();
                this.loadDaysOnProject();
                this.props.onChangedDay();  
                var load = "";
                    this.state.daysOnProject.forEach((d) =>{
                        var data = JSON.parse(d);
                        var dayId = data.ScheduleDayId;
                        var dayName = "";
                        if(dayId===1){dayName="Понедельник";}
                        if(dayId===2){dayName="Вторник";}
                        if(dayId===3){dayName="Среда";}
                        if(dayId===4){dayName="Четверг";}
                        if(dayId===5){dayName="Пятница";}
                        if(dayId===6){dayName="Суббота";}
                        load = load + dayName + " ";
                    });
                    if(load===""||load===" "){
                        load = "---";
                    }
                    else load = load + dayName;
                    this.setState({workLoad: load}); 
            }
        });
        this.setState({day: 0});
    }
    renderFreeDays(){
        if(this.state.freeDays.length===0){
            return(<div>Свободных дней нет</div> );
            } 
            else return <Form onSubmit={this.onScheduleSubmit}>
            <FormGroup validationState={this.validateDay()}>
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
            <Button type="submit" className="change-btn">Добавить</Button>
            </Form>
    }
    validatePercent() {
        if (parseInt(this.state.percent) < 100) return 'success';
        if (parseInt(this.state.percent) > 100) return 'error';
        if (!parseInt(this.state.percent)) return 'error';
        return null;
    }
    onPercentChange(e) {
        this.setState({percent: e.target.value});
    }
    onNewPercentSubmit(e) {
        e.preventDefault();
        var percent = this.state.percent;
        var load = percent+"%";
        this.setState({workLoad: load});

        if (!percent) {
            return;
        }
        this.props.onSubmitNewPercent(percent);
        this.changeWorkloadModalClose();
            this.setState({percent: 0});
    }
    onDayDelete(id){
        if(id){
            scheduleService.deleteById(id).then(res =>{
                if(res!==null){
                    this.loadDaysOnProject();
                    this.loadFreeDays();
                    var load = "";
                    this.state.daysOnProject.forEach((day) =>{
                        var data = JSON.parse(day);
                        if(data.Id===id) return;
                        var dayId = data.ScheduleDayId;
                        var dayName = "";
                        if(dayId===1){dayName="Понедельник";}
                        if(dayId===2){dayName="Вторник";}
                        if(dayId===3){dayName="Среда";}
                        if(dayId===4){dayName="Четверг";}
                        if(dayId===5){dayName="Пятница";}
                        if(dayId===6){dayName="Суббота";}
                        load = load + dayName + " ";
                    });
                    if(load===""||load===" "){
                        load = "---";
                    }
                    console.log(load);
                    this.setState({workLoad: load});
                }
            });
        }
        this.props.onChangedDay();
    }
    renderWorkLoad(){
        if(!this.state.percentOrScheduleId) return <Loading />
        if(this.state.percentOrScheduleId===1){
            return <Form onSubmit={this.onNewPercentSubmit}>
            <FormGroup validationState={this.validatePercent()}>
                <FormControl 
                    type="number"
                    placeholder="Процент загруженности"
                    value={this.state.percent}
                    onChange={this.onPercentChange} />
                <FormControl.Feedback />
            </FormGroup>
            <Button type="submit" className="change-btn">Добавить</Button>
        </Form>
        }
        if(this.state.percentOrScheduleId===2){
            if(this.state.daysOnProject.length===0) return <div>Расписание не добавлено</div>
            return <div>
                {this.renderFreeDays()}
                <Table id="schedule-table">
            <thead>
                <tr>
                <th>Дни</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.state.daysOnProject.map((day) => { 
                        var data = JSON.parse(day);
                        var id = data.Id;
                        var dayId = data.ScheduleDayId;
                        var dayName = "";
                        if(dayId===1){dayName="Понедельник";}
                        if(dayId===2){dayName="Вторник";}
                        if(dayId===3){dayName="Среда";}
                        if(dayId===4){dayName="Четверг";}
                        if(dayId===5){dayName="Пятница";}
                        if(dayId===6){dayName="Суббота";}
                        return <tr key={id}>
                            <td>{dayName}</td>
                            <td><Button onClick={() => this.onDayDelete(id)} className="change-btn">
                            Удалить</Button>
                            </td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
            </div>
        }
    }
    componentDidMount(){
        this.setState({projWorkId: this.props.projectWorkId});
        this.setState({workLoad: this.props.load});
        this.loadRoles();
        historyService.getEmployeesHistory(this.props.projectWorkId).then(res =>{
            if(res.data!==""){
                this.setState({history: res.data,
                    start: JSON.parse(res.data).StartDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3'),
                    end: JSON.parse(res.data).EndDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3')});
            }
        })
        projectWorkService.getProjectWorkById(this.props.projectWorkId).then(res =>{
            if(res!==null){
                var empId = JSON.parse(res.data).EmployeeId;
                employeeService.getEmployeeById(empId).then(result => {
                    if(result!==null){
                        var empName = JSON.parse(result.data).EmployeeName;
                        var empSurname = JSON.parse(result.data).EmployeeSurname;
                        var name = empSurname + " " + empName;
                        this.setState({employeeName: name});
                        var loadType = JSON.parse(result.data).PercentOrScheduleId;
                        this.setState({percentOrScheduleId: loadType});
                        if(loadType===2){
                            this.loadDaysOnProject();
                            this.loadFreeDays();
                        }
                    }
                });
                var projId = JSON.parse(res.data).ProjectId;
                projectService.getProjectById(projId).then(item => {
                    if(item!==null){
                        var project = JSON.parse(item.data);
                        var projName = project.ProjectName;
                        this.setState({projectName: projName,
                        projectstart: project.ProjectStartDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3'),
                        projectend: project.ProjectEndDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$1-$2-$3')});
                    }
                });
                var roleId = JSON.parse(res.data).ProjectRoleId;
                projectRoleService.getRoleById(roleId).then(item => {
                    if(item!==null){
                        var roleName = JSON.parse(item.data).ProjectRoleName;
                        this.setState({role: roleName});
                    }
                });
            }
        });
    }
    onRoleSubmit(e){
        e.preventDefault();
        var roleId = this.state.projectRoleId;
        var roleName = "";
        if(roleId==="1"){roleName="Front-end разработчик";}
        if(roleId==="2"){roleName="Back-end разработчик";}
        if(roleId==="3"){roleName="Full-stack разработчик";}
        if(roleId==="4"){roleName="Дизайнер";}
        if(roleId==="5"){roleName="Тестировщик";}
        if(roleId==="6"){roleName="Менеджер проекта";}
        if(roleId==="7"){roleName="Бизнес-аналитик";}
        this.setState({role: roleName});
        if (!roleId) {
            return;
        }
        this.props.onSubmitRole(roleId);
            this.setState({projectRoleId: 0});
            this.changeRoleModalClose();
    }
    renderChangeForm(){
        if(this.state.percentOrScheduleId===0) return <tbody>
            <tr>
                <Loading />
                </tr>
            </tbody>
        if(this.state.workLoad==="---"){
            if(this.state.history==="" || this.state.history===null) return <tbody>
                <tr>
                            <th>Роль:</th>
                            <td>{this.state.role}</td>
                            <td><Button onClick={() => this.onChangeProjectRole()}>
                            Изменить</Button>
                            </td>
                        </tr>
            </tbody> 
            else {
                var start = this.state.start.replace(/^(\d+)-(\d+)-(\d+)$/, '$3.$2.$1');
                var end = this.state.end.replace(/^(\d+)-(\d+)-(\d+)$/, '$3.$2.$1');
                return <tbody>
                <tr>
                            <th>Роль:</th>
                            <td>{this.state.role}</td>
                            <td><Button onClick={() => this.onChangeProjectRole()}>
                            Изменить</Button>
                            </td>
                        </tr>
                        <tr>
                            <th>Дата начала участия:</th>
                            <td>{start}</td>
                            <td><Button onClick={() => this.onChangeStartDate()}>
                            Изменить</Button>
                            </td>
                        </tr>
                        <tr>
                            <th>Дата окончания участия:</th>
                            <td>{end}</td>
                            <td><Button onClick={() => this.onChangeEndDate()}>
                            Изменить</Button>
                            </td>
                        </tr>
            </tbody> 
            }
        }
        else {
            if(this.state.history==="" || this.state.history===null) return <tbody>
                <tr>
                    <th>Роль:</th>
                    <td>{this.state.role}</td>
                    <td><Button onClick={() => this.onChangeProjectRole()}>
                    Изменить</Button>
                    </td>
                </tr>
                <tr>
                    <th>Загруженность:</th>
                    <td>{this.state.workLoad}</td>
                    <td><Button onClick={() => this.onChangeWorkLoad()}>
                    Изменить</Button>
                    </td>
                </tr>          
            </tbody>
            else{
                var startt = this.state.start.replace(/^(\d+)-(\d+)-(\d+)$/, '$3.$2.$1');
                var endd = this.state.end.replace(/^(\d+)-(\d+)-(\d+)$/, '$3.$2.$1');
                return <tbody>
                <tr>
                    <th>Роль:</th>
                    <td>{this.state.role}</td>
                    <td><Button onClick={() => this.onChangeProjectRole()}>
                    Изменить</Button>
                    </td>
                </tr>
                <tr>
                    <th>Дата начала участия:</th>
                    <td>{startt}</td>
                    <td><Button onClick={() => this.onChangeStartDate()}>
                    Изменить</Button>
                    </td>
                </tr>
                <tr>
                    <th>Дата окончания участия:</th>
                    <td>{endd}</td>
                    <td><Button onClick={() => this.onChangeEndDate()}>
                    Изменить</Button>
                    </td>
                </tr>
                <tr>
                    <th>Загруженность:</th>
                    <td>{this.state.workLoad}</td>
                    <td><Button onClick={() => this.onChangeWorkLoad()}>
                    Изменить</Button>
                    </td>
                </tr>          
            </tbody>
            }
        }
    }
    render(){
        if(!this.state.roles) return <Loading />
        return <div id="change-pw">
            <div>
                <h5><b>Проект:</b> {this.state.projectName}</h5>
                <h5><b>Сотрудник:</b> {this.state.employeeName}</h5>
            </div>     
            <Table>
            <thead>
            </thead>
                {this.renderChangeForm()}
                </Table>
                <Modal show={this.state.changeRoleShow} onHide={this.changeRoleModalClose}>
                        <Modal.Header closeButton>Изменить роль</Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.onRoleSubmit} >
                                <FormGroup validationState={this.validateRole()}>
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
                                <Button type="submit" className="change-btn">Изменить</Button>
                            </Form>
                        </Modal.Body>
                </Modal>
                <Modal show={this.state.changeWorkloadShow} onHide={this.changeWorkloadModalClose}>
                        <Modal.Header closeButton>Изменить загруженность</Modal.Header>
                        <Modal.Body>
                            {
                                this.renderWorkLoad()
                            }
                        </Modal.Body>
                </Modal>
                <Modal show={this.state.changeStartShow} onHide={this.changeStartModalClose}>
                        <Modal.Header closeButton>Изменить дату начала участия</Modal.Header>
                        <Modal.Body>
                        <Form onSubmit={this.onStartSubmit} className="date-form">
                            <FormGroup controlId="formBasicStart"
                            validationState={this.validateStartDate()}>
                                <ControlLabel>Новая дата начала</ControlLabel>
                                <FormControl
                                    type="date"
                                    placeholder="Дата начала"
                                    value={this.state.startDate}
                                    onChange={this.onStartDateChange} />
                                <FormControl.Feedback />
                            </FormGroup>
                            <Button type="submit" className="change-btn">Изменить</Button>
                        </Form> 
                        </Modal.Body>
                </Modal>
                <Modal show={this.state.changeEndShow} onHide={this.changeEndModalClose}>
                        <Modal.Header closeButton>Изменить дату окончания участия</Modal.Header>
                        <Modal.Body>
                        <Form onSubmit={this.onEndSubmit} className="date-form">
                            <FormGroup controlId="formBasicEnd"
                            validationState={this.validateEndDate()}>
                                <ControlLabel>Новая дата окончания</ControlLabel>
                                <FormControl
                                    type="date"
                                    placeholder="Дата окончания"
                                    value={this.state.endDate}
                                    onChange={this.onEndDateChange} />
                                <FormControl.Feedback />
                            </FormGroup>
                            <Button type="submit" className="change-btn">Изменить</Button>
                        </Form> 
                        </Modal.Body>
                </Modal>
        </div>
    }
}

export default ChangeProjectWorkForm;