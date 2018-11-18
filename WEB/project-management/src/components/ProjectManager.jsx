import React, { Component } from 'react';
import {Table, Button, Modal, FormGroup, FormControl, Form} from 'react-bootstrap';
import * as projectService from '../../src/services/projectService';
import * as projectWorkService from '../../src/services/projectWorkService';
import * as projectRoleService from '../../src/services/projectRoleService';
import * as employeeService from '../../src/services/employeeService';
import * as scheduleService from '../../src/services/scheduleService';
import ScheduleDayList from './AddSchedule';

import "./ProjectManager.css";

class AddWorkLoadForm extends Component{
 
    _isMounted = false;
    get isMounted() {
        return this._isMounted;
    }
    set isMounted(value) {
        this._isMounted = value;
    }
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
                        if(this.isMounted){
                            this.setState({freeDays: result.data}); }}).catch(error => {
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
                if(this.isMounted){
                    this.setState({daysOnProject: res.data});}}}).catch(error => {
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
        this.props.onScheduleDaySubmit({projectWorkId: projectWorkId, scheduleDayId: day});
        this.setState({day: 0});
    }
    componentDidUpdate(){
        this.loadFreeDays();
    }
    componentWillUnmount(){
        this.isMounted = false;
    }
    renderFreeDays(){
        if(this.state.freeDays.length===0){
            return(<div>Свободных дней нет</div> );
            } 
            else return <Form onSubmit={this.onScheduleSubmit}>
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
        this.isMounted = true;
        projectWorkService.getWorkLoadType(this.props.workId).then(res =>{
            if(res!==null){
                var load = JSON.parse(res.data);
                this.setState({workLoadType: load});
                if(load!==1) {this.loadFreeDays();}
            }
        });
        
    }
    renderPercent(){
        return <div>
            <h3>Добавить процент</h3>
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
        if(!this.state.workLoadType) return <div>Загрузка...</div>
        if(this.state.workLoadType===3){
            return(
                <div>
                {this.renderPercent()}
                    <div>
                    <h4>Добавить расписание</h4>
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
        if(this.state.workLoadType===2) return <div>
        <h4>Добавить расписание</h4>
           <div>
               {this.renderFreeDays()}
               {this.renderDaysOnProject()}
           </div>
        </div>
    }
}



















class ChangeProjectWorkForm extends Component{
    constructor(props){
        super(props);
        this.state = {projWorkId:0, projectName: "", employeeName: "", role: "", workLoad: "",
        roles: [], percentOrScheduleId: 0, projectRoleId: 0, show1: false, show2: false,
        percent: 0, daysOnProject: []};

        this.loadRoles = this.loadRoles.bind(this);
        this.handleClose1 = this.handleClose1.bind(this);
        this.handleShow1 = this.handleShow1.bind(this);
        this.onChangeProjectRole = this.onChangeProjectRole.bind(this);
        this.handleClose2 = this.handleClose2.bind(this);
        this.handleShow2 = this.handleShow2.bind(this);
        this.onChangeWorkLoad = this.onChangeWorkLoad.bind(this);
        this.onRoleSubmit = this.onRoleSubmit.bind(this);
        this.onProjectRoleIdChange = this.onProjectRoleIdChange.bind(this);
        this.renderChangeForm = this.renderChangeForm.bind(this);
        this.renderWorkLoad = this.renderWorkLoad.bind(this);
        this.onNewPercentSubmit = this.onNewPercentSubmit.bind(this);
        this.onPercentChange = this.onPercentChange.bind(this);
        this.loadDaysOnProject = this.loadDaysOnProject.bind(this);
    }
    handleClose1() {
        this.setState({ show1: false });
      }
    handleShow1() {
        this.setState({ show1: true });
      }
    onChangeProjectRole(){
        this.handleShow1();
    }
    handleClose2() {
        this.setState({ show2: false });
      }
    handleShow2() {
        this.setState({ show2: true });
      }
    onChangeWorkLoad(){
        this.handleShow2();
    }
    validateRole() {
        if (this.state.projectRoleId !== 0) return 'success';
        if (this.state.projectRoleId === 0) return 'error';
        return null;
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
        this.handleClose2();
            this.setState({percent: 0});
    }
    renderWorkLoad(){
        if(!this.state.percentOrScheduleId) return <div>Загрузка</div>
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
            <Button type="submit">Добавить</Button>
        </Form>
        }
        if(this.state.percentOrScheduleId===2){
            return <div>
                
            </div>
        }
    }
    componentDidMount(){
        this.setState({projWorkId: this.props.projectWorkId});
        this.setState({workLoad: this.props.load});
        this.loadRoles();
        this.
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
                        }
                    }
                });
                var projId = JSON.parse(res.data).ProjectId;
                projectService.getProjectById(projId).then(item => {
                    if(item!==null){
                        var projName = JSON.parse(item.data).ProjectName;
                        this.setState({projectName: projName});
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
            this.handleClose1();
    }
    renderChangeForm(){
        if(this.state.percentOrScheduleId===0) return <tbody>
            <tr>
                <td>Загрузка...</td>
                </tr>
            </tbody>
        if(this.state.workLoad==="---"){
            return <tbody>
                <tr>
                            <th>Роль:</th>
                            <td>{this.state.role}</td>
                            <td><Button onClick={() => this.onChangeProjectRole()}>
                            Изменить</Button>
                            </td>
                        </tr>
            </tbody> 
        }
        else return <tbody>
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
    }
    render(){
        if(!this.state.roles) return <div>Загрузка...</div>
        return <div>
            <div>
                <h5><b>Проект:</b> {this.state.projectName}</h5>
                <h5><b>Сотрудник:</b> {this.state.employeeName}</h5>
            </div>     
            <Table>
            <thead>
            </thead>
                {this.renderChangeForm()}
                </Table>
                <Modal show={this.state.show1} onHide={this.handleClose1}>
                        <Modal.Header closeButton>Изменить роль</Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.onRoleSubmit}>
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
                                <Button type="submit">Изменить</Button>
                            </Form>
                        </Modal.Body>
                </Modal>
                <Modal show={this.state.show2} onHide={this.handleClose2}>
                        <Modal.Header closeButton>Изменить загруженность</Modal.Header>
                        <Modal.Body>
                            {
                                this.renderWorkLoad()
                            }
                        </Modal.Body>
                </Modal>
        </div>
    }
}

class NamesAndLoadList extends Component{
    constructor(props){
        super(props);
        this.state = {actualWork:0, actualWorkLoad: "", workLoadType: 0, show: false, show1: false};
        this.onAddWorkLoad = this.onAddWorkLoad.bind(this);
        this.renderSchedule = this.renderSchedule.bind(this);  
        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);   
        this.onWorkLoadClick = this.onWorkLoadClick.bind(this);
        this.onClick = this.onClick.bind(this);
        this.handleShow1 = this.handleShow1.bind(this);
        this.handleClose1 = this.handleClose1.bind(this);
        this.onChangeRole = this.onChangeRole.bind(this);
        this.onChangePercent = this.onChangePercent.bind(this);
    }
  
    handleClose() {
        this.setState({ show: false });
        this.props.changed();
      }
    handleShow() {
        this.setState({ show: true });
      }
    onWorkLoadClick(workId){
        if(workId){
            this.setState({actualWork: workId});
            this.handleShow();
        }
    }
    handleClose1() {
        this.setState({ show1: false });
      }
    handleShow1() {
        this.setState({ show1: true });
      }
    onClick(id, workload){
        this.setState({actualWork: id});
        var load = "";
        if(workload==="0%" || workload==="" || workload==="%"){
            load = "---";
        }
        else load = workload;
        this.setState({actualWorkLoad: load});
        this.handleShow1();
    }
    onAddWorkLoad(workLoad){
        if (workLoad) {
        projectWorkService.addWorkLoad(this.state.actualWork, workLoad).then(res =>{
            if(res!==null){
                this.props.changed();
                this.handleClose();
                }
            });
        }
    }
    onAddSchedule(schedule){
        if (schedule) {
            var data = JSON.stringify({"ProjectWorkId":schedule.projectWorkId, 
            "ScheduleDayId":schedule.scheduleDayId});
        scheduleService.createSchedule(data).then(res =>{
            if(res!==null){
                this.props.changed();
            }
        });
        }
    }
    onChangeRole(roleId){
        if(roleId){
            projectWorkService.changeEmployeesProjectRole(this.state.actualWork, roleId).then(res => {
                if(res!==null){
                    this.props.changed();
                }
            })
        }
    }
    onChangePercent(percent){
        if(percent){
            projectWorkService.changeWorkLoad(this.state.actualWork, percent).then(res => {
                if(res!==null){
                    this.props.changed();
                }
            })
        }
    }
    renderSchedule(data){
        if(data.Item4==="0%" || data.Item4==="" || data.Item4==="%")
        {
            return <th><Button onClick={() => this.onWorkLoadClick(data.Item1)}>Добавить загруженность</Button></th>
        }
        else {return <th>{data.Item4}</th>}
    }
    render(){ 
        return <div>
                <div  id="namesandloadscroll">
                <Table>
            <thead>
                <tr>
                <th>Имя</th>
                <th>Роль</th>
                <th>Загруженность</th>
                <th></th>
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
                            {this.renderSchedule(data)}
                            <td><Button onClick={() => this.onClick(id, data.Item4)}>
                            Редактировать</Button>
                            </td>
                            <td><Button onClick={() => this.props.onDeleteEmployeeFromProject(id)}>
                            Удалить</Button>
                            </td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
                </div>
                <Modal show={this.state.show} onHide={this.handleClose}>
                    <Modal.Header closeButton>Загруженность</Modal.Header>
                    <Modal.Body>
                    <AddWorkLoadForm workId={this.state.actualWork} onWorkLoadSubmit={this.onAddWorkLoad} 
                    onScheduleDaySubmit={this.onAddSchedule}/>
                    </Modal.Body>
                </Modal>
                <Modal show={this.state.show1} onHide={this.handleClose1}>
                    <Modal.Header closeButton>Редактирование</Modal.Header>
                    <Modal.Body>
                        <ChangeProjectWorkForm projectWorkId={this.state.actualWork} 
                        onSubmitRole={this.onChangeRole} load={this.state.actualWorkLoad}
                        onSubmitNewPercent={this.onChangePercent}/>
                    </Modal.Body>
                </Modal>
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
                    var start = data.ProjectStartDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
                    var end = data.ProjectEndDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
                    return <tr key={id}>
                        <td>
                            <Button onClick={() => this.props.openModal(data)}>
                            {data.ProjectName}
                            </Button>
                        </td>
                        <td>{data.ProjectDescription}</td>
                        <td>{start}</td>
                        <td>{end}</td>
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
    this.changeWorkLoad = this.changeWorkLoad.bind(this);
    this.deleteProjectWork = this.deleteProjectWork.bind(this);
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
    changeWorkLoad(){
        this.loadEmployeesOnProject(this.state.Project.Id);
    }
    renderEmployeeList(){
        if(this.state.employees.length===0){
            return <div>Участники отсутствуют</div>     
        }
        else return <NamesAndLoadList works={this.state.employees} projectId={this.state.Project.Id} 
        changed={this.changeWorkLoad} onDeleteEmployeeFromProject={this.deleteProjectWork}/>
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
    onAddEmployee(work) {
        if (work) {
            var data = JSON.stringify({"EmployeeId":work.EmployeeId, "ProjectId":work.ProjectId,
        "ProjectRoleId": work.ProjectRoleId});
        projectWorkService.createProjectWork(data).then(res => {
            if (res !== null) {
                this.loadEmployeesOnProject(this.state.Project.Id);
                this.handleClose2();
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