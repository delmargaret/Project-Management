import React, { Component } from 'react';
import * as tokenService from '../../src/services/tokenService';
import {Button, Form, FormControl, FormGroup} from 'react-bootstrap';
import * as method from '../../src/services/methods';

class LogInPage extends Component {
    constructor(props){
      super(props);
      this.state = {email: "", password: ""}
  
      this.onSubmit = this.onSubmit.bind(this);
      this.onEmailChange = this.onEmailChange.bind(this);
      this.onPasswordChange = this.onPasswordChange.bind(this);
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
            if(res!==null){
                var token = JSON.parse(res.data);
                method.setToken(token);
                this.props.onLogIn();
            }
        });
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
                <Button type="submit">Войти</Button>
                    </Form>           
      </div>
  }
}

export default LogInPage;