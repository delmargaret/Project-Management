import React, { Component } from 'react';
import {Table, Button, Modal} from 'react-bootstrap';
import ChangeProjectForm from './ChangeProjectForm';
import "../styles/AddProject.css";

class ProjectList extends Component{
    constructor(props){
        super(props);
        this.state = {project: null, show: false};

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
    onClick(data){
        this.handleShow();
        this.setState({project: data});
    }
    render(){ 
        return <div id="scroll">
            <Table>
            <thead>
                <tr>
                <th>Название</th>
                <th>Описание</th>
                <th>Дата начала</th>
                <th>Дата окончания</th>
                <th>Статус</th>
                <th></th>
                <th></th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.proj.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Id;  
                        var statusname = "";
                        var start = data.ProjectStartDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
                        var end = data.ProjectEndDate.replace(/^(\d+)-(\d+)-(\d+)\D.+$/, '$3.$2.$1'); 
                        if(data.ProjectStatusId===1){statusname="Открыт";}
                        if(data.ProjectStatusId===2){statusname="Закрыт";}
                        return <tr key={id}>
                            <td>{data.ProjectName}</td>
                            <td>{data.ProjectDescription}</td>
                            <td>{start}</td>
                            <td>{end}</td>
                            <td>{statusname}</td>
                            <td><Button onClick={() => this.onClick(data)}>
                            Редактировать</Button>
                            </td>
                            <td><Button onClick={() => this.props.onDeleteProject(id)}>
                            Удалить</Button>
                            </td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
                <Modal show={this.state.show} onHide={this.handleClose}>
                        <Modal.Header closeButton>Редактирование проекта</Modal.Header>
                        <Modal.Body>
                            <ChangeProjectForm proj={this.state.project} onSubmitName={this.props.onAddName}
                            onSubmitDescription={this.props.onAddDescription} 
                            onSubmitStartDate={this.props.onAddStartDate}
                            onSubmitEndDate={this.props.onAddEndDate}/>
                        </Modal.Body>
                </Modal>
        </div>
            }
}

export default ProjectList;