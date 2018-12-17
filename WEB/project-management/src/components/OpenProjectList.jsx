import React, { Component } from 'react';
import {Table, Button} from 'react-bootstrap';
import "../styles/ProjectManager.css";

class OpenProjectList extends Component{
    render(){ 
    return <div id="managerList">
    <div id="scrollpm">
        <Table>
        <thead>
            <tr>
            <th>Проект</th>
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
                            <Button onClick={() => this.props.openModal(data)} id="project-btn">
                            {data.ProjectName}
                            </Button>
                        </td>
                        <td>{data.ProjectDescription}</td>
                        <td>{start}</td>
                        <td>{end}</td>
                        <td><Button onClick={() => this.props.onCl(id)} id="close-project-btn">
                            Завершить проект</Button>
                        </td>
                    </tr>              
                })
                }
                    </tbody>
            </Table>
        </div>
    </div>
        }
}

export default OpenProjectList;