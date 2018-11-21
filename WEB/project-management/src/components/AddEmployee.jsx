import React, { Component } from 'react';
import * as employeeService from '../../src/services/employeeService';
import * as roleService from '../../src/services/roleService';
import * as projectWorkService from '../../src/services/projectWorkService';
import * as method from '../../src/services/methods';

import {Table, Button, Modal, FormGroup, FormControl, Form} from 'react-bootstrap';
import "./AddEmployee.css";

class EmployeeInformation extends Component{
    constructor(props){
        super(props);
        this.state = {employeeId: 0, employee: null, projects: [], workLoad: "---"};
 
        this.renderProjects = this.renderProjects.bind(this);
    }
    componentWillMount(){
        this.setState({employeeId: this.props.employeeId});
    }
    componentDidMount(){
        employeeService.getEmployeeById(this.state.employeeId).then(res =>{
            if(res.data!==""){
                this.setState({employee: res.data});
            }
        });
        projectWorkService.getEmployeesProjects(this.state.employeeId).then(res=>{
            if(res.data!==""){
                this.setState({projects: res.data});
            }
        });
        projectWorkService.getEmployeesWorkLoad(this.state.employeeId).then(res =>{
            if(res.data!==""){
                this.setState({workLoad: res.data});
            }
        });
    }
    renderProjects(){
        if(this.state.projects.length===0) return <div>Проекты не найдены</div>
        else return <div>
            <h4>Проекты</h4>
            <Table>
            <thead>
                <tr>
                <th>Название</th>
                <th>Роль</th>
                <th>Загруженность</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.state.projects.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Item1;  
                        return <tr key={id}>
                            <td>{data.Item3}</td>
                            <td>{data.Item4}</td>
                            <td>{data.Item5}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
    }
    render(){
        if(!this.state.employee) return <div>Загрузка...</div>
        var employee = JSON.parse(this.state.employee);
        var git = "";
        var phone = "";
        var workLoad = this.state.workLoad;
        if(employee.GitLink===null){
            git = "---";
        }
        else git = employee.GitLink;
        if(employee.PhoneNumber===null){
            phone = "---";
        }
        else phone = employee.PhoneNumber;
        return <div>
            <div>
            <h5>E-mail: {employee.Email}</h5>
            <h5>Git: {git}</h5>
            <h5>Телефон: {phone}</h5>
            <h5>Загруженность: {workLoad}</h5>
            </div>
            <hr />
            {this.renderProjects()}
        </div>
            
    }
}

class EmployeeList extends Component{
    constructor(props, context) {
        super(props, context);
    
        this.handleClick = e => {
          this.setState({ target: e.target, show: !this.state.show });
        };
    
        this.state = {
          show: false
        };
      }
    render(){ 
        return <div id="scrolltable">
            <Table>
            <thead>
                <tr>
                <th>Фамилия</th>
                <th>Имя</th>
                <th>Отчество</th>
                <th>E-mail</th>
                <th>Роль</th>
                <th></th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.emp.map((employee) => {  
                        var data = JSON.parse(employee); 
                        var id = data.Id;  
                        if(id===1) return null;
                        var role = "";
                        var roleId = data.RoleId;
                        if(roleId===1){role="Ресурсный менеджер";}
                        if(roleId===2){role="Проектный менеджер";}
                        if(roleId===3){role="Разработчик";}
                        var fullname = data.EmployeeSurname + " " +
                        data.EmployeeName + " " + data.EmployeePatronymic;
                        var name = data.EmployeeSurname + " " +
                        data.EmployeeName;
                        return <tr key={id}>
                            <td>
                            <Button onClick={() => this.props.onShowInformation(id, name)}>
                            {fullname}</Button>
                            </td>
                            <td>{data.Email}</td>
                            <td>{role}</td>
                            <td><Button onClick={() => this.props.onDeleteEmployee(id)}>
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

class AddEmployeeForm extends Component{
 
    constructor(props){
        super(props);
        this.state = {name: "", surname: "", patronymic: "",
                    email: "", roleIdIsValid: true};
 
        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onSurnameChange = this.onSurnameChange.bind(this);
        this.onPatronymicChange = this.onPatronymicChange.bind(this);
        this.onEmailChange = this.onEmailChange.bind(this);
    }
    validateSurname() {
        if (this.state.surname.length > 1) return 'success';
        if (this.state.surname.length > 0) return 'error';
        return null;
      }
      validateName() {
        if (this.state.name.length > 1) return 'success';
        if (this.state.name.length > 0) return 'error';
        return null;
      }
      validatePatronymic() {
        if (this.state.patronymic.length > 1) return 'success';
        if (this.state.patronymic.length > 0) return 'error';
        return null;
      }
    validateRole(roleId){
        return roleId>0 && roleId<4;
    }
    onNameChange(e) {
        this.setState({name: e.target.value});
    }
    onSurnameChange(e) {
        this.setState({surname: e.target.value});
    }
    onPatronymicChange(e) {
        this.setState({patronymic: e.target.value});
    }
    onEmailChange(e) {
        this.setState({email: e.target.value});
    }
    onSubmit(e) {
        e.preventDefault();
        var name = this.state.name.trim();
        var surname = this.state.surname.trim();
        var patronymic = this.state.patronymic.trim();
        var email = this.state.email.trim();
        var roleid = this.props.roleid;

        var roleValid = this.validateRole(roleid);
        this.setState({roleIdIsValid: roleValid});
        if (!name || !surname || !patronymic || !email || roleid<0 || roleid>3) {
            return;
        }
        if(this.state.roleIdIsValid===true){
                this.props.onEmployeeSubmit({ name: name, surname: surname,
                    patronymic: patronymic, email: email, roleId: roleid});
                    this.setState({name: "", surname: "", patronymic: "",
                    email: "", roleId: 0});
            }
    }
    render() {
        return (
          <form onSubmit={this.onSubmit}>
                <FormGroup controlId="formBasicText"
                    validationState={this.validateSurname()}>
                <FormControl
                    type="text"
                    placeholder="Фамилия"
                    value={this.state.surname}
                    onChange={this.onSurnameChange} />
                <FormControl.Feedback />
                </FormGroup>

                <FormGroup controlId="formBasicText"
                    validationState={this.validateName()}>
                <FormControl
                    type="text"
                    placeholder="Имя"
                    value={this.state.name}
                    onChange={this.onNameChange} />
                <FormControl.Feedback />
                </FormGroup>

                <FormGroup controlId="formBasicText"
                    validationState={this.validatePatronymic()}>
                <FormControl
                    type="text"
                    placeholder="Отчество"
                    value={this.state.patronymic}
                    onChange={this.onPatronymicChange} />
                <FormControl.Feedback />
                </FormGroup>

                <FormGroup controlId="formControlsEmail">
                <FormControl
                    type="email"
                    placeholder="E-mail"
                    value={this.state.email}
                    onChange={this.onEmailChange} />
                <FormControl.Feedback />
                </FormGroup>
            <Button type="submit">Добавить</Button>
          </form>
        );
    }
}

class RoleList extends Component{
 
    constructor(props){
        super(props);
        this.state = { roles: [], activeRole: 0, employees: [], modalIsOpen: false, show: false, sortId: 0,
        roleAsc: [], roleDesc: [], surnameAsc: [], surnameDesc: [], activeEmployee: 0, show1: false, name: " "};
        this.onAddEmployee = this.onAddEmployee.bind(this);
        this.loadRoles = this.loadRoles.bind(this);
        this.loadEmployees = this.loadEmployees.bind(this);
        this.onClick = this.onClick.bind(this);
        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleShow1 = this.handleShow1.bind(this);
        this.handleClose1 = this.handleClose1.bind(this);
        this.deleteEmployee = this.deleteEmployee.bind(this);
        this.sortEmployeesByRoleAsc = this.sortEmployeesByRoleAsc.bind(this);
        this.sortEmployeesByRoleDesc = this.sortEmployeesByRoleDesc.bind(this);
        this.sortEmployeesBySurnameAsc = this.sortEmployeesBySurnameAsc.bind(this);
        this.sortEmployeesBySurnameDesc = this.sortEmployeesBySurnameDesc.bind(this);
        this.onSortChange = this.onSortChange.bind(this);
        this.onSort = this.onSort.bind(this);
        this.showInformation = this.showInformation.bind(this);
    }
    handleClose() {
        this.setState({ show: false });
      }
    handleShow() {
        this.setState({ show: true });
      }
    handleClose1() {
        this.setState({ show1: false });
      }
    handleShow1() {
        this.setState({ show1: true });
      }
    deleteEmployee(id) {
        if (id) {
        employeeService.deleteById(id).then(res => {
            if (res !== null) {
                this.loadEmployees();
                }
            });
        }
    }
    showInformation(id, name){
        if(id){
            this.setState({activeEmployee: id});
            this.setState({name: name});
            this.handleShow1();
        }
    }
    loadRoles() {
        roleService.getRoles().then(res => { this.setState({roles: res.data}) });
    }
    loadEmployees() {
        employeeService.getEmployees().then(res => { this.setState({employees: res.data}) }).catch(error => {
            if (error.response) {
                return null;
              }
        });
    }
    sortEmployeesBySurnameAsc(){
        employeeService.sortBySurnameAsc().then(res => { this.setState({employees: res.data}) }).catch(error => {
            if (error.response) {
                return null;
              }
        });
    }
    sortEmployeesBySurnameDesc(){
        employeeService.sortBySurnameDesc().then(res => { this.setState({employees: res.data}) }).catch(error => {
            if (error.response) {
                return null;
              }
        });
    }
    sortEmployeesByRoleAsc(){
        employeeService.sortByRoleAsc().then(res => { this.setState({employees: res.data}) }).catch(error => {
            if (error.response) {
                return null;
              }
        });
    }
    sortEmployeesByRoleDesc(){
        employeeService.sortByRoleDesc().then(res => { this.setState({employees: res.data}) }).catch(error => {
            if (error.response) {
                return null;
              }
        });
    }
    renderEmployeeList(employees){
        if(this.state.employees.length!==0){
            return <div>
                <Form id="SortSelect">
                    <FormGroup>
                        <FormControl componentClass="select" value={this.state.sortId}
                        onChange={this.onSortChange} onClick={() => this.onSort()}>
                        <option value={0}>Сортировать по</option>
                        <option value={1}>Фамилии(по возрастанию)</option>
                        <option value={2}>Фамилии(по убыванию)</option>
                        <option value={3}>Роли(по возрастанию)</option>
                        <option value={4}>Роли(по убыванию)</option>
                        </FormControl>
                    </FormGroup>
                    </Form>
                    <EmployeeList emp={employees} onDeleteEmployee={this.deleteEmployee}
                    onShowInformation={this.showInformation}/>
            </div> 
        }
        else return <div>Сотрудники не найдены</div>
    }
    componentDidMount() {
        this.loadRoles();
        this.loadEmployees();
    }
    
    onAddEmployee(employee) {
        if (employee) {
            var data = JSON.stringify({"EmployeeName":employee.name, "EmployeeSurname":employee.surname,
        "EmployeePatronymic": employee.patronymic, "Email": employee.email, "RoleId": employee.roleId});
        employeeService.createEmployee(data).then(res => {
            if (res !== null) {
                this.loadEmployees();
                this.handleClose();
                }
            });
        }
    }
    onSortChange(e){
        this.setState({sortId: e.target.value});
    }
    onSort(){
        var sortId = this.state.sortId;
        console.log(sortId==="2");
        if(sortId==="1"){
            this.sortEmployeesBySurnameAsc();
        }
        if(sortId==="2"){
            this.sortEmployeesBySurnameDesc();
        }
        if(sortId==="3"){
            this.sortEmployeesByRoleAsc();
        }
        if(sortId==="4"){
            this.sortEmployeesByRoleDesc();
        }
        else this.loadEmployees();
    }
    onClick(id){
        this.setState({
            activeRole: id
        })
        this.handleShow();
    }
    render(){ 
        var token = method.getToken();
        console.log(token);
        var addrolename = "";
        const activerole = this.state.activeRole;
        if(activerole===1){addrolename="Добавить ресурсного менеджера";}
        if(activerole===2){addrolename="Добавить проектного менеджера";}
        if(activerole===3){addrolename="Добавить разработчика";}
        const roledata = this.state.roles;
        if (!roledata) return <div>Загрузка...</div>; 
            return <div>
                <h2>Сотрудники</h2>
                <div>
                    <Table>
                        <tbody>
                        {
                    this.state.roles.map((role) => {  
                        var data = JSON.parse(role); 
                        var id = data.Id;  
                        return <tr key={id}>
                            <td>{data.Id}.</td>
                            <td>{data.RoleName}</td>
                            <td><Button onClick={() => this.onClick(id)}>
                                Добавить</Button>
                            </td>
                        </tr>              
                    })
                    }
                        </tbody>
                    </Table>
                    {this.renderEmployeeList(this.state.employees)}
                </div>
                    <Modal show={this.state.show} onHide={this.handleClose}>
                        <Modal.Header closeButton>{addrolename}</Modal.Header>
                        <Modal.Body>
                            <AddEmployeeForm roleid = {activerole} onEmployeeSubmit={this.onAddEmployee}/>
                        </Modal.Body>
                    </Modal>
                    <Modal show={this.state.show1} onHide={this.handleClose1}>
                        <Modal.Header closeButton>{this.state.name}</Modal.Header>
                        <Modal.Body>
                            <EmployeeInformation employeeId={this.state.activeEmployee}/>
                        </Modal.Body>
                    </Modal>
        </div>;        
     }
}

export default RoleList;
