import React, { Component } from 'react';
import {Table, Button, Modal} from 'react-bootstrap';
import * as projectWorkService from '../../src/services/projectWorkService';
import * as historyService from '../../src/services/participationHistoryService';
import AddHistoryForm from './AddHistoryForm';
import AddWorkLoadForm from './AddWorkLoadForm';
import ChangeProjectWorkForm from './ChangeProjectWorkForm';
import "../styles/ProjectManager.css";

class NamesAndLoadList extends Component{
    constructor(props){
        super(props);
        this.state = {actualWork:0, actualWorkLoad: "", workLoadType: 0, changeLoadShow: false, 
        addHistoryShow: false, changeProjectWorkShow: false};
        this.onAddWorkLoad = this.onAddWorkLoad.bind(this);
        this.renderSchedule = this.renderSchedule.bind(this);  
        this.changeLoadModalShow = this.changeLoadModalShow.bind(this);
        this.changeLoadModalClose = this.changeLoadModalClose.bind(this);   
        this.onWorkLoadClick = this.onWorkLoadClick.bind(this);
        this.onClick = this.onClick.bind(this);
        this.addHistoryModalShow = this.addHistoryModalShow.bind(this);
        this.addHistoryModalClose = this.addHistoryModalClose.bind(this);
        this.onChangeRole = this.onChangeRole.bind(this);
        this.onChangePercent = this.onChangePercent.bind(this);
        this.renderHisroty = this.renderHisroty.bind(this);
        this.onHistoryClick = this.onHistoryClick.bind(this);
        this.changeProjectWorkModalShow = this.changeProjectWorkModalShow.bind(this);
        this.changeProjectWorkModalClose = this.changeProjectWorkModalClose.bind(this);   
        this.onAddHistory = this.onAddHistory.bind(this);
        this.onChangeStartDay = this.onChangeStartDay.bind(this);
        this.onChangeEndDay = this.onChangeEndDay.bind(this);
    }
    changeLoadModalClose() {
        this.setState({ changeLoadShow: false });
        this.props.changed();
      }
    changeLoadModalShow() {
        this.setState({ changeLoadShow: true });
      }
    onWorkLoadClick(workId){
        if(workId){
            this.setState({actualWork: workId});
            this.changeLoadModalShow();
        }
    }
    addHistoryModalClose() {
        this.setState({ addHistoryShow: false });
        this.props.changed();
      }
    addHistoryModalShow() {
        this.setState({ addHistoryShow: true });
      }
    onHistoryClick(workId){
        if(workId){
            this.setState({actualWork: workId});
            this.addHistoryModalShow();
        }
    }
    changeProjectWorkModalClose() {
        this.setState({ changeProjectWorkShow: false });
      }
    changeProjectWorkModalShow() {
        this.setState({ changeProjectWorkShow: true });
      }
    onClick(id, workload){
        this.setState({actualWork: id});
        var load = "";
        if(workload==="0%" || workload==="" || workload==="%"){
            load = "---";
        }
        else load = workload;
        this.setState({actualWorkLoad: load});
        this.changeProjectWorkModalShow();
    }
    onAddWorkLoad(workLoad){
        if (workLoad) {
        projectWorkService.addWorkLoad(this.state.actualWork, workLoad).then(res =>{
            if(res!==null){
                this.props.changed();
                this.changeLoadModalClose();
                }
            });
        }
    }
    onAddHistory(history){
        if (history) {
            var data = JSON.stringify({"ProjectWorkId":history.workId, "StartDate":history.startDate,
        "EndDate": history.endDate});
        historyService.createHistory(data).then(res =>{
            if(res.data!==""){
                this.addHistoryModalClose();                
                }
            });
        }
    }
    onAddSchedule(){
        this.props.changed();
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
    onChangeStartDay(start){
        if(start){
            var startdate = JSON.stringify(start.startDate);
            historyService.changeHistoryStartDate(start.historyId, startdate).then(res =>{
                if(res.data!==""){
                    this.props.changed();
                }
            })
        }
    }
    onChangeEndDay(end){
        if(end){
            var enddate = JSON.stringify(end.endDate);
            historyService.changeHistoryEndDate(end.historyId, enddate).then(res =>{
                if(res.data!==""){
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
        if(data.Item5==="0%" || data.Item5==="" || data.Item5==="%")
        {
            return <td><Button onClick={() => this.onWorkLoadClick(data.Item1)}>Добавить загруженность</Button></td>
        }
        else {return <td>{data.Item5}</td>}
    }
    renderHisroty(data){
        if(data.Item6===" ")
        {
            return <td><Button onClick={() => this.onHistoryClick(data.Item1)}>Добавить историю</Button></td>
        }
        else {return <td>{data.Item6}</td>}
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
                <th>Даты участия</th>
                <th></th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.works.map((employee) => {  
                        var data = JSON.parse(employee); 
                        var id = data.Item1;  
                        return <tr key={id}>
                            <td>{data.Item3}</td>
                            <td>{data.Item4}</td>
                            {this.renderSchedule(data)}
                            {this.renderHisroty(data)}
                            <td><Button onClick={() => this.onClick(id, data.Item5)}>
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
                <Modal show={this.state.changeLoadShow} onHide={this.changeLoadModalClose}>
                    <Modal.Header closeButton>Загруженность</Modal.Header>
                    <Modal.Body>
                    <AddWorkLoadForm workId={this.state.actualWork} onWorkLoadSubmit={this.onAddWorkLoad} 
                    onScheduleDaySubmit={this.onAddSchedule}/>
                    </Modal.Body>
                </Modal>
                <Modal show={this.state.addHistoryShow} onHide={this.addHistoryModalClose}>
                    <Modal.Header closeButton>История участия</Modal.Header>
                    <Modal.Body>
                    <AddHistoryForm workId={this.state.actualWork} onHistorySubmit={this.onAddHistory} />
                    </Modal.Body>
                </Modal>
                <Modal show={this.state.changeProjectWorkShow} onHide={this.changeProjectWorkModalClose}>
                    <Modal.Header closeButton>Редактирование</Modal.Header>
                    <Modal.Body>
                        <ChangeProjectWorkForm projectWorkId={this.state.actualWork} 
                        onSubmitRole={this.onChangeRole} load={this.state.actualWorkLoad}
                        onSubmitNewPercent={this.onChangePercent} onChangedDay={this.props.changeDay}
                        onStartDaySubmit={this.onChangeStartDay} onEndDaySubmit={this.onChangeEndDay}/>
                    </Modal.Body>
                </Modal>
        </div>
        }
    }

export default NamesAndLoadList;