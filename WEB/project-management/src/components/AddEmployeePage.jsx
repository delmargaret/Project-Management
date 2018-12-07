import React, { Component } from 'react';
import * as employeeService from '../services/employeeService';
import * as roleService from '../services/roleService';
import EmployeeInformation from './EmployeeInformation';
import EmployeeList from './EmployeeList';
import AddEmployeeForm from './AddEmployeeForm';
import Menu from './Menu';
import {Grid, Row, Col} from 'react-bootstrap';

import {Table, Button, Modal, FormGroup, FormControl, Form} from 'react-bootstrap';
import "../styles/AddEmployee.css";

class AddEmployeePage extends Component{
 
    constructor(props){
        super(props);
        this.state = { roles: [], activeRole: 0, employees: [], modalIsOpen: false, addEmployeeShow: false, sortId: 0,
        roleAsc: [], roleDesc: [], surnameAsc: [], surnameDesc: [], activeEmployee: 0, 
        employeeInfoShow: false, name: " ", errorShow: false};
        this.onAddEmployee = this.onAddEmployee.bind(this);
        this.loadRoles = this.loadRoles.bind(this);
        this.loadEmployees = this.loadEmployees.bind(this);
        this.onClick = this.onClick.bind(this);
        this.addEmployeeModalShow = this.addEmployeeModalShow.bind(this);
        this.addEmployeeModalClose = this.addEmployeeModalClose.bind(this);
        this.employeeInfoModalShow = this.employeeInfoModalShow.bind(this);
        this.employeeInfoModalClose = this.employeeInfoModalClose.bind(this);
        this.errorModalShow = this.errorModalShow.bind(this);
        this.errorModalClose = this.errorModalClose.bind(this);
        this.deleteEmployee = this.deleteEmployee.bind(this);
        this.sortEmployeesByRoleAsc = this.sortEmployeesByRoleAsc.bind(this);
        this.sortEmployeesByRoleDesc = this.sortEmployeesByRoleDesc.bind(this);
        this.sortEmployeesBySurnameAsc = this.sortEmployeesBySurnameAsc.bind(this);
        this.sortEmployeesBySurnameDesc = this.sortEmployeesBySurnameDesc.bind(this);
        this.onSortChange = this.onSortChange.bind(this);
        this.onSort = this.onSort.bind(this);
        this.showInformation = this.showInformation.bind(this);
    }
    addEmployeeModalClose() {
        this.setState({ addEmployeeShow: false });
      }
    addEmployeeModalShow() {
        this.setState({ addEmployeeShow: true });
      }
    employeeInfoModalClose() {
        this.setState({ employeeInfoShow: false });
      }
    employeeInfoModalShow() {
        this.setState({ employeeInfoShow: true });
      }
    errorModalClose() {
        this.setState({ errorShow: false });
      }
    errorModalShow() {
        this.setState({ errorShow: true });
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
            this.employeeInfoModalShow();
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
        if(this.state.employees.length===0) return <div>Сотрудники не найдены</div>
        if(this.state.employees.length===1) return <div>Сотрудники не найдены</div>
            else return <div>
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
    componentDidMount() {
        this.loadRoles();
        this.loadEmployees();
    }
    
    onAddEmployee(employee) {
        if (employee) {
            var data = JSON.stringify({"EmployeeName":employee.name, "EmployeeSurname":employee.surname,
        "EmployeePatronymic": employee.patronymic, "Email": employee.email, "RoleId": employee.roleId});
        employeeService.createEmployee(data).then(res => {
            if (res.data !== "") {
                this.loadEmployees();
                this.addEmployeeModalClose();
                }
                else this.errorModalShow();
            });
        }
    }
    onSortChange(e){
        this.setState({sortId: e.target.value});
    }
    onSort(){
        var sortId = this.state.sortId;
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
        this.addEmployeeModalShow();
    }
    render(){ 
        var addrolename = "";
        const activerole = this.state.activeRole;
        if(activerole===1){addrolename="Добавить ресурсного менеджера";}
        if(activerole===2){addrolename="Добавить проектного менеджера";}
        if(activerole===3){addrolename="Добавить разработчика";}
        const roledata = this.state.roles;
        if (!roledata) return <div>Загрузка...</div>; 
            return <Grid>
            <Row>
              <Col xs={3} md={3}>{<Menu/>}</Col>
              <Col xs={15} md={9}>
                <div>
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
                        <Modal show={this.state.addEmployeeShow} onHide={this.addEmployeeModalClose}>
                            <Modal.Header closeButton>{addrolename}</Modal.Header>
                            <Modal.Body>
                                <AddEmployeeForm roleid = {activerole} onEmployeeSubmit={this.onAddEmployee}/>
                            </Modal.Body>
                        </Modal>
                        <Modal show={this.state.employeeInfoShow} onHide={this.employeeInfoModalClose}>
                            <Modal.Header closeButton>{this.state.name}</Modal.Header>
                            <Modal.Body>
                                <EmployeeInformation employeeId={this.state.activeEmployee}/>
                            </Modal.Body>
                        </Modal>
                        <Modal show={this.state.errorShow} onHide={this.errorModalClose}>
                            <Modal.Header closeButton>Ошибка</Modal.Header>
                                <Modal.Body>
                                    <div>Пользователь с таким e-mail уже создан</div>
                                </Modal.Body>
                        </Modal>  
                    </div>
              </Col>
            </Row>
          </Grid>;        
     }
}

export default AddEmployeePage;