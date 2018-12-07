import React, { Component } from 'react';
import * as tokenService from '../services/tokenService';
import {Button, Form, FormControl, FormGroup, Modal} from 'react-bootstrap';
import '../styles/RegistrationPage.css';

class RegistrationPage extends Component {
    constructor(props){
      super(props);
      this.state = {email: "", password: "", confirmedPassword: "", errorShow: false, successShow: false}
  
      this.onSubmit = this.onSubmit.bind(this);
      this.onEmailChange = this.onEmailChange.bind(this);
      this.onPasswordChange = this.onPasswordChange.bind(this);
      this.onConfirmedPassword = this.onConfirmedPassword.bind(this);
      this.errorModalShow = this.errorModalShow.bind(this);
      this.errorModalClose = this.errorModalClose.bind(this);
      this.successModalShow = this.successModalShow.bind(this);
      this.successModalClose = this.successModalClose.bind(this);
  }
    errorModalClose() {
        this.setState({ errorShow: false });
    }
    errorModalShow() {
        this.setState({ errorShow: true });
    }
    successModalClose() {
        this.setState({ successShow: false });
    }
    successModalShow() {
        this.setState({ successShow: true });
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
                this.successModalShow();
            }
        }).catch(()=>{
            this.errorModalShow();
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
            <Modal show={this.state.errorShow} onHide={this.errorModalClose}>
                <Modal.Header closeButton>Ошибка</Modal.Header>
                    <Modal.Body>
                        <div>Неверный e-mail</div>
                    </Modal.Body>
            </Modal>     
            <Modal show={this.state.successShow} onHide={this.successModalClose}>
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