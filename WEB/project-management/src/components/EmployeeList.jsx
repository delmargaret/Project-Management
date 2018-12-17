import React, { Component } from 'react';
import {Table, Button} from 'react-bootstrap';
import "../styles/AddEmployee.css";

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
                <th>ФИО</th>
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
                            <Button onClick={() => this.props.onShowInformation(id, name)} className="employee-btn">
                            {fullname}</Button>
                            </td>
                            <td>{data.Email}</td>
                            <td>{role}</td>
                            <td><Button onClick={() => this.props.onDeleteEmployee(id)} className="addemployee-btn">
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

export default EmployeeList;