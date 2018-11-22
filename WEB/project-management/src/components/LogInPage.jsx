import React, { Component } from 'react';
import * as tokenService from '../../src/services/tokenService';
import {Button, Form, FormControl, FormGroup, Modal} from 'react-bootstrap';
import * as method from '../../src/services/methods';
import '../styles/logInPage.css'

class LogInPage extends Component {
    constructor(props){
      super(props);
      this.state = {email: "", password: "", show: false}
  
      this.onSubmit = this.onSubmit.bind(this);
      this.onEmailChange = this.onEmailChange.bind(this);
      this.onPasswordChange = this.onPasswordChange.bind(this);
      this.handleShow = this.handleShow.bind(this);
      this.handleClose = this.handleClose.bind(this);
  }
    handleClose() {
        this.setState({ show: false });
    }
    handleShow() {
        this.setState({ show: true });
    }
    onEmailChange(e) {
        var val = e.target.value;
        this.setState({email: val});
    }
    onPasswordChange(e) {
        var val = e.target.value;
        this.setState({password: val});
    }
    onSubmit(e){
        e.preventDefault();
        var email = this.state.email.trim();
        var password = this.state.password.trim();
        tokenService.login(email, password).then(res =>{
            if(res!==""){
                var token = JSON.parse(res.data);
                method.setToken(token);
                this.props.onLogIn();
            } 
        }).catch(()=>{this.handleShow();})
    }
    render(){
      return <div id="logindiv">
          <Form  onSubmit={this.onSubmit} id="loginform">
                    <FormGroup>
                        <FormControl
                            type="email"
                            placeholder="E-mail"
                            value={this.state.email}
                            onChange={this.onEmailChange} />
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup>
                        <FormControl
                            type="password"
                            placeholder="Пароль" 
                            value={this.state.password}
                            onChange={this.onPasswordChange} />
                        <FormControl.Feedback />
                    </FormGroup>
                <Button type="submit">Войти</Button>
                    </Form>  
                    <Modal show={this.state.show} onHide={this.handleClose}>
                        <Modal.Header closeButton>Ошибка</Modal.Header>
                            <Modal.Body>
                                <div>Неверно введен логин или пароль!</div>
                            </Modal.Body>
                    </Modal>         
      </div>
  }
}

export default LogInPage;