import React, { Component } from 'react';
import * as employeeService from '../../src/services/employeeService';
import * as roleService from '../../src/services/roleService';
import {Table, Button, Modal, FormGroup, FormControl} from 'react-bootstrap';
import "./AddEmployee.css";

class EmployeeList extends Component{
    render(){ 
        return <div id="scrolltable">
            <Table>
            <thead>
                <tr>
                <th>Фамилия</th>
                <th>Имя</th>
                <th>Отчество</th>
                <th>E-mail</th>
                <th>Id роли</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.emp.map((employee) => {  
                        var data = JSON.parse(employee); 
                        var id = data.Id;  
                        return <tr key={id}>
                            <td>{data.EmployeeSurname}</td>
                            <td>{data.EmployeeName}</td>
                            <td>{data.EmployeePatronymic}</td>
                            <td>{data.Email}</td>
                            <td>{data.RoleId}</td>
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
        this.state = { roles: [], activeRole: 0, employees: [], modalIsOpen: false, show: false};
        this.onAddEmployee = this.onAddEmployee.bind(this);
        this.loadRoles = this.loadRoles.bind(this);
        this.loadEmployees = this.loadEmployees.bind(this);
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
    renderEmployeeList(){
        if(this.state.employees.length!==0){
            return <EmployeeList emp={this.state.employees}/>
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
                }
            });
        }
    }
    onClick(id){
        this.setState({
            activeRole: id
        })
        this.handleShow();
    }
    render(){ 
        var addrolename = "";
        const activerole = this.state.activeRole;
        if(activerole===1){addrolename="Добавить ресурсного менеджера";}
        if(activerole===2){addrolename="Добавить проектного менеджера";}
        if(activerole===3){addrolename="Добавить сотрудника";}
        const roledata = this.state.roles;
        if (!roledata) return <div>Загрузка...</div>; 
        return <div>
                <h2>Добавить сотрудника</h2>
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
                    {this.renderEmployeeList()}
                </div>
                    <Modal show={this.state.show} onHide={this.handleClose}>
                        <Modal.Header closeButton>{addrolename}</Modal.Header>
                        <Modal.Body>
                            <AddEmployeeForm roleid = {activerole} onEmployeeSubmit={this.onAddEmployee}/>
                        </Modal.Body>
                    </Modal>
        </div>;
     }
}

export default RoleList;
