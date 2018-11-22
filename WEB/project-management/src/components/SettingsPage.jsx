import React, { Component } from 'react';
import * as method from '../../src/services/methods';
import * as tokenService from '../../src/services/tokenService';
import * as employeeService from '../services/employeeService';
import {Table, Button, Modal, Form, FormControl, FormGroup} from 'react-bootstrap';
import '../styles/Settings.css'

class SettingsPage extends Component{
    constructor(props){
        super(props);
        this.state = {employeeId: 0, employee: null, show1: false, show2: false,
        show3: false, show4: false, show5: false, surname: "", newsurname: "", name: "", newname: "",
        patronymic: "", newpatronymic: "", git: "", newgit: "", phone: "", newphone: "", password: "", 
        confirmedPassword: "", show: false, show6: false};
 
        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleShow1 = this.handleShow1.bind(this);
        this.handleClose1 = this.handleClose1.bind(this);
        this.handleShow2 = this.handleShow2.bind(this);
        this.handleClose2 = this.handleClose2.bind(this);
        this.handleShow3 = this.handleShow3.bind(this);
        this.handleClose3 = this.handleClose3.bind(this);
        this.handleShow4 = this.handleShow4.bind(this);
        this.handleClose4 = this.handleClose4.bind(this);
        this.handleShow5 = this.handleShow5.bind(this);
        this.handleClose5 = this.handleClose5.bind(this);
        this.handleShow6 = this.handleShow6.bind(this);
        this.handleClose6 = this.handleClose6.bind(this);
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
    handleClose1() {
        this.setState({ show1: false });
      }
    handleShow1() {
        this.setState({ show1: true });
      }
    handleClose2() {
        this.setState({ show2: false });
      }
    handleShow2() {
        this.setState({ show2: true });
      }
    handleClose3() {
        this.setState({ show3: false });
      }
    handleShow3() {
        this.setState({ show3: true });
      }
    handleClose4() {
        this.setState({ show4: false });
      }
    handleShow4() {
        this.setState({ show4: true });
      }
    handleClose5() {
        this.setState({ show5: false });
      }
    handleShow5() {
        this.setState({ show5: true });
      }
    handleClose6() {
        this.setState({ show6: false });
      }
    handleShow6() {
        this.setState({ show6: true });
      }
    onChangeSurname(){
        this.handleShow1();
    }
    onChangeName(){
        this.handleShow2();
    }
    onChangePatronymic(){
        this.handleShow3();
    }
    onChangeGit(){
        this.handleShow4();
    }
    onChangePhoneNumber(){
        this.handleShow5();
    }
    onChangePassword(){
        this.handleShow6();
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
        this.handleClose1();
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
        this.handleClose2();
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
        this.handleClose3();
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
        this.handleClose4();
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
        this.handleClose5();
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
                this.handleClose6();
            }
        }).catch(()=>{
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
       <Modal show={this.state.show1} onHide={this.handleClose1}>
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
        <Modal show={this.state.show2} onHide={this.handleClose2}>
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
        <Modal show={this.state.show3} onHide={this.handleClose3}>
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
        <Modal show={this.state.show4} onHide={this.handleClose4}>
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
        <Modal show={this.state.show5} onHide={this.handleClose5}>
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
        <Modal show={this.state.show6} onHide={this.handleClose6}>
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