import React, { Component } from 'react';
import * as method from '../../src/services/methods';
import * as tokenService from '../../src/services/tokenService';
import * as employeeService from '../services/employeeService';
import {Table, Button, Modal, Form, FormControl, FormGroup} from 'react-bootstrap';
import '../styles/Settings.css'

class SettingsPage extends Component{
    constructor(props){
        super(props);
        this.state = {employeeId: 0, employee: null, changeSurnameShow: false, changeNameShow: false,
        changePatronymicShow: false, changeGitShow: false, changePhoneShow: false, surname: "", newsurname: "", name: "", newname: "",
        patronymic: "", newpatronymic: "", git: "", newgit: "", phone: "", newphone: "", password: "", 
        confirmedPassword: "", show: false, changePasswordShow: false};
 
        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.changeSurnameModalShow = this.changeSurnameModalShow.bind(this);
        this.changeSurnameModalClose = this.changeSurnameModalClose.bind(this);
        this.changeNameModalShow = this.changeNameModalShow.bind(this);
        this.changeNameModalClose = this.changeNameModalClose.bind(this);
        this.changePatronymicModalShow = this.changePatronymicModalShow.bind(this);
        this.changePatronymicModalClose = this.changePatronymicModalClose.bind(this);
        this.changeGitModalShow = this.changeGitModalShow.bind(this);
        this.changeGitModalClose = this.changeGitModalClose.bind(this);
        this.changePhoneModalShow = this.changePhoneModalShow.bind(this);
        this.changePhoneModalClose = this.changePhoneModalClose.bind(this);
        this.changePasswordModalShow = this.changePasswordModalShow.bind(this);
        this.changePasswordModalClose = this.changePasswordModalClose.bind(this);
        this.onChangeGit = this.onChangeGit.bind(this);
        this.onChangePhoneNumber = this.onChangePhoneNumber.bind(this);
        this.onChangeName = this.onChangeName.bind(this);
        this.onChangeSurname = this.onChangeSurname.bind(this);
        this.onChangePatronymic = this.onChangePatronymic.bind(this);
        this.onDeleteGit = this.onDeleteGit.bind(this);
        this.onDeletePhoneNumber = this.onDeletePhoneNumber.bind(this);
        this.onSurnameChange = this.onSurnameChange.bind(this);
        this.onNewSurnameSubmit = this.onNewSurnameSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onNewNameSubmit = this.onNewNameSubmit.bind(this);
        this.onPatronymicChange = this.onPatronymicChange.bind(this);
        this.onNewPatronymicSubmit = this.onNewPatronymicSubmit.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);
        this.onConfirmedPassword = this.onConfirmedPassword.bind(this);
        this.onChangePassword = this.onChangePassword.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.onGitChange = this.onGitChange.bind(this);
        this.onNewGitSubmit = this.onNewGitSubmit.bind(this);
        this.onPhoneNumberChange = this.onPhoneNumberChange.bind(this);
        this.onNewPhoneNumberSubmit = this.onNewPhoneNumberSubmit.bind(this);
    }
    handleClose() {
        this.setState({ show: false });
    }
    handleShow() {
        this.setState({ show: true });
    }
    changeSurnameModalClose() {
        this.setState({ changeSurnameShow: false });
      }
    changeSurnameModalShow() {
        this.setState({ changeSurnameShow: true });
      }
    changeNameModalClose() {
        this.setState({ changeNameShow: false });
      }
    changeNameModalShow() {
        this.setState({ changeNameShow: true });
      }
    changePatronymicModalClose() {
        this.setState({ changePatronymicShow: false });
      }
    changePatronymicModalShow() {
        this.setState({ changePatronymicShow: true });
      }
    changeGitModalClose() {
        this.setState({ changeGitShow: false });
      }
    changeGitModalShow() {
        this.setState({ changeGitShow: true });
      }
    changePhoneModalClose() {
        this.setState({ changePhoneShow: false });
      }
    changePhoneModalShow() {
        this.setState({ changePhoneShow: true });
      }
    changePasswordModalClose() {
        this.setState({ changePasswordShow: false });
      }
    changePasswordModalShow() {
        this.setState({ changePasswordShow: true });
      }
    onChangeSurname(){
        this.changeSurnameModalShow();
    }
    onChangeName(){
        this.changeNameModalShow();
    }
    onChangePatronymic(){
        this.changePatronymicModalShow();
    }
    onChangeGit(){
        this.changeGitModalShow();
    }
    onChangePhoneNumber(){
        this.changePhoneModalShow();
    }
    onChangePassword(){
        this.changePasswordModalShow();
    }
    validateSurname() {
        if (this.state.newsurname.length > 1) return 'success';
        if (this.state.newsurname.length > 0) return 'error';
        return null;
    }
    onSurnameChange(e) {
        this.setState({newsurname: e.target.value});
    }
    onNewSurnameSubmit(e) {
        e.preventDefault();
        var surname = this.state.newsurname.trim();

        if (!surname) {
            return;
        }
        var sr = JSON.stringify(surname);
        employeeService.changeSurname(this.state.employeeId, sr);
        this.setState({surname: surname});
        this.setState({newsurname: ""});
        this.changeSurnameModalClose();
    }
    validateName() {
        if (this.state.newname.length > 1) return 'success';
        if (this.state.newname.length > 0) return 'error';
        return null;
    }
    onNameChange(e) {
        this.setState({newname: e.target.value});
    }
    onNewNameSubmit(e) {
        e.preventDefault();
        var name = this.state.newname;

        if (!name) {
            return;
        }

        var nm = JSON.stringify(name);
        employeeService.changeName(this.state.employeeId, nm);
        this.setState({name: name});
        this.setState({newname: ""});
        this.changeNameModalClose();
    }
    validatePatronymic() {
        if (this.state.newpatronymic.length > 1) return 'success';
        if (this.state.newpatronymic.length > 0) return 'error';
        return null;
    }
    onPatronymicChange(e) {
        this.setState({newpatronymic: e.target.value});
    }
    onNewPatronymicSubmit(e) {
        e.preventDefault();
        var patronymic = this.state.newpatronymic;

        if (!patronymic) {
            return;
        }

        var pt = JSON.stringify(patronymic);
        employeeService.changePatronymic(this.state.employeeId, pt);
        this.setState({patronymic: patronymic});
        this.setState({newpatronymic: ""});
        this.changePatronymicModalClose();
    }
    validateGit() {
        if (this.state.newgit.length > 1) return 'success';
        if (this.state.newgit.length > 0) return 'error';
        if (this.state.newgit.length > 30) return 'error';
        return null;
    }
    onGitChange(e) {
        this.setState({newgit: e.target.value});
    }
    onNewGitSubmit(e) {
        e.preventDefault();
        var git = this.state.newgit;

        if (!git) {
            return;
        }

        var gt = JSON.stringify(git);
        employeeService.changeGitLink(this.state.employeeId, gt);
        this.setState({git: git});
        this.setState({newgit: ""});
        this.changeGitModalClose();
    }
    validatePhone() {
        if (/^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})+$/.test(this.state.newphone)) return 'success';
        if (!(/^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})+$/.test(this.state.newphone))) return 'error';
        return null;
    }
    onPhoneNumberChange(e) {
        this.setState({newphone: e.target.value});
    }
    onNewPhoneNumberSubmit(e) {
        e.preventDefault();
        var phone = this.state.newphone;

        if (!phone) {
            return;
        }

        var pn = JSON.stringify(phone);
        employeeService.changePhoneNumber(this.state.employeeId, pn);
        this.setState({phone: phone});
        this.setState({newphone: ""});
        this.changePhoneModalClose();
    }
    validatePassword(){
        if (this.state.password.length===0) return null;
        if (this.state.password!==this.state.confirmedPassword) return 'error';
        if (this.state.password===this.state.confirmedPassword) return 'success';
    }
    onPasswordChange(e) {
        var val = e.target.value;
        this.setState({password: val});
    }
    onConfirmedPassword(e) {
        var val = e.target.value;
        this.setState({confirmedPassword: val});
    }
    onSubmit(e){
        e.preventDefault();

        var password = this.state.password;
        tokenService.changePassword(this.state.employee.Email, password).then(res =>{
            if(res!==""){
                this.changePasswordModalClose();
            }
        }).catch((ex)=>{
            this.handleShow();
        });
        this.setState({password: "", confirmedPassword: ""});
    }
    onDeleteGit(){
        employeeService.deleteGitLink(this.state.employeeId).then(res =>{
            if(res!==""){
                this.setState({git: "---"});
            }
        });
    }
    onDeletePhoneNumber(){
        employeeService.deletePhoneNumber(this.state.employeeId).then(res =>{
            if(res!==""){
                this.setState({phone: "---"});
            }
        });
    }
    componentWillMount(){
        var token = method.getToken();
      if(token){
        tokenService.getUser().then(res =>{
          if(res!==null){
            var data = JSON.parse(res.data);
            var git = "";
            var phone = "";
            if(data.GitLink===null||data.GitLink===""){
                git = "---";
            }
            else git = data.GitLink;
            if(data.PhoneNumber===null||data.PhoneNumber===""){
                phone = "---";
            }
            else phone = data.PhoneNumber; 
            this.setState({employee: data,
                employeeId: data.Id, name: data.EmployeeName, surname: data.EmployeeSurname,
            patronymic: data.EmployeePatronymic, git: git, phone: phone});
          }
      });
    }
    }
    render(){
       if(!this.state.employee) return <div>Загрузка...</div>
       return <div id="settings">
       <h3>Настройки</h3>
       <br></br>
           <Table>
           <tbody>
                <tr>
                    <th>Фамилия:</th>
                    <td>{this.state.surname}</td>
                    <td><Button onClick={() => this.onChangeSurname()}>
                        Изменить</Button>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th>Имя:</th>
                    <td>{this.state.name}</td>
                    <td><Button onClick={() => this.onChangeName()}>
                        Изменить</Button>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th>Отчество:</th>
                    <td>{this.state.patronymic}</td>
                    <td><Button onClick={() => this.onChangePatronymic()}>
                        Изменить</Button>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th>Git:</th>
                    <td>{this.state.git}</td>
                    <td><Button onClick={() => this.onChangeGit()}>
                        Изменить</Button>
                    </td>
                    <td><Button onClick={() => this.onDeleteGit()}>
                        Удалить</Button>
                    </td>
                </tr>
                <tr>
                    <th>Телефон:</th>
                    <td>{this.state.phone}</td>
                    <td><Button onClick={() => this.onChangePhoneNumber()}>
                        Изменить</Button>
                    </td>
                    <td><Button onClick={() => this.onDeletePhoneNumber()}>
                        Удалить</Button>
                    </td>
                </tr>
                <tr>
                    <td><Button onClick={() => this.onChangePassword()}>
                        Изменить пароль</Button>
                    </td>
                </tr>
            </tbody> 
       </Table>
       <Modal show={this.state.changeSurnameShow} onHide={this.changeSurnameModalClose}>
            <Modal.Header closeButton>Изменить фамилию</Modal.Header>
                <Modal.Body>
                    <Form onSubmit={this.onNewSurnameSubmit}>
                        <FormGroup validationState={this.validateSurname()}>
                            <FormControl 
                                type="text"
                                placeholder="Новая фамилия"
                                value={this.state.newsurname}
                                onChange={this.onSurnameChange} />
                            <FormControl.Feedback />
                        </FormGroup>
                        <Button type="submit">Изменить</Button>
                    </Form>
                </Modal.Body>
        </Modal>
        <Modal show={this.state.changeNameShow} onHide={this.changeNameModalClose}>
            <Modal.Header closeButton>Изменить имя</Modal.Header>
                <Modal.Body>
                <Form onSubmit={this.onNewNameSubmit}>
                        <FormGroup validationState={this.validateName()}>
                            <FormControl 
                                type="text"
                                placeholder="Новое имя"
                                value={this.state.newname}
                                onChange={this.onNameChange} />
                            <FormControl.Feedback />
                        </FormGroup>
                        <Button type="submit">Изменить</Button>
                    </Form>
                </Modal.Body>
        </Modal>
        <Modal show={this.state.changePatronymicShow} onHide={this.changePatronymicModalClose}>
            <Modal.Header closeButton>Изменить отчество</Modal.Header>
                <Modal.Body>
                <Form onSubmit={this.onNewPatronymicSubmit}>
                        <FormGroup validationState={this.validatePatronymic()}>
                            <FormControl 
                                type="text"
                                placeholder="Новое отчество"
                                value={this.state.newpatronymic}
                                onChange={this.onPatronymicChange} />
                            <FormControl.Feedback />
                        </FormGroup>
                        <Button type="submit">Изменить</Button>
                    </Form>
                </Modal.Body>
        </Modal>
        <Modal show={this.state.changeGitShow} onHide={this.changeGitModalClose}>
            <Modal.Header closeButton>Изменить git</Modal.Header>
                <Modal.Body>
                <Form onSubmit={this.onNewGitSubmit}>
                        <FormGroup validationState={this.validateGit()}>
                            <FormControl 
                                type="text"
                                placeholder="Новая ссылка"
                                value={this.state.newgit}
                                onChange={this.onGitChange} />
                            <FormControl.Feedback />
                        </FormGroup>
                        <Button type="submit">Изменить</Button>
                    </Form>
                </Modal.Body>
        </Modal>
        <Modal show={this.state.changePhoneShow} onHide={this.changePhoneModalClose}>
            <Modal.Header closeButton>Изменить номер телефона</Modal.Header>
                <Modal.Body>
                <Form onSubmit={this.onNewPhoneNumberSubmit}>
                        <FormGroup validationState={this.validatePhone()}>
                            <FormControl 
                                type="text"
                                placeholder="+375(xx)xxxxxxx"
                                value={this.state.newphone}
                                onChange={this.onPhoneNumberChange} />
                            <FormControl.Feedback />
                        </FormGroup>
                        <Button type="submit">Изменить</Button>
                    </Form>
                </Modal.Body>
        </Modal>
        <Modal show={this.state.changePasswordShow} onHide={this.changePasswordModalClose}>
            <Modal.Header closeButton>Изменить пароль</Modal.Header>
                <Modal.Body>
                <Form  onSubmit={this.onSubmit}>
                    <FormGroup>
                        <FormControl
                            type="password"
                            placeholder="Новый пароль" 
                            value={this.state.password}
                            onChange={this.onPasswordChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup validationState={this.validatePassword()}>
                        <FormControl
                            type="password"
                            placeholder="Подтверждение пароля" 
                            value={this.state.confirmedPassword}
                            onChange={this.onConfirmedPassword} />
                        <FormControl.Feedback />
                    </FormGroup>
                <Button type="submit">Изменить</Button>
                    </Form>  
                </Modal.Body>
        </Modal>
        <Modal show={this.state.show} onHide={this.handleClose}>
                <Modal.Header closeButton>Ошибка</Modal.Header>
                    <Modal.Body>
                        <div>Новый пароль не должен совпадать со старым</div>
                </Modal.Body>
        </Modal>  
   </div>
    }
}

export default SettingsPage;