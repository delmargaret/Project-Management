import React, { Component } from 'react';
import {Button, FormGroup, FormControl} from 'react-bootstrap';
import * as projectRoleService from '../../src/services/projectRoleService';
import * as employeeService from '../../src/services/employeeService';
import "../styles/ProjectManager.css";

class AddEmployeeForm extends Component{
    constructor(props){
    super(props);
    this.state = {employeeId: 0, projectRoleId: 0, employees: [], roles: []};

    this.onSubmit = this.onSubmit.bind(this);
    this.onEmployeeIdChange = this.onEmployeeIdChange.bind(this);
    this.onProjectRoleIdChange = this.onProjectRoleIdChange.bind(this);
    this.loadEmployees = this.loadEmployees.bind(this);
    this.loadRoles = this.loadRoles.bind(this);    
    this.renderEmployeeSelect = this.renderEmployeeSelect.bind(this);
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
        employeeService.getEmployeesNotOnProject(this.props.projId).then(res => 
            { this.setState({employees: res.data}) }).catch(error => {
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
    renderEmployeeSelect(){
        if (this.state.employees.length===0) return <div>Сотрудники не найдены</div>
        if (this.state.employees.length===1) return <div>Сотрудники не найдены</div>
        else return <form onSubmit={this.onSubmit}>
                <FormGroup id="formControlsSelect1" validationState={this.validateEmployee()}>
                <FormControl componentClass="select" onChange={this.onEmployeeIdChange}>
                <option>Выберите сотрудника</option>
                    {
                        this.state.employees.map((employee) => {  
                        var data = JSON.parse(employee);
                        var id = data.Id; 
                        if (id===1) return null; 
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
    }
    render() {
        return <div>
            {this.renderEmployeeSelect()}
        </div>
        }
    }

export default AddEmployeeForm; 