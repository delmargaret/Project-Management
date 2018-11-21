import React, { Component } from 'react';
import * as tokenService from '../../src/services/tokenService';
import {Button, Form, FormControl, FormGroup} from 'react-bootstrap';

class RegistrationPage extends Component {
    constructor(props){
      super(props);
      this.state = {email: "", password: "", confirmedPassword: ""}
  
      this.onSubmit = this.onSubmit.bind(this);
      this.onEmailChange = this.onEmailChange.bind(this);
      this.onPasswordChange = this.onPasswordChange.bind(this);
      this.onConfirmedPassword = this.onConfirmedPassword.bind(this);
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
        if (this.state.password!==this.state.confirmedPassword) return 'error';
        if (this.state.password===this.state.confirmedPassword) return 'success';
    }
    onSubmit(e){
        e.preventDefault();
        var email = this.state.email.trim();
        var password = this.state.password.trim();
        tokenService.registrate(email, password);
    }
    render(){
      return <div>
          <Form  onSubmit={this.onSubmit}>
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

                    <FormGroup>
                        <FormControl
                            type="password"
                            placeholder="Подтверждение пароля" 
                            value={this.state.confirmedPassword}
                            onChange={this.onConfirmedPassword} />
                        <FormControl.Feedback />
                    </FormGroup>
                <Button type="submit">Зарегистрироваться</Button>
                    </Form>           
      </div>
  }
}

export default RegistrationPage;