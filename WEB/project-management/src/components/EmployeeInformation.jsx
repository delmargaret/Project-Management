import React, { Component } from 'react';
import * as employeeService from '../../src/services/employeeService';
import * as projectWorkService from '../../src/services/projectWorkService';
import {Table} from 'react-bootstrap';
import "../styles/AddEmployee.css";

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
                <th>Период работы</th>
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
                            <td>{data.Item6}</td>
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

export default EmployeeInformation;