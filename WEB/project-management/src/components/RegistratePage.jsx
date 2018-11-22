import React, { Component } from 'react';
import * as tokenService from '../../src/services/tokenService';
import {Button, Form, FormControl, FormGroup, Modal} from 'react-bootstrap';
import '../styles/RegistrationPage.css';

class RegistrationPage extends Component {
    constructor(props){
      super(props);
      this.state = {email: "", password: "", confirmedPassword: "", show: false, show1: false}
  
      this.onSubmit = this.onSubmit.bind(this);
      this.onEmailChange = this.onEmailChange.bind(this);
      this.onPasswordChange = this.onPasswordChange.bind(this);
      this.onConfirmedPassword = this.onConfirmedPassword.bind(this);
      this.handleShow = this.handleShow.bind(this);
      this.handleClose = this.handleClose.bind(this);
      this.handleShow1 = this.handleShow1.bind(this);
      this.handleClose1 = this.handleClose1.bind(this);
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
    onEmailChange(e) {
        var val = e.target.value;
        this.setState({email: val});
    }
    onPasswordChange(e) {
        var val = e.target.value;
        this.setState({password: val});
    }
    onConfirmedPassword(e) {
        var val = e.target.value;
        this.setState({confirmedPassword: val});
    }
    validatePassword(){
        if (this.state.password.length===0) return null;
        if (this.state.password!==this.state.confirmedPassword) return 'error';
        if (this.state.password===this.state.confirmedPassword) return 'success';
    }
    onSubmit(e){
        e.preventDefault();
        var email = this.state.email.trim();
        var password = this.state.password.trim();
        tokenService.registrate(email, password).then(res =>{
            if(res!==""){
                this.handleShow1();
            }
        }).catch(()=>{
            this.handleShow();
        })
    }
    render(){
      return <div id="registrationdiv">
          <Form  onSubmit={this.onSubmit} id="registrationform" >
                    <FormGroup >
                        <FormControl
                            type="email"
                            placeholder="E-mail"
                            value={this.state.email}
                            onChange={this.onEmailChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup >
                        <FormControl
                            type="password"
                            placeholder="Пароль" 
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
                <Button type="submit">Зарегистрироваться</Button>
                    </Form>  
                    <Modal show={this.state.show} onHide={this.handleClose}>
                        <Modal.Header closeButton>Ошибка</Modal.Header>
                            <Modal.Body>
                                <div>Неверный e-mail</div>
                            </Modal.Body>
                    </Modal>     
                    <Modal show={this.state.show1} onHide={this.handleClose1}>
                        <Modal.Header closeButton></Modal.Header>
                            <Modal.Body>
                                <div>Вы успешно зарегистрированы!</div>
                                <div>Для того, чтобы продолжить, войдите в систему</div>
                                <a href="/login">Войти</a>
                            </Modal.Body>
                    </Modal>     
      </div>
  }
}

export default RegistrationPage;